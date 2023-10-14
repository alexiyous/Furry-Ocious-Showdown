using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class ChangeHeliSpeed : ActionNode
{
    public float newSpeed;

    protected override void OnStart() {
        context.attackHelicopter.baseSpeed = newSpeed;

    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
