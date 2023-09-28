using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Chase-Horizontal Player Chase", menuName = "Enemy Logic/Chase Logic/Horizontal Player Chase")]

public class HorizontalMove : EnemyChaseSOBase
{
    [SerializeField] private float _movementSpeed = 4f;

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

        Vector2 moveDirection = playerTransform.position.x > enemy.transform.position.x ? Vector2.right : Vector2.left;

        enemy.MoveEnemy(moveDirection * _movementSpeed);

        if (enemy.isWithinAttackingRadius)
        {
            enemy.stateMachine.ChangeState(enemy.attackState);
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
