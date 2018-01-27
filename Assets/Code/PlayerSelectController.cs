using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class PlayerSelectController : MonoBehaviour {

    [SerializeField]
    Animator Player1Animator;

    [SerializeField]
    Animator Player2Animator;

    bool player1Ready;
    bool player2Ready;

    void OnEnable() {
        Events.playerJoined += PlayerJoined;
        Events.playerLeft += PlayerLeft;
        Events.playerReady += PlayerReady;
    }

    void OnDisable() {
        Events.playerJoined -= PlayerJoined;
        Events.playerLeft -= PlayerLeft;
        Events.playerReady -= PlayerReady;
    }

    private void PlayerJoined(bool isPlayer1) {
        Animator animator = isPlayer1 ? Player1Animator : Player2Animator;
        animator.SetBool("PlayerJoined", true);
    }

    private void PlayerLeft(bool isPlayer1) {
        Animator animator = isPlayer1 ? Player1Animator : Player2Animator;
        animator.SetBool("PlayerJoined", false);
    }

    private void PlayerReady(bool isPlayer1) {
        if (isPlayer1) {
            player1Ready = true;
        } else {
            player2Ready = true;
        }
        if (player1Ready && player2Ready) {
            StartGame();
        }

    }

    public void StartGame() {
        Debug.Log("Starting game"); ;
        SceneManager.LoadScene(1);
    }
}
