using UnityEditor;
using UnityEngine;

public class CarPlaceholder : MonoBehaviour
{

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 5);
    }

    [MenuItem("GameObject/Create Other/Car Placeholder", false, 10)]
    static void Create()
    {
        GameObject car = GameObject.CreatePrimitive(PrimitiveType.Cube);
        car.AddComponent<CarPlaceholder>().name = "Car Placeholder";
        car.transform.localScale = new Vector3(1, 1, 2);
    }
}
