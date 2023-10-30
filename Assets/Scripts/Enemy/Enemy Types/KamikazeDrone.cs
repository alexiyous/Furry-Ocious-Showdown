using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeDrone : Enemy
{
    /*public int soundToStop = 17;*/

    public override void Die()
    {
        base.Die();

        /*AudioManager.instance.StopSFX(soundToStop);*/
    }
}
