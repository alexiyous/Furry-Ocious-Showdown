using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Start Player Chase", menuName = "Enemy Logic/Chase Logic/Start Player Chase")]
public class ChaseStartOnly : EnemyChaseSOBase
{
    Vector2 moveDirection;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        moveDirection = playerTransform.position.x > enemy.transform.position.x ? Vector2.right : Vector2.left;
        enemy.MoveEnemy(moveDirection * enemy.slowAmount);
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

        enemy.CheckSlowEffect();

        enemy.MoveEnemy(moveDirection * enemy.baseSpeed * enemy.slowAmount);

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
