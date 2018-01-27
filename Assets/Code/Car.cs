using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(AudioSource))]
public class Car : MonoBehaviour
{

    public Transform model;
    [HideInInspector]
    public Controller controller;
    AudioSource audioSource;

    [SerializeField]
    AudioClip idle = null, running = null;

    [HideInInspector]
    public Vector3 direction = Vector3.zero;
    [HideInInspector]
    public float speed = 0;
    [HideInInspector]
    public int gear = 1;
    [HideInInspector]
    public float revolution = 0;

    [HideInInspector]
    public int playerIndex = 0;
    [HideInInspector]
    public bool isDrifting;

    float targetSpeed;
    IEnumerator driftingCamera;

    private void Awake()
    {
        controller = GetComponent<Controller>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        /*      STEERING CONTROLL      */
        direction.y = 30 * controller.GetSteering();
        isDrifting = speed > 7 && controller.IsHandBrakePressed() && Mathf.Abs(controller.GetSteering()) > 0.7f;
        if (isDrifting)
        {
            direction.y = (30 + revolution * 10) * controller.GetSteering();
        }
        if (direction.y != 0 && speed > 0.05)
        {
            float sign = speed / Mathf.Abs(speed);
            transform.Rotate(0, sign * direction.y * Time.deltaTime, 0);
        }


        /*      GEAR CONTROLL      */
        if (controller.GetShiftUp() && gear < 5)
        {
            gear++;
            revolution -= 5 * 0.75f;
        }
        if (controller.GetShiftDown() && gear > 1)
        {
            gear--;
            revolution = 5;
        }

        float maxSpeed = gear * 2;
        float minSpeed = (gear - 1) * 2;
        targetSpeed = gear * 2 + revolution / 2.5f;
        /*      REVOLUTION CONTROLL      */
        if (controller.GetThrottle() > 0.05 && speed > minSpeed - 0.5f)
        {
            revolution += controller.GetThrottle() * 10 * 20 * Time.deltaTime;
            revolution = Mathf.Min(revolution, 5);
        }
        if (controller.GetThrottle() > 0.05 && speed <= minSpeed - 0.5f)
        {
            revolution -= 20 * Time.deltaTime;
            revolution = Mathf.Max(revolution, 0);
        }
        else if (!isDrifting)
        {
            revolution -= 10 * Time.deltaTime;
            revolution = Mathf.Max(revolution, 0);
        }

        if (isDrifting)
        {
            speed -= 2 * Time.deltaTime;
            speed = Mathf.Max(speed, 0);
        }
        else if (controller.IsHandBrakePressed())
        {
            speed -= 10 * Time.deltaTime;
            speed = Mathf.Max(speed, 0);
        }
        else if (targetSpeed > speed)
            speed += (targetSpeed - speed) / 2 * Time.deltaTime;
        else if (targetSpeed < speed)
            speed += (targetSpeed - speed) * Time.deltaTime;
        transform.Translate(speed * 5 * transform.forward * Time.deltaTime, Space.World);

        /*      SOUND CONTROLL      */
        if (controller.GetThrottle() > 0 && audioSource.clip != running)
        {
            audioSource.clip = running;
            audioSource.Play();
        }
        else if (controller.GetThrottle() == 0 && audioSource.clip != idle)
        {
            audioSource.clip = idle;
            audioSource.Play();
        }
        audioSource.pitch = 1f + 0.4f * controller.GetThrottle();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 30), "steering " + controller.GetSteering().ToString());
        GUI.Label(new Rect(0, 30, 300, 30), "handbrake " + controller.IsHandBrakePressed());
    }
}
