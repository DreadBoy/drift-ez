using InControl;
using UnityEngine;
using System;

public class Controller : MonoBehaviour {

    public InputDevice inputDevice;
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
        if (inputDevice == null)
            return 0;
        return inputDevice.GetControl(InputControlType.LeftStickX).Value;
    }

    public float GetThrottle() {
        if (inputDevice == null)
            return 0;
        return Mathf.Max(inputDevice.GetControl(InputControlType.RightTrigger).Value, 0.0f);
    }

    public float GetBrake() {
        if (inputDevice == null)
            return 0;
        return -Mathf.Min(inputDevice.GetControl(InputControlType.LeftTrigger).Value, 0.0f);
    }

    public bool GetShiftUp() {
        if (isPlayer1) {
            return Input.GetKeyDown(KeyCode.W);
        } else {
            return Input.GetKeyDown(KeyCode.UpArrow);
        }
    }

    public bool GetShiftDown() {
        if (isPlayer1) {
            return Input.GetKeyDown(KeyCode.S);
        } else {
            return Input.GetKeyDown(KeyCode.DownArrow);
        }
    }

    public bool IsClutchPressed() {
        return inputDevice.GetControl(InputControlType.Action2).IsPressed;
    }

    public bool IsHandBrakePressed() {
        if (isPlayer1) {
            return Input.GetKey(KeyCode.LeftShift);
        } else {
            return Input.GetKey(KeyCode.RightShift);
        }
    }
}