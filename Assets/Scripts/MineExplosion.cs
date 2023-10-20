using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    public string type = "Projectile";

    [SerializeField] private GameObject explosionPrefab;

    private bool isHit;

    private void OnEnable()
    {
        isHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !isHit)
        {
            isHit = true;
            ObjectPoolManager.SpawnObject(explosionPrefab, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

            /*ObjectPoolManager.ReturnObjectPool(gameObject);*/

            Destroy(gameObject);
        }

    }
}
