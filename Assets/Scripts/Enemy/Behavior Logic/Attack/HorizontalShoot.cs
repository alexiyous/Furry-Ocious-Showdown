using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Horizontal Shoot", menuName = "Enemy Logic/Attack Logic/Horizontal Shoot")]

public class HorizontalShoot : EnemyAttackSOBase
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

            enemy.animator.Play("Tank Idle");
        }

        _timer += Time.deltaTime * enemy.slowAmount;
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        enemy.CheckForDirection(playerTransform.position.x > enemy.transform.position.x ? Vector2.right : Vector2.left);

        enemy.CheckSlowEffect();

        if(_timer > _timeBetweenShots - 1)
        {
            enemy.animator.Play("Tank Attack");
        }
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
        float xOffset = enemy.isFacingRight ? 1f : -1f;
        Vector3 offset = new Vector3(xOffset * 2f, 0f, 0f);

        Vector2 direction = playerTransform.position.x > enemy.transform.position.x ? Vector2.right : Vector2.left;
        Rigidbody2D bullet = Instantiate(bulletPrefab, enemy.transform.position + offset, Quaternion.identity);
        bullet.velocity = direction * bulletSpeed;
    }
}
