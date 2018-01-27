using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour {

    [SerializeField]
    private GameObject MainMenuPane;

    [SerializeField]
    private GameObject PlayerSelectPane;

    void Awake() {
        ShowMainMenu();
    }

    public void ShowMainMenu() {
        MainMenuPane.SetActive(true);
        PlayerSelectPane.SetActive(false);
    }

    public void ShowPlayerSelect() {
        MainMenuPane.SetActive(false);
        PlayerSelectPane.SetActive(true);
    }

    public void StartGame() {
        Debug.LogError("Trying to start game"); ;
        SceneManager.LoadScene(1);
    }
}
