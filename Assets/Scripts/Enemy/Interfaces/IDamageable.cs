using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(int damageAmount, int armorPenetration);

    void Die();

    int maxHealth { get; set; }

    int currentHealth { get; set; }
}
