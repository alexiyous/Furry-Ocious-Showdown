using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightManager : MonoBehaviour
{
    private Light2D[] lights;

    private void Awake()
    {
        lights = GetComponentsInChildren<Light2D>(true);

        if(!GameManager.instance.isNight)
        {
            foreach (Light2D light in lights)
            {
                light.enabled = false;
            }
        }
    }
}
