using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class LightningEffect : MonoBehaviour
{
    private Light2D _light2D;

    public float minStrikeDelay = 10;
    public float maxStrikeDelay = 30;

    public float minFlash = 1;
    public float maxFlash = 3;

    public float minThunderDelay;
    public float maxThunderDelay = 1;

    public float minLightningLingerFrames = 2;
    public float maxLightningLingerFrames = 10;

    public float minLightningNextFlashDelayFrames = 3;
    public float maxLightningNextFlashDelayFrames = 5;

    public float lightningIntensity = 10;
    public float lightningFadeDuration = 1f;

    public Ease lightningEase = Ease.InOutSine;

    private void OnEnable()
    {
        _light2D = GetComponent<Light2D>();
        _light2D.enabled = false;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        float thunderDelay = Random.Range(minThunderDelay, maxThunderDelay);

        while(enabled)
        {
            yield return new WaitForSeconds(Random.Range(minStrikeDelay, maxStrikeDelay));

            float flashes = Random.Range(minFlash, maxFlash);
            for(int i = 0; i < flashes;i++)
            {
                _light2D.enabled = true;
                _light2D.intensity = lightningIntensity;
                float lingerFrames = Random.Range(minLightningLingerFrames, maxLightningLingerFrames);

                for(int j = 0; j < lingerFrames;j++)
                {
                    yield return null;
                }

                _light2D.enabled = false;

                int nextFlashDelayFrames = Random.Range((int)minLightningNextFlashDelayFrames, (int)maxLightningNextFlashDelayFrames);

                for (int j = 0; j < nextFlashDelayFrames; j++)
                {
                    yield return null;
                }

                
            }
            _light2D.enabled = true;

            DOTween.To(() => _light2D.intensity, x => _light2D.intensity = x, 0, lightningFadeDuration).SetEase(lightningEase).OnComplete(() => { 
            
                _light2D.enabled = false;
            });

        }
    }
}
