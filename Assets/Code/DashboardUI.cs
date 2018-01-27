﻿using UnityEngine;
using UnityEngine.UI;

public class DashboardUI : MonoBehaviour {

    public Car car;
    public Text gear;
    public RectTransform wheelSprite;
    public MaskedSlider accelerationSlider;
    public MaskedSlider speedSlider;

    private void Start() {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(960 * car.playerIndex, 0);
    }

    void Update() {
        gear.text = car.gear.ToString();

        speedSlider.SetValue(car.speed);
        float acceleration = 10 - car.acceleration;
        if (acceleration > 10)
            acceleration = 0;
        accelerationSlider.SetValue(acceleration);
        Debug.Log(10 - acceleration);
        //if (car.acceleration < 2)
        //    accelerationSlider.SetColor(new Color(61, 255, 49));
        //else
        //    accelerationSlider.ResetColor();
        wheelSprite.rotation = Quaternion.Euler(0, 0, -car.direction.y * 2);


    }
}