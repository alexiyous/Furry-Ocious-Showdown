using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Sirenix.OdinInspector;

public class ShootToTarget : ActionNode
{
    [SerializeField] private float fireRate = .5f;
    [SerializeField] private int bulletCount = 6;
    [SerializeField] private float inaccuracy = 1f;
    [InlineEditor][SerializeField] private GameObject bulletPrefab;

    private Transform target;

    public bool isAimed = true;

    [SerializeField] private StartPoint startPoint;

    public enum StartPoint
    {
        MachineGun,
        MissileLauncher
    }

    protected override void OnStart()
    {
        target = context.attackHelicopter.playerTransform;



        context.attackHelicopter.StartCoroutine(ShootBullets());
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }

    private IEnumerator ShootBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 direction;
            GameObject bullet;
            float angle;

            Vector3 shootInaccuracy = new Vector3(0, Random.Range(-inaccuracy, inaccuracy), 0);

            switch (startPoint)
            {
                case StartPoint.MachineGun:
                    direction = (target.position - context.attackHelicopter.shootPoint1.position + shootInaccuracy).normalized;

                    bullet = ObjectPoolManager.SpawnObject(bulletPrefab, context.attackHelicopter.shootPoint1.position, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

                    angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    if (isAimed)
                    {
                        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    }
                    break;
                case StartPoint.MissileLauncher:
                    direction = (target.position - context.attackHelicopter.shootPoint2.position + shootInaccuracy).normalized;

                    bullet = ObjectPoolManager.SpawnObject(bulletPrefab, context.attackHelicopter.shootPoint2.position, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

                    angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    if (isAimed)
                    {
                        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
                    }
                    break;
            }
            // Rotate the bullet to face the player's position

            yield return new WaitForSeconds(fireRate);
        }
    }
}
