using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBullet : TowerBullet
{
    public float rotateSpeed = 200f;


    public override void Move()
    {
        if (!target) return;

        switch (bulletLevel)
        {
            case BulletUpgrade.Level1:
                rb.velocity = transform.position * bulletSpeed;
                break;
            case BulletUpgrade.Level2:
                direction = (target.position - transform.position).normalized;
                float rotateAmount = Vector3.Cross(direction, transform.up).z;
                rb.angularVelocity = -rotateAmount * rotateSpeed;
                rb.velocity = transform.up * bulletSpeed * 1.5f;
                break;
            case BulletUpgrade.Level3:
                damage = damage * 1.5f;
                direction = (target.position - transform.position).normalized;
                float rotateAmount1 = Vector3.Cross(direction, transform.up).z;
                rb.angularVelocity = -rotateAmount1 * rotateSpeed;
                rb.velocity = transform.up * bulletSpeed * 1.5f;
                break;
        }
    }
}
