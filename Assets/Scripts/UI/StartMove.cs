using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class StartMove : MonoBehaviour
{
    public float speed = 50f;
    public Vector2 initialPosition;
    public Vector2 finalPosition;

    public Ease easeType = Ease.InQuad;

    RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        float distance = Vector2.Distance(initialPosition, finalPosition);
        float duration = distance / speed;

        // Use DoTween to move the UI element
        rectTransform.anchoredPosition = initialPosition; // Set the initial position
        rectTransform.DOAnchorPos(finalPosition, duration).SetEase(easeType).OnComplete(() =>
        {

            SceneTransitionHandler.instance.EndTransition("Main Menu");
        });

    }

}
