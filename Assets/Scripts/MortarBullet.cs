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
        if (this == null || !gameObject.activeInHierarchy) yield break; // Check if the bullet exists before continuing

        yield return new WaitForSeconds(timeToHit + 0.5f); // Wait for the calculated time + 0.5 seconds

        if (isTargeting)
        {
            if (gameObject != null) Destroy(gameObject); // Destroy the game object only if it's still targeting
        }
    }

    public override void OnBecameInvisible()
    {
        gameObject.SetActive(false);
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
