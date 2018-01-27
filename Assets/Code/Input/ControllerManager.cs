using InControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ControllerManager : MonoBehaviour {


    private InputDevice player1 = null;
    private InputDevice player2 = null;

    // Use this for initialization
    void Start() {
        if (SceneManager.GetActiveScene().name != "MainMenu") {
            if (player1 == null && player2 == null) {
                Debug.LogError("No device selected -> Using default input device");
                player1 = InputManager.ActiveDevice;
            }
        }
    }

    void Update() {
        if (InputManager.ActiveDevice.GetControl(InputControlType.Action1).WasPressed) {
            InputDevice controller = InputManager.ActiveDevice;
            if (player1 == null && controller != player2) {
                player1 = InputManager.ActiveDevice;
            } else if (player2 == null && controller != player1) {
                player2 = InputManager.ActiveDevice;
            }
        }
        player1 = CheckForControllerCancel(player1);
        player2 = CheckForControllerCancel(player2);

        if (SceneManager.GetActiveScene().name == "MainMenu") {
            Debug.Log("Player 1: " + (player1 == null ? "null" : player1.Name) + " Player2: " + (player2 == null ? "null" : player2.Name));
        }
    }

    private InputDevice CheckForControllerCancel(InputDevice controller) {
        if (controller != null && controller.GetControl(InputControlType.Action2).WasPressed) {
            return null;
        }
        return controller;
    }

    public InputDevice GetInputDeviceForPlayer(int player) {
        if (player == 0) {
            return player1;

        } else if (player == 1) {
            return player2;

        } else {
            throw new InvalidOperationException("Max 2 players supported!");
        }

    }

    public InputDevice GetPlayer1InputDevice() {
        return player1; ;
    }

    public InputDevice GetPlayer2InputDevice() {
        return player2;
    }
}
