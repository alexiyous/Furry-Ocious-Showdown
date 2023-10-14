using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDestroyAfterTimer : MonoBehaviour
{
    private TrailRenderer _trailRenderer;

    private void Awake()
    {
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void OnEnable()
    {
        _trailRenderer.Clear();

    }
}
