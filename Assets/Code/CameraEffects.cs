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

        camera.fieldOfView = 60 - car.speed / 5;

        var settings = postProcessingBehaviour.profile.chromaticAberration.settings;
        settings.intensity = car.speed / 100f;
        postProcessingBehaviour.profile.chromaticAberration.settings = settings;

        var vignette = postProcessingBehaviour.profile.vignette.settings;
        if (car.acceleration < 0 && car.controller.GetThrottle() > 0)
            vignette.intensity = 0.636F;
        else
            vignette.intensity = 0f;
        postProcessingBehaviour.profile.vignette.settings = vignette;
    }
}