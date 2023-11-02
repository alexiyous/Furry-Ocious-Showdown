using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    public Vector3 originalTransform = new Vector3(1f, 1f, 1f);

    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);

        
        transform.DOScale(originalTransform, 1f).SetEase(Ease.OutBack).SetUpdate(true);
    }

}
