using UnityEngine;
using UnityEngine.PostProcessing;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(PostProcessingBehaviour))]
[RequireComponent(typeof(ShaderEffect_BleedingColors))]
public class CameraEffects : MonoBehaviour
{

    new Camera camera;
    PostProcessingBehaviour postProcessingBehaviour;
    ShaderEffect_BleedingColors bleedingColors;
    [SerializeField]
    Car car;

    void Awake()
    {
        camera = GetComponent<Camera>();
        postProcessingBehaviour = GetComponent<PostProcessingBehaviour>();
        bleedingColors = GetComponent<ShaderEffect_BleedingColors>();
    }

    void Update()
    {
        if (car.isDrifting)
            bleedingColors.shift = -car.controller.GetSteering();
        else
            bleedingColors.shift = 0;

        camera.fieldOfView = 60 - 2 * car.speed;

        var settings = postProcessingBehaviour.profile.chromaticAberration.settings;
        settings.intensity = car.speed / 10f;
        postProcessingBehaviour.profile.chromaticAberration.settings = settings;
    }
}