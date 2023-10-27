using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiercingDamageProjectile : MonoBehaviour
{
    [SerializeField] private int damage = 30;
    [SerializeField] private int armorPenetration = 0;
    private int count;
    [SerializeField] private int piercePower = 1;
    [SerializeField] private GameObject hitEffect;

    private void OnEnable()
    {
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);
            ObjectPoolManager.SpawnObject(hitEffect, transform.position, Quaternion.identity);

            count++;
            
            if (count >= piercePower)
            {

                Destroy(gameObject);
            }
        }
    }
}
