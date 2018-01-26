using UnityEngine;

public class Car : MonoBehaviour
{
    new Camera camera;
    Vector3 direction = Vector3.zero;

    private void Start()
    {
        camera = FindObjectOfType<Camera>();
    }

    void Update()
    //TODO Replace this with input script
    {
        if (Input.GetKey(KeyCode.A))
            direction.y = direction.y - 30 * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            direction.y = direction.y + 30 * Time.deltaTime;
        if (direction.y != 0)
        {
            direction.y = direction.y / Mathf.Abs(direction.y) * Mathf.Min(Mathf.Abs(direction.y), 20);
            if (Input.GetKey(KeyCode.W))
                transform.Rotate(0, direction.y * Time.deltaTime, 0);
            if (Input.GetKey(KeyCode.S))
                transform.Rotate(0, -direction.y * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.W))
            transform.Translate(transform.forward * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(-transform.forward * Time.deltaTime, Space.World);
        Debug.DrawRay(camera.transform.position, Quaternion.Euler(direction) * transform.forward * 5);
    }
}
