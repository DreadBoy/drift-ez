using InControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ControllerManager : MonoBehaviour {


    private InputDevice player1 = null;
    private InputDevice player2 = null;

    private bool player1Ready;
    private bool player2Ready;

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
        if (SceneManager.GetActiveScene().name == "MainMenu") {
            if (InputManager.ActiveDevice.GetControl(InputControlType.Action1).WasPressed) {
                InputDevice controller = InputManager.ActiveDevice;

                if (controller == player1) {
                    Debug.LogError("Player1 ready");
                    player1Ready = true;
                    Events.OnPlayerReady(true);
                } else if (controller == player2) {
                    Debug.LogError("Player2 ready");
                    player2Ready = true;
                    Events.OnPlayerReady(false);
                }

                if (player1 == null && controller != player2) {
                    player1 = InputManager.ActiveDevice;
                    player1Ready = false;
                    Events.OnPlayerJoined(true);
                } else if (player2 == null && controller != player1) {
                    player2 = InputManager.ActiveDevice;
                    player2Ready = false;
                    Events.OnPlayerJoined(false);
                }
            }

            if (InputManager.ActiveDevice.GetControl(InputControlType.Action2).WasReleased) {
                InputDevice controller = InputManager.ActiveDevice;
                if (controller == player1 && !player1Ready) {
                    player1 = null;
                    player1Ready = false;
                    Events.OnPlayerLeft(true);
                } else if (controller == player2 && !player2Ready) {
                    player2 = null;
                    player2Ready = false;
                    Events.OnPlayerLeft(false);
                }

            }
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
        return player1;
    }

    public InputDevice GetPlayer2InputDevice() {
        return player2;
    }
}
