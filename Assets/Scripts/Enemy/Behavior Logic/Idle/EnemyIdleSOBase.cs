using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyIdleSOBase : ScriptableObject
{
    protected Enemy enemy;
    protected Transform transform;
    protected GameObject gameObject;

    protected Transform playerTransform;
    protected Transform[] buildingTransforms;
    protected Transform target;

    public virtual void Initialize(GameObject gameObject, Enemy enemy)
    {
        this.gameObject = gameObject;
        transform = gameObject.transform;
        this.enemy = enemy;

        target = GameObject.FindGameObjectWithTag("Enemy Target").transform;

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        buildingTransforms = GameObject.FindGameObjectsWithTag("Building")
                            .Select(building => building.transform)
                            .ToArray();
    }

    public virtual void DoEnterLogic() { }
    public virtual void DoExitLogic() { ResetValues(); }
    public virtual void DoFrameUpdateLogic() {
        if (enemy.isAggroed)
        {
            enemy.stateMachine.ChangeState(enemy.chaseState);
        }
    }
    public virtual void DoPhysicsLogic() { }
    public virtual void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType) { }
    public virtual void ResetValues() { }

}

