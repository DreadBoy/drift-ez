using UnityEngine;
using UnityEngine.UI;

public class DashboardUI : MonoBehaviour
{

    public Car car;
    public Text gear, speed, revolution;

    void Update()
    {
        gear.text = car.gear.ToString();
        speed.text = car.speed.ToString("0.00");
        revolution.text = car.revolution.ToString("0.00");
    }
}