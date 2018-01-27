using UnityEngine;
using UnityEngine.UI;

public class DashboardUI : MonoBehaviour
{

    public Car car;
    public Text gear;
    public RectTransform wheelSprite;
    public MaskedSlider accelerationSlider;
    public MaskedSlider speedSlider;

    private void Start()
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(960 * car.playerIndex, 0);
    }

    void Update()
    {
        gear.text = car.gear.ToString();

        speedSlider.SetValue(car.speed);
        float acceleration = 10 - car.acceleration;
        if (8 < acceleration && acceleration < 10)
        {
            accelerationSlider.SetValue(acceleration);
            accelerationSlider.SetColor(new Color(61 / 255f, 255 / 255f, 49 / 255f));
        }
        else if (acceleration > 10)
        {
            accelerationSlider.SetValue(10);
            accelerationSlider.SetColor(new Color(1, 0, 0));
        }
        else
        {
            accelerationSlider.SetValue(acceleration);
            accelerationSlider.ResetColor();
        }
        wheelSprite.rotation = Quaternion.Euler(0, 0, -car.direction.y * 2);
    }
}