using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraStartTransition : MonoBehaviour
{
    private CinemachineVirtualCamera transitionCamera;
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private Transform targetPosition;
    private Vector3 originPosition;

    public float moveDuration = 2.0f; // Duration of the camera movement.
    public Ease moveEaseType = Ease.OutQuad; // Easing type for the camera movement.

    // Start is called before the first frame update
    void Start()
    {
        transitionCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        originPosition = transitionCamera.gameObject.transform.position;
        MoveToTargetAndBack();
    }

    [Button]
    public void MoveToTargetAndBack()
    {
        // Move the camera to the target position.
        transitionCamera.transform.DOMove(targetPosition.position, moveDuration)
            .SetEase(moveEaseType)
            .OnComplete(ReturnToOriginalPosition);
    }

    private void ReturnToOriginalPosition()
    {
        // Move the camera back to the original position.
        transitionCamera.transform.DOMove(originPosition, moveDuration)
            .SetEase(moveEaseType).OnComplete(() =>
            {
                GameManager.instance.beginGame = false;
                mainCamera.enabled = true;
                gameObject.SetActive(false);
            });
        
        
    }
}
