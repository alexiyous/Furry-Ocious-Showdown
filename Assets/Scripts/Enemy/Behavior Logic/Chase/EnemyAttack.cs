using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Show & Player Attack", menuName = "Enemy Logic/Chase Logic/Show Enemy-Player Aim")]

public class EnemyAttack : EnemyChaseSOBase
{
    [SerializeField] public Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed;
    private float _timer;
    [SerializeField] private float _timeBetweenShots = 3f;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.enemyCollider.enabled = true;
        enemy.animator.Play("Show");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();
        enemy.CheckForDirection((playerTransform.position - enemy.transform.position).normalized);

        if(_timer > (_timeBetweenShots - .9f))
        {
            enemy.animator.Play("Attack");
        }

        if(_timer > _timeBetweenShots)
        {
            _timer = 0;

            Shoot();

            enemy.animator.Play("Chase");
        }
        _timer += Time.deltaTime * enemy.slowAmount;

        if (!enemy.isAggroed)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
        enemy.CheckSlowEffect();
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

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the bullet to face the player's position
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        bullet.velocity = direction * bulletSpeed;
    }
}
