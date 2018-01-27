using InControl;
using UnityEngine;

public class Controller : MonoBehaviour {

    InputDevice inputDevice;
    InputDevice keyboard;

    void Awake() {
        inputDevice = InputManager.ActiveDevice;
        foreach (InputDevice device in InputManager.Devices) {
            if (device.Name == "Keyboard") {
                keyboard = device; ;
            }
        }

    }

    public void SetInputDevice(InputDevice inputDevice) {
        this.inputDevice = inputDevice;
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
        return inputDevice.GetControl(InputControlType.DPadUp).WasPressed;
    }

    public bool GetShiftDown() {
        return inputDevice.GetControl(InputControlType.DPadDown).WasPressed;
    }

    public bool IsClutchPressed() {
        return inputDevice.GetControl(InputControlType.Action2).IsPressed;
    }

    public bool IsHandBrakePressed() {
        return inputDevice.GetControl(InputControlType.Action3).IsPressed;
    }
}