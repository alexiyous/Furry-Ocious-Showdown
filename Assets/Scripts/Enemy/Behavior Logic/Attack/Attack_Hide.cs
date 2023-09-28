using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Hide Enemy", menuName = "Enemy Logic/Attack Logic/Hide Enemy")]
public class Attack_Hide : EnemyAttackSOBase
{

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.enemyCollider.enabled = false;
        enemy.animator.Play("Hide");
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (!enemy.isWithinAttackingRadius)
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
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
}
