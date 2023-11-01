using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterTimerNoPause : EnableAfterTimer
{
    public override IEnumerator EnableAfter(float time)
    {
        if (objectToEnable == null)
        {
            Debug.LogWarning("No object to enable!");
            yield break;
        }

        PauseHandler.ableToPause = false;
        yield return new WaitForSeconds(time);
        PauseHandler.ableToPause = true;
        objectToEnable.SetActive(true);
    }
}
