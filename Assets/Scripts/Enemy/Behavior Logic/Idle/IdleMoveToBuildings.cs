using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Idle-MoveToBuildings", menuName = "Enemy Logic/Idle Logic/Move To Buildings")]
public class IdleMoveToBuildings : EnemyIdleSOBase
{
    private Transform target1;

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

        GetTarget();

        enemy.MoveEnemyFloat((target1.position - enemy.transform.position).normalized * enemy.baseSpeed * enemy.slowAmount);
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

    private void GetTarget()
    {
        float closestBuildingDistance = Mathf.Infinity;

        // Loop through all building transforms to find the closest one
        foreach (Transform buildingTransform in buildingTransforms)
        {
            float distance = Vector3.Distance(transform.position, buildingTransform.position);
            if (distance < closestBuildingDistance && buildingTransform.gameObject.activeInHierarchy)
            {
                closestBuildingDistance = distance;

                if (buildingTransform != null)
                {
                    target1 = buildingTransform;
                }

            }
        }
    }
}
