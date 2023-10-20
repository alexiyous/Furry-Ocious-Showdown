using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHit : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int armorPenetration;
    [SerializeField] private GameObject hitEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);
            ObjectPoolManager.SpawnObject(hitEffect, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);

            /*ObjectPoolManager.ReturnObjectPool(gameObject);*/
        }
    }
}
