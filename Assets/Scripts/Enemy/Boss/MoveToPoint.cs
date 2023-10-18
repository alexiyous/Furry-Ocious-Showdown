using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MoveToPoint : ActionNode
{
    public float newSpeed;
    public int pointIndex;

    protected override void OnStart()
    {
        context.attackHelicopter.baseSpeed = newSpeed;
    }
    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {

        Vector2 moveDirection = (context.attackHelicopter.movePoints[pointIndex].position - context.attackHelicopter.transform.position).normalized;

        context.attackHelicopter.MoveEnemyNoTurn(moveDirection * context.attackHelicopter.baseSpeed * context.attackHelicopter.slowAmount);

        if ((context.attackHelicopter.transform.position - context.attackHelicopter.movePoints[pointIndex].position).sqrMagnitude < 0.01f)
        {
            context.attackHelicopter.baseSpeed = 0;
        }

        return State.Success;

    }
}
