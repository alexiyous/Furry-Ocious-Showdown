using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = -360f; // Degrees per second

    private void Start()
    {
        // Rotate the GameObject on the Z-axis infinitely
        transform.DORotate(new Vector3(0, 0, rotationSpeed), 1.0f, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear)
            .OnKill(() => ResetRotation()); // Reset rotation on completion
    }

    private void ResetRotation()
    {
        transform.DOKill(); // Kill the rotation animation
        transform.localRotation = Quaternion.identity; // Reset rotation to the initial state
    }
}
