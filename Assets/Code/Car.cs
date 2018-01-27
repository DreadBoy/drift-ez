using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(AudioSource))]
public class Car : MonoBehaviour
{

    [HideInInspector]
    public new Camera camera;
    [HideInInspector]
    public Controller controller;
    AudioSource audioSource;

    [SerializeField]
    AudioClip idle = null, running = null;

    Vector3 direction = Vector3.zero;
    [HideInInspector]
    public float speed = 0;
    [HideInInspector]
    public int gear = 1;
    [HideInInspector]
    public float revolution = 0;

    [HideInInspector]
    public int playerIndex = 0;

    float targetSpeed;

    private void Awake()
    {
        camera = GetComponentInChildren<Camera>();
        controller = GetComponent<Controller>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        direction.y = 30 * controller.GetSteering();
        if (direction.y != 0 && speed > 0)
        {
            float sign = speed / Mathf.Abs(speed);
            transform.Rotate(0, sign * direction.y * Time.deltaTime, 0);
        }

        if (controller.GetThrottle() > 0.05)
        {
            revolution += controller.GetThrottle() * 10 * 20 * Time.deltaTime;
            revolution = Mathf.Min(revolution, 5);
        }
        else
        {
            revolution -= 10 * Time.deltaTime;
            revolution = Mathf.Max(revolution, 0);
        }

        if (controller.GetShiftUp())
            gear++;
        gear = Mathf.Min(gear, 5);
        if (controller.GetShiftDown())
            gear--;
        gear = Mathf.Max(gear, 1);

        targetSpeed = gear * revolution / 2.5f;
        if (targetSpeed > speed)
            speed += (targetSpeed - speed) / 2 * Time.deltaTime;
        else if (targetSpeed < speed)
            speed += (targetSpeed - speed) * Time.deltaTime;
        transform.Translate(speed * 5 * transform.forward * Time.deltaTime, Space.World);

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
        GUI.Label(new Rect(0, 0, 300, 30), "target speed " + targetSpeed.ToString());
        GUI.Label(new Rect(0, 30, 300, 30), "speed " + speed.ToString());
    }
}
