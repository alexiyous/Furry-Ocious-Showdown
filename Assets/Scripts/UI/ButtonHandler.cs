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

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.DOScale(originalScale, animationDuration);

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

    public void OnSelect(BaseEventData eventData)
    {
        transform.DOScale(originalScale * scaleMultiplier, animationDuration);
        if (selector.activeInHierarchy == false)
        {
            selector.SetActive(true);
        }
        /*AudioManager.instance.PlaySFXAdjusted(1);*/
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOScale(originalScale, animationDuration);
        /*AudioManager.instance.PlaySFXAdjusted(0);*/
    }
}
