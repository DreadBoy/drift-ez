#if UNITY_EDITOR 
using UnityEditor;
#endif
using UnityEngine;
using System.Linq;

public class SceneSetup : MonoBehaviour {
    public Car carPrefab;
    public AudioClip musicLoop;

    void Start() {
        new GameObject().AddComponent<AudioListener>().name = "Audio listener";

        AudioSource audioSource = new GameObject().AddComponent<AudioSource>();
        audioSource.clip = musicLoop;
        audioSource.Play();

        CarPlaceholder[] placeholders = FindObjectsOfType<CarPlaceholder>();
        if (placeholders.Length != 2) {
            Debug.LogError("You need to place 2 car placeholders in scene!");
            return;
        }
        int playerCount = 2;
        ControllerManager controllerManager = FindObjectOfType<ControllerManager>();
        if (controllerManager == null || controllerManager.GetPlayer2InputDevice() == null) {
            playerCount = 1;
        }

        Debug.LogError("Player count: " + playerCount);

        for (int i = 0; i < playerCount; i++) {
            Debug.LogError("Creating car " + i); ;
            Car car = Instantiate(carPrefab);
            car.transform.position = placeholders[i].transform.position;
            car.transform.rotation = placeholders[i].transform.rotation;
            car.GetComponentInChildren<Camera>().rect = new Rect(i * 1f / playerCount, 0, 1f / playerCount, 1);
            if (controllerManager != null) {
                car.controller.SetInputDevice(controllerManager.GetInputDeviceForPlayer(i));
                car.controller.SetPlayer(i == 0);
                car.playerIndex = i;
            }
        }

        foreach (var placeholder in placeholders)
            Destroy(placeholder.gameObject);
    }

#if UNITY_EDITOR
    [MenuItem("GameObject/Create Other/Scene Setup", false, 10)]
    static void Create() {
        new GameObject().AddComponent<SceneSetup>().name = "Scene setup";
    }
#endif
}
