using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarTower : Tower
{
    private Vector2 Vo;

    public override void RotateTowardsTarget()
    {
        if (!target) return;

        Vo = CalculateVelocity(target.position, firingPoint.position, 1f);

        // Calculate the angle in radians between the current position and the target
        float angle = Mathf.Atan2(Vo.y, Vo.x) - 90f;

        // Convert the angle to degrees and set it as the Z rotation of turretRotationPoint
        turretRotationPoint.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
    }

    public override void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
        var rb = bulletObj.GetComponent<Rigidbody2D>();
        rb.velocity = Vo;
        TowerBullet bulletScript = bulletObj.GetComponent<TowerBullet>();
        bulletScript.SetTarget(target);
    }

    Vector2 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector2 distance = target - origin; // Changed from Vector3 to Vector2

        float Sx = distance.x;
        float Sy = distance.y;

        float Vx = Sx / time;
        float Vy = (Sy + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time) / time;

        return new Vector2(Vx, Vy);
    }

}