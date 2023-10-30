using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableSound : MonoBehaviour
{
    public int soundToPlay;

    // Start is called before the first frame update
    private void OnEnable()
    {
        AudioManager.instance.PlaySFXAdjusted(soundToPlay);
    }
}
