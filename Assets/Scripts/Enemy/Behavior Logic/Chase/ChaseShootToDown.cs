using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Shoot To Down", menuName = "Enemy Logic/Chase Logic/Shoot To Down")]
public class ChaseShootToDown : EnemyChaseSOBase
{
    [SerializeField] private float reloadTime = 2f;
    private float timer;

    [SerializeField] private float inaccuracy = .1f;

    [SerializeField] private GameObject bulletPrefab;
    private bool isShooting = false;

    [SerializeField] public float randomMovementRange = 1f;
    private Vector3 _targetPos;
    private Vector3 _direction;

    [SerializeField] private float _exitTimer;
    [SerializeField] private float _timeTillExit = 2f;

    public int soundToPlay;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        enemy.MoveEnemyFloat(Vector2.zero);
        enemy.RB.Sleep();

        timer = 1f;

        _targetPos = GetRandomPointInCircle();
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        if(timer < reloadTime)
        {
            enemy.MoveEnemyFloat(Vector2.zero);
        }

        _direction = (_targetPos - enemy.transform.position).normalized;

        if (timer > reloadTime && !isShooting)
        {
            Shoot();

            _targetPos = GetRandomPointInCircle();

            enemy.MoveEnemyFloat(_direction * enemy.baseSpeed * enemy.slowAmount * .5f);

            
        }

        if(timer > reloadTime + 1f)
        {
            timer = 0f;

            enemy.MoveEnemyFloat(Vector2.zero);
            enemy.RB.Sleep();

            isShooting = false;
        }

        timer += Time.deltaTime * enemy.slowAmount;

        if (!enemy.isAggroed)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                enemy.stateMachine.ChangeState(enemy.idleState);
            }
        }
        else
        {
            _exitTimer = 0f;
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
        isShooting = true;

        AudioManager.instance.PlaySFXAdjusted(soundToPlay);

        Vector3 shootInaccuracy = new Vector3(Random.Range(-inaccuracy, inaccuracy), 0, 0);

        Vector2 direction = (Vector3.down + shootInaccuracy).normalized;

        GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)Random.insideUnitCircle * randomMovementRange + new  Vector3(0, 1f, 0);
    }
}
