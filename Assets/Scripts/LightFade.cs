using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightFade : MonoBehaviour
{
    public float fadeDuration = 2f;
    public float targetIntensity = 0f;
    public float targetFalloff = 0f;
    public float targetRadius = 0f;

    private Light2D light2D;

    private float initialIntensity;
    private float initialFalloff;
    private float initialRadius;


    private void Start()
    {
        light2D = GetComponent<Light2D>();
        initialIntensity = light2D.intensity;
        initialFalloff = light2D.falloffIntensity;
        initialRadius = light2D.pointLightOuterRadius;
    }
    private void OnEnable()
    {
        // Create a sequence of animations using DOTween
        Sequence fadeSequence = DOTween.Sequence();

        // Store the initial intensity and falloff strength

        // Fade the intensity
        fadeSequence.Append(DOTween.To(() => initialIntensity, x => light2D.intensity = x, targetIntensity, fadeDuration));

        // Fade the falloff strength
        fadeSequence.Join(DOTween.To(() => initialFalloff, x => light2D.falloffIntensity = x, targetFalloff, fadeDuration));
        /*fadeSequence.Join(DOTween.To(() => initialRadius, x => light2D.pointLightOuterRadius = x, targetRadius, fadeDuration));*/

        // Optional: Add a callback when the fading is complete
        fadeSequence.OnComplete(FadeComplete);

        // Play the animation sequence
        fadeSequence.Play();
    }

    private void FadeComplete()
    {
        // This method is called when the fading is complete
        /*Debug.Log("Light fading complete.");*/
    }
}
