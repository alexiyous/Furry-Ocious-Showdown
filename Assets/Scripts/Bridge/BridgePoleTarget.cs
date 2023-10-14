using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgePoleTarget : MonoBehaviour, IDamageable
{
    BridgePoleHealth bridgePoleHealth;

    public float maxHealth { get => ((IDamageable)bridgePoleHealth).maxHealth; set => ((IDamageable)bridgePoleHealth).maxHealth = value; }
    public float currentHealth { get => ((IDamageable)bridgePoleHealth).currentHealth; set => ((IDamageable)bridgePoleHealth).currentHealth = value; }

    public void Damage(int damageAmount, int armorPenetration)
    {
        ((IDamageable)bridgePoleHealth).Damage(damageAmount, armorPenetration);
    }

    public void Die()
    {
        ((IDamageable)bridgePoleHealth).Die();
    }

    // Start is called before the first frame update
    void Start()
    {
        bridgePoleHealth = GetComponentInParent<BridgePoleHealth>();
    }


}
