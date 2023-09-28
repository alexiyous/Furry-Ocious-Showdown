using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Player Attack", menuName = "Enemy Logic/Attack Logic/Player Aim")]

public class AimToPlayerProjectile : EnemyAttackSOBase
{
    private Transform _playerTransform;

    [SerializeField] private float _movementSpeed;

    [SerializeField] private float _exitTimer;
    [SerializeField] private float _timeTillExit = 3f;
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

        Vector2 moveDirection = (playerTransform.position - enemy.transform.position).normalized;

        enemy.MoveEnemy(moveDirection * _movementSpeed);

        if (!enemy.isWithinAttackingRadius)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                enemy.stateMachine.ChangeState(enemy.chaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }
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
}
