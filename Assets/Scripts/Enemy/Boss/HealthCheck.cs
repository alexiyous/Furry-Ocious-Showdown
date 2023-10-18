using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class HealthCheck : ActionNode
{
    public float HealthBelowPercentage;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        if(context.enemy.currentHealth <= context.enemy.maxHealth * HealthBelowPercentage)
        {
            return State.Success;
        }
        else
        {
            return State.Failure;
        }

    }
}
