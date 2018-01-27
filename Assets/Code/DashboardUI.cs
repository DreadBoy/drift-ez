using UnityEngine;
using UnityEngine.UI;

public class DashboardUI : MonoBehaviour
{

    public Car car;
    public Text gear, speed, revolution;
    public RectTransform wheelSprite;

    private void Start()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(960 * car.playerIndex, 0);
    }

    void Update()
    {
        gear.text = car.gear.ToString();
        speed.text = car.speed.ToString("0.00");
        revolution.text = car.revolution.ToString("0.00");
        wheelSprite.rotation = Quaternion.Euler(0, 0, -car.direction.y * 2);

    }
}