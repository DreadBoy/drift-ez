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
}