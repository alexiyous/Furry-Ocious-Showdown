using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler, IPointerClickHandler
{
    public float animationDuration = .1f;

    public float scaleMultiplier = 1.05f;

    public GameObject selector;

    private Vector3 originalScale;

    public int clickSound = 36;

    public virtual void Start()
    {
        originalScale = transform.localScale;
    }

    public virtual void OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(originalScale, animationDuration).SetUpdate(true);

        if (selector == null) return;

        if (selector.activeInHierarchy == true)
        {
            selector.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventData.selectedObject = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventData.selectedObject = null;
    }

    public virtual void OnSelect(BaseEventData eventData)
    {
        AudioManager.instance.PlaySFXAdjusted(35);

        transform.DOScale(originalScale * scaleMultiplier, animationDuration).SetUpdate(true);

        if(selector == null) return;

        if (selector.activeInHierarchy == false)
        {
            selector.SetActive(true);
        }
        /*AudioManager.instance.PlaySFXAdjusted(1);*/
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.instance.PlaySFXAdjusted(clickSound);

        transform.DOScale(originalScale, animationDuration).SetUpdate(true).OnComplete(() =>
        {
            transform.DOScale(originalScale * scaleMultiplier, animationDuration).SetUpdate(true);
        });
        /*AudioManager.instance.PlaySFXAdjusted(0);*/
    }
}
