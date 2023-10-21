using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle-MoveToTarget", menuName = "Enemy Logic/Idle Logic/Move To Target")]

public class IdleMoveToTarget : EnemyIdleSOBase
{
    public float randomMovementSpeedOffset = 1;
    public float targetOffsetY = 3;
    Vector3 targetOffset;
    float movementSpeed;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.Play("run");
        movementSpeed = Random.Range(enemy.baseSpeed - randomMovementSpeedOffset, enemy.baseSpeed + randomMovementSpeedOffset);
        targetOffset = new Vector3(0, Random.Range(-targetOffsetY, targetOffsetY), 0);

    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        Vector2 moveDirection = ((target.position + targetOffset) - enemy.transform.position).normalized;


        enemy.MoveEnemyFloat(moveDirection * movementSpeed * enemy.slowAmount);
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
