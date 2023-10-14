using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MachinegunTower : Tower
{
    public Transform[] firingPoints;

    public override void Shoot()
    {
        int randomIndex = Random.Range(0, firingPoints.Length);
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoints[randomIndex].position, turretRotationPoint.rotation);
        TowerBullet bulletScript = bulletObj.GetComponent<TowerBullet>();
        bulletScript.SetTarget(target);
    }
}
