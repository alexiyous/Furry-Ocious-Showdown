using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy
{
    public float shakeIntensity = 5f;
    public float shakeTime = .2f;

    public override void Die()
    {
        base.Die();

        CinemachineShake.instance.ShakeCamera(shakeIntensity, shakeTime);
    }
}
