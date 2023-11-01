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
                damage = damage2;
                armor = armor2;
                rb.velocity = transform.up * bulletSpeed * 1.5f;
                break;
            case LevelUpgrade.Level3:
                damage = damage3;
                armor = armor3;
                rb.velocity = transform.up * bulletSpeed * 1.5f;
                break;
        }
    }
}
