using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartRotate : MonoBehaviour
{
    public Vector3 rotation = new Vector3(0,0,20f);
    public float duration = 3f;

    public Vector3 movement = new Vector3(-1f, 0 , 0);
    public float moveDuration = 10f;

    private void OnEnable()
    {
        transform.DOLocalRotate(rotation, duration).SetEase(Ease.InQuad);
        transform.DOLocalMove(movement, moveDuration).SetEase(Ease.InQuad);
    }
}
