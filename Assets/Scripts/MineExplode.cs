using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplode : MonoBehaviour
{
    public GameObject explosionEffect;
    [SerializeField] private int damage;
    [SerializeField] private int armorPenetration;

    public int soundToPlay = 23;

    // Start is called before the first frame update
    void OnEnable()
    {
        AudioManager.instance.PlaySFXAdjusted(soundToPlay);

        ObjectPoolManager.SpawnObject(explosionEffect, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);

            
        }
    }
}
