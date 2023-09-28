using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : EnemyAttackSOBase
{
    [SerializeField] public Rigidbody2D bulletPrefab;

    private float _timer;
    [SerializeField] private float _timeBetweenShots = 3f;
    [SerializeField] private float bulletSpeed;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        enemy.MoveEnemy(Vector2.zero);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        enemy.MoveEnemy(Vector2.zero);

        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;

            Shoot();
        }

        _timer += Time.deltaTime;
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private void Shoot()
    {
        Vector2 direction = (playerTransform.position - enemy.transform.position).normalized;
        Rigidbody2D bullet = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.identity);
        bullet.velocity = direction * bulletSpeed;
    }
}
