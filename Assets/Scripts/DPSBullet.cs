using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPSBullet : TowerBullet
{
    public override void Move()
    {
        if (!target) return;

        switch (bulletLevel)
        {
            case LevelUpgrade.Level1:
                rb.velocity = transform.up * bulletSpeed;
                break;
            case LevelUpgrade.Level2:
                damage *= 1.2f;
                rb.velocity = transform.up * bulletSpeed * 1.5f;
                break;
            case LevelUpgrade.Level3:
                damage *= 1.5f;
                rb.velocity = transform.position * bulletSpeed * 1.5f;
                break;
        }
    }
}
