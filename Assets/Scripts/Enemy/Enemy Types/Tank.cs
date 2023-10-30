using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    public float shakeIntensity = 5f;
    public float shakeTime = .2f;

    public int lowHitSound = 16;
    public int highHitSound = 25;

    public int aliveSound;
    public override void Damage(int damageAmount, int armorPenetration)
    {
        float damage = damageAmount * ((float)armorPenetration / (float)armor);

        if (damage < 1)
        {
            damage = 1;
        }

        

        if (armorPenetration > armor)
        {
            damage = damageAmount;
        }

        if(damage <= 50)
        {
            AudioManager.instance.PlaySFXAdjusted(lowHitSound);
        } else if (damage > 50)
        {
            AudioManager.instance.PlaySFXAdjusted(highHitSound);
        }

        StartCoroutine(ChangeColor(new Color(1, 0.6f, 0.6f)));

        currentHealth -= (int)damage;
        //AudioManager.instance.PlaySFXAdjusted(13);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();

        CinemachineShake.instance.ShakeCamera(shakeIntensity, shakeTime);

        AudioManager.instance.StopSFX(aliveSound);
    }

    public override void Start()
    {
        base.Start();

        AudioManager.instance.PlaySFXAdjusted(aliveSound);
    }
}
