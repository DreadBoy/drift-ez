using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(AudioSource))]
public class Car : MonoBehaviour {

    public new Camera camera;
    Controller controller;
    AudioSource audioSource;

    [SerializeField]
    AudioClip idle = null, running = null;

    Vector3 direction = Vector3.zero;
    float speed = 0;

    private void Awake() {
        camera = GetComponent<Camera>();
        controller = GetComponent<Controller>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    //TODO Replace this with input script
    {
        direction.y = 30 * controller.GetSteering();
        if (direction.y != 0 && speed > 0) {
            float sign = speed / Mathf.Abs(speed);
            transform.Rotate(0, sign * direction.y * Time.deltaTime, 0);
        }
        if (controller.GetThrottle() > 0.05) {
            speed += controller.GetThrottle() * 10 * 20 * Time.deltaTime;
            speed = Mathf.Min(speed, 5);
        } else {
            speed -= 10 * Time.deltaTime;
            speed = Mathf.Max(speed, 0);
        }
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
}
