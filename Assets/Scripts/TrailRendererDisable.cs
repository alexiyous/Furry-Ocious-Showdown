using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererDisable : MonoBehaviour
{
    TrailRenderer tr;
    private void Start()
    {
        tr = GetComponent<TrailRenderer>();
    }
    private void OnEnable()
    {
        StartCoroutine(ResetTrailRenderer(tr));
    }

    private void OnDisable()
    {
        
    }

    IEnumerator ResetTrailRenderer(TrailRenderer tr)
    {
        float trailTime = tr.time;
        tr.time = 0;
        yield return null;
        tr.time = trailTime;
    }
}
