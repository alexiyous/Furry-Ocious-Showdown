using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHit : MonoBehaviour
{
    [SerializeField] private float hitRate;

    [SerializeField] private int damage = 30;
    [SerializeField] private int armorPenetration = 20;

    [SerializeField] private GameObject hitEffect;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(timer > hitRate)
        {
            timer = 0;
        }

        timer += Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(timer > hitRate)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);

                ObjectPoolManager.SpawnObject(hitEffect, collision.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            }
        }
         
    }
}
