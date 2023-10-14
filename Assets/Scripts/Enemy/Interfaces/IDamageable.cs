using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(int damageAmount, int armorPenetration);

    void Die();

    float maxHealth { get; set; }

    float currentHealth { get; set; }
}