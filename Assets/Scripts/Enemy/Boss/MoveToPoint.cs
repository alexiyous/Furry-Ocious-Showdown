using DG.Tweening;
using TheKiwiCoder;
using UnityEngine;

public class MoveToPoint : ActionNode
{
    public float startSpeed;
    public float newSpeed;
    public int pointIndex;
    public float timeToNewSpeed;

    private Tweener speedTween;
    public Ease moveEase = Ease.Linear;

    protected override void OnStart()
    {
        context.attackHelicopter.pointIndex = pointIndex;
        context.attackHelicopter.baseSpeed = startSpeed;

        // Stop any previous speed tweens if they exist
        if (speedTween != null && speedTween.IsActive())
        {
            speedTween.Kill();
        }

        // Tween the baseSpeed property
        speedTween = DOTween.To(
            () => context.attackHelicopter.baseSpeed,
            x => context.attackHelicopter.baseSpeed = x,
            newSpeed,
            timeToNewSpeed
        ).SetEase(moveEase);
    }
    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {

        Vector2 moveDirection = (context.attackHelicopter.movePoints[pointIndex].position - context.attackHelicopter.transform.position).normalized;

        context.attackHelicopter.MoveEnemyNoTurn(moveDirection * context.attackHelicopter.baseSpeed);

        if ((context.attackHelicopter.transform.position - context.attackHelicopter.movePoints[pointIndex].position).sqrMagnitude < 0.01f)
        {
            context.attackHelicopter.baseSpeed = 0;
        }

        return State.Success;

    }
}
