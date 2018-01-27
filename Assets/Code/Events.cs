using UnityEngine;
using InControl;

public static class Events {
    private static bool debugMessages = true;

    public delegate void UiAction(bool isPlayer1);
    public static event UiAction playerJoined;
    public static event UiAction playerLeft;
    public static event UiAction playerReady;

    public static void OnPlayerJoined(bool isPlayer1) {
        if (debugMessages) {
            Debug.Log("Events triggered: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        if (playerJoined != null) {
            playerJoined(isPlayer1);
        }
    }

    public static void OnPlayerLeft(bool isPlayer1) {
        if (debugMessages) {
            Debug.Log("Events triggered: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        if (playerLeft != null) {
            playerLeft(isPlayer1);
        }
    }

    public static void OnPlayerReady(bool isPlayer1) {
        if (debugMessages) {
            Debug.Log("Events triggered: " + System.Reflection.MethodBase.GetCurrentMethod().Name);
        }

        if (playerReady != null) {
            playerReady(isPlayer1);
        }
    }
}