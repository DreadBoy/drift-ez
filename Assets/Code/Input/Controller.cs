using InControl;
using UnityEngine;

public class Controller : MonoBehaviour {

    InputDevice controller;

    void Start() {
        //TODO: dont get on start
        controller = InputManager.ActiveDevice;
    }

    public void SetController(InputDevice controller) {
        this.controller = controller;
    }

    public float GetSteering() {
        return controller.GetControl(InputControlType.LeftStickX).Value;
    }

    public float GetThrottle() {
        return Mathf.Max(controller.GetControl(InputControlType.RightStickY).Value, 0.0f);
    }

    public float GetBrake() {
        return -Mathf.Min(controller.GetControl(InputControlType.RightStickY).Value, 0.0f);
    }

    public bool GetShiftUp() {
        return controller.GetControl(InputControlType.DPadUp).WasPressed;
    }

    public bool GetShiftDown() {
        return controller.GetControl(InputControlType.DPadDown).WasPressed;
    }

    public bool IsClutchPressed() {
        return controller.GetControl(InputControlType.Action2).IsPressed;
    }

    public bool IsHandBrakePressed() {
        return controller.GetControl(InputControlType.Action3).IsPressed;
    }
}