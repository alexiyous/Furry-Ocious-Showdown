using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ParticleAfterTimer : MonoBehaviour
{

    private ParticleSystem _particleSystem;

    public float afterTimer;

    private float timer;

    public short initialBurst;
    public float initialRate;

    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        var emissionModule = _particleSystem.emission;

    }

    private void OnEnable()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        var emissionModule = _particleSystem.emission;

        emissionModule.SetBurst(0, new ParticleSystem.Burst(0.0f, initialBurst, initialBurst, 1, 0.01f));
        emissionModule.rateOverTime = initialRate;

        timer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var emissionModule = _particleSystem.emission;
        if (timer > afterTimer)
        {
            timer = 0f;
            emissionModule.SetBurst(0, new ParticleSystem.Burst(0.0f, 0, 0, 1, 0.01f));
            emissionModule.rateOverTime = 0;
        }

        timer += Time.deltaTime;
    }
}
