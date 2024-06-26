using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarHit : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int armorPenetration;
    
    public GameObject explosionEffect;


    private void OnEnable()
    {
        ObjectPoolManager.SpawnObject(explosionEffect, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            

            collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);
           

            /*ObjectPoolManager.ReturnObjectPool(gameObject);*/
        }
    }
}
