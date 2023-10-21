using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using DG.Tweening;

public class EllipseMove : ActionNode
{
    public Transform center; // The center of the ellipse
    public float radiusX = 6.0f; // X-axis radius
    public float radiusY = 2.0f; // Y-axis radius
    public float speed = 2.0f; // Movement speed

    private Transform thisTransform;
    private Vector3 centerPosition;
    private float currentAngle = 0;

    protected override void OnStart()
    {
        thisTransform = context.transform;
        centerPosition = thisTransform.position;

        // Use DOTween to move the GameObject in an ellipse
        DOTween.To(() => currentAngle, x => currentAngle = x, 360, 2 * Mathf.PI * radiusX / speed)
            .OnUpdate(() =>
            {
                float x = centerPosition.x + Mathf.Cos(currentAngle) * radiusX;
                float y = centerPosition.y + Mathf.Sin(currentAngle) * radiusY;

                thisTransform.position = new Vector3(x, y, thisTransform.position.z);
            })
            .SetEase(Ease.Linear)
            .SetLoops(-1); // Loop the animation indefinitely
    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        return State.Success;
    }

}
