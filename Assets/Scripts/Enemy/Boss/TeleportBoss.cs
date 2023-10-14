using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class TeleportBoss : ActionNode
{
    public int teleportPointIndex;

    protected override void OnStart() {
        context.attackHelicopter.transform.position = context.attackHelicopter.teleportPoints[teleportPointIndex].position;
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
