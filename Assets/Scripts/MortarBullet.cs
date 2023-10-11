using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBullet : TowerBullet
{
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
}
