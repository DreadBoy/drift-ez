using UnityEngine;

public class Car : MonoBehaviour
{
    new Camera camera;

    Vector3 direction = Vector3.zero;
    float speed = 0;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    void Update()
    //TODO Replace this with input script
    {
        direction.y = 20 * Input.GetAxis("Horizontal");
        if (direction.y != 0 && speed > 0)
        {
            float sign = speed / Mathf.Abs(speed);
            transform.Rotate(0, sign * direction.y * Time.deltaTime, 0);
        }
        if (-Input.GetAxis("Vertical") > 0.05)
        {
            speed += -Input.GetAxis("Vertical") * 10 * 20 * Time.deltaTime;
            speed = Mathf.Min(speed, 5);
        }
        else
        {
            speed -= 10 * Time.deltaTime;
            speed = Mathf.Max(speed, 0);
        }
        transform.Translate(speed * transform.forward * Time.deltaTime, Space.World);
        Debug.DrawRay(camera.transform.position, Quaternion.Euler(direction) * transform.forward * 5);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 300, 50), "Vertical " + Input.GetAxis("Vertical"));
        GUI.Label(new Rect(0, 15, 300, 50), "Horizontal " + Input.GetAxis("Horizontal"));
        GUI.Label(new Rect(0, 30, 300, 50), "Speed " + speed);
    }
}
