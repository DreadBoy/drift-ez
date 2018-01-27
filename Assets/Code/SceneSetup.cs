using UnityEditor;
using UnityEngine;

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

        ControllerManager controllerManager = FindObjectOfType<ControllerManager>();

        for (int i = 0; i < placeholders.Length; i++) {
            Car car = Instantiate(carPrefab);
            car.transform.position = placeholders[i].transform.position;
            car.transform.rotation = placeholders[i].transform.rotation;
            car.camera.rect = new Rect(i * 1f / placeholders.Length, 0, 1f / placeholders.Length, 1);
            if (controllerManager != null) {
                car.controller.SetInputDevice(controllerManager.GetInputDeviceForPlayer(i));
                car.controller.SetPlayer(i == 0);
                car.playerIndex = i;
            }
        };

        foreach (var placeholder in placeholders)
            Destroy(placeholder.gameObject);
    }

    [MenuItem("GameObject/Create Other/Scene Setup", false, 10)]
    static void Create() {
        new GameObject().AddComponent<SceneSetup>().name = "Scene setup";
    }
}
