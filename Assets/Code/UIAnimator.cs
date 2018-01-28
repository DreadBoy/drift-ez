using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour {

    [SerializeField]
    Animator Player1Animator;

    [SerializeField]
    Animator Player2Animator;

    [SerializeField]
    Animator ControlsAnimator;


    void OnEnable() {
        Events.playerJoined += PlayerJoined;
        Events.playerLeft += PlayerLeft;
    }

    void OnDisable() {
        Events.playerJoined -= PlayerJoined;
        Events.playerLeft -= PlayerLeft;
    }

    private void PlayerJoined(bool isPlayer1) {
        Animator animator = isPlayer1 ? Player1Animator : Player2Animator;
        animator.SetBool("playerJoined", true);
    }

    private void PlayerLeft(bool isPlayer1) {
        Animator animator = isPlayer1 ? Player1Animator : Player2Animator;
        animator.SetBool("playerJoined", false);
    }

    public void OpenControls() {
        ControlsAnimator.SetBool("open", true); ;
    }

    public void CloseControls() {
        ControlsAnimator.SetBool("open", false);
    }
}
