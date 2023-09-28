using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Chase-Player Chase", menuName = "Enemy Logic/Chase Logic/Player Chase")]

public class EnemyChaseToPlayer : EnemyChaseSOBase
{
    [SerializeField] private Vector3 offset = new Vector3(0f,0f,0f);

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.isHovering = false;
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 moveDirection = ((playerTransform.position + offset) - enemy.transform.position).normalized;

        enemy.CheckSlowEffect();

        enemy.MoveEnemyFloat(moveDirection * enemy.baseSpeed * enemy.slowAmount);

        if (enemy.isWithinAttackingRadius)
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
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
