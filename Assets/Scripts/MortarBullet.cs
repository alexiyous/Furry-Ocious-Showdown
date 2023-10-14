using System.Collections;
using UnityEngine;

public class MortarBullet : TowerBullet
{
    public float timeToHitTarget;
    public bool isTargeting;

    public override void Move()
    {
        if (!target) return;

        switch (bulletLevel)
        {
            case LevelUpgrade.Level2:
                damage *= 1.2f;
                bulletSpeed *= 1.5f;
                break;
            case LevelUpgrade.Level3:
                damage *= 1.5f;
                bulletSpeed *= 1.5f;
                break;
        }
    }

    public IEnumerator DestroyBulletIfMissed(float timeToHit)
    {
        yield return new WaitForSeconds(timeToHit + 0.5f); // Wait for the calculated time + 0.5 seconds

        if (!isTargeting) yield break; // If the bullet is not targeting, exit the coroutine
        if (this.gameObject == null) yield break; // If the bullet's game object is null, exit the coroutine
        Destroy(this.gameObject);

    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            isTargeting = false;
            Destroy(gameObject);
        }

        if (collision.CompareTag("Building"))
        {
            isTargeting = false;
            Destroy(gameObject);
        }
    }
}
