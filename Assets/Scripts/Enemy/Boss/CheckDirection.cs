using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckDirection : ActionNode
{
    public Vector2 velocity;

    protected override void OnStart()
    {

    }
    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        context.enemy.CheckForDirection(velocity);

        return State.Success;

    }
}
