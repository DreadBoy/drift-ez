using InControl;
using UnityEngine;
using System;

public class Controller : MonoBehaviour {

    InputDevice inputDevice;
    bool isPlayer1 = true;

    void Awake() {
        inputDevice = InputManager.ActiveDevice;


    }

    public void SetInputDevice(InputDevice inputDevice) {
        this.inputDevice = inputDevice;
    }

    public void SetPlayer(bool isPlayer1) {
        this.isPlayer1 = isPlayer1;
    }

    public InputDevice GetInputDevice() {
        return inputDevice;
    }

    public float GetSteering() {
        return inputDevice.GetControl(InputControlType.LeftStickX).Value;
    }

    public float GetThrottle() {
        return Mathf.Max(inputDevice.GetControl(InputControlType.RightStickY).Value, 0.0f);
    }

    public float GetBrake() {
        return -Mathf.Min(inputDevice.GetControl(InputControlType.RightStickY).Value, 0.0f);
    }

    public bool GetShiftUp() {
        if (isPlayer1) {
            return Input.GetKeyDown(KeyCode.UpArrow);
        } else {
            return Input.GetKeyDown(KeyCode.W);
        }
    }

    public bool GetShiftDown() {
        if (isPlayer1) {
            return Input.GetKeyDown(KeyCode.DownArrow);
        } else {
            return Input.GetKeyDown(KeyCode.S);
        }
    }

    public bool IsClutchPressed() {
        return inputDevice.GetControl(InputControlType.Action2).IsPressed;
    }

    public bool IsHandBrakePressed() {
        return inputDevice.GetControl(InputControlType.Action3).IsPressed;
    }
}