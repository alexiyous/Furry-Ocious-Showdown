using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackHelicopter : Enemy
{
    public Transform shootPoint1;
    public Transform shootPoint2;

    public Transform[] movePoints;
    public Transform[] teleportPoints;

    public GameObject machineGun;

    public Transform playerTransform;

    public Transform[] buildingsTransform;

    public ParticleSystem _particleSystem;

    public int pointIndex = 0;

    private bool phase1 = false;
    private bool phase2 = false;
    private bool phase3 = false;

    public float shakeIntensity = 5f;
    public float shakeTime = .2f;

    public override void Start()
    {
        base.Start();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        buildingsTransform = GameObject.FindGameObjectsWithTag("Building")
                            .Select(building => building.transform)
                            .ToArray();
        AudioManager.instance.PlaySFX(8);
    }

    public override void Die()
    {
        base.Die();

        machineGun.SetActive(false);

        AudioManager.instance.StopSFX(8);

        CinemachineShake.instance.ShakeCamera(shakeIntensity, shakeTime);
    }

    public override void Damage(int damageAmount, int armorPenetration)
    {
        base.Damage(damageAmount, armorPenetration);

        AudioManager.instance.PlaySFXAdjusted(16);

        var mainModule = _particleSystem.main;
        var emissionModule = _particleSystem.emission;

        if (currentHealth <= maxHealth * 0.3f && !phase3)
        {
            phase3 = true;
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
                _particleSystem.Clear(); // Clear existing particles
            }

            mainModule.duration = 0.15f;
            emissionModule.SetBurst(0, new ParticleSystem.Burst(0.0f, 15, 15, 1, 0.01f));
            mainModule.startColor = new Color(0.2f, 0.2f, 0.2f, 1.0f);

            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
        }
        else if (currentHealth <= maxHealth * 0.5f && !phase2)
        {
            phase2 = true;
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
                _particleSystem.Clear(); // Clear existing particles
            }

            mainModule.duration = 0.3f;
            emissionModule.SetBurst(0, new ParticleSystem.Burst(0.0f, 10, 10, 1, 0.01f));
            mainModule.startColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);

            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
        }
        else if (currentHealth <= maxHealth * 0.8f && !phase1)
        {
            phase1 = true;
            if (_particleSystem.isPlaying)
            {
                _particleSystem.Stop();
                _particleSystem.Clear(); // Clear existing particles
            }

            mainModule.duration = 0.5f;
            emissionModule.SetBurst(0, new ParticleSystem.Burst(0.0f, 10, 10, 1, 0.01f));

            if (!_particleSystem.isPlaying)
            {
                _particleSystem.Play();
            }
        }
    }

    public override void Update()
    {
        Vector2 moveDirection = (movePoints[pointIndex].position - transform.position).normalized;

        MoveEnemyNoTurn(moveDirection * baseSpeed);
    }
}
