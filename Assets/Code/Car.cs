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

    public AnimationCurve[] accelerationCurves;
    public AnimationCurve[] deccelerationCurves;

    float targetSpeed;
    float acceleration;

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
        if (controller.GetShiftUp() && gear < accelerationCurves.Length)
        {
            gear++;
        }
        if (controller.GetShiftDown() && gear > 1)
        {
            gear--;
        }

        /*      REVOLUTION CONTROLL      */
        if (controller.GetThrottle() > 0.05)
        {
            acceleration = accelerationCurves[gear - 1].Evaluate(speed) * controller.GetThrottle();
        }
        else
        {
            acceleration = -deccelerationCurves[gear - 1].Evaluate(speed);
        }

        if (isDrifting)
        {
            acceleration = -2;
        }
        else if (controller.IsHandBrakePressed())
        {
            acceleration = -20;
        }
        speed += acceleration * Time.deltaTime * 0.1f;
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
        GUI.Label(new Rect(0, 0, 300, 30), "revolution " + revolution);
        GUI.Label(new Rect(0, 30, 300, 30), "handbrake " + controller.IsHandBrakePressed());
    }
}
