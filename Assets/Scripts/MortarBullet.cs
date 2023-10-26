using DG.Tweening;
using System.Collections;
using UnityEngine;


public class MortarBullet : TowerBullet
{
    public float timeToHitTarget;
    public bool isTargeting;
    public CircleCollider2D hitZone;
    public float bombCollide;
    public float hitZone2;
    public float hitZone3;

    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject explosion3;

    private GameObject explosionToUse;

    public override void Move()
    {
        if (!target) return;

        switch (bulletLevel)
        {
            case LevelUpgrade.Level1:
                explosionToUse = explosion1;
                break;
            case LevelUpgrade.Level2:
                explosionToUse = explosion2;
                damage = damage2;
                armor = armor2;
                /*bombCollide = hitZone2;*/
                break;
            case LevelUpgrade.Level3:
                explosionToUse = explosion3;
                damage = damage3;
                armor = armor3;
                /*bombCollide = hitZone3;*/
                break;
        }
    }

    public IEnumerator DestroyBulletIfMissed(float timeToHit)
    {
        if (this == null || !gameObject.activeInHierarchy) yield break; // Check if the bullet exists before continuing

        yield return new WaitForSeconds(timeToHit + 0.5f); // Wait for the calculated time + 0.5 seconds

        if (isTargeting)
        {
            if (gameObject != null)
            {
                /*hitZone.radius = bombCollide;*/
                ObjectPoolManager.SpawnObject(explosionToUse, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
                Destroy(gameObject, 0.2f); // Destroy the game object after timeToHit seconds
            }
        }
    }

    public override void OnBecameInvisible()
    {
        /*gameObject.SetActive(false);*/
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isTargeting = false;
            /*hitZone.radius = bombCollide;*/
            collision.GetComponent<IDamageable>().Damage(damage, armor);
            ObjectPoolManager.SpawnObject(explosionToUse,transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            Destroy(gameObject);
        }
    }
}
