using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class StartAnyKey : MonoBehaviour
{
    public CanvasGroup buttons;

    public GameObject background;
    public CanvasGroup logo;

    private bool isBlinking = false;
    private bool isPressed = false;

    private TextMeshProUGUI text;
    public float fadeDuration;

    private Color initialTextColor;

    public float blinkDuration = 0.5f; // Duration of each blink cycle
    public float blinkAlpha = 0.5f;    // Target alpha during the blink

    public float targetPosition; // The target position you want to move the UI element to

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        initialTextColor = text.color;

        StartBlinking();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && !isPressed)
        {
            if (text != null)
            {
                isPressed = true;

                StopBlinking();

                // Create a sequence for the fade animation
                Sequence sequence = DOTween.Sequence();

                // Fade out the TextMeshPro text by modifying the alpha of its color
                sequence.Append(text.DOColor(new Color(initialTextColor.r, initialTextColor.g, initialTextColor.b, 0.0f), fadeDuration));
                sequence.Join(background.transform.DOMoveX(targetPosition, 1f));
                sequence.Join(logo.DOFade(0, 1f));
                // When the animation is complete, set the GameObject inactive and activate 'buttons'
                sequence.OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    buttons.gameObject.SetActive(true);
                    buttons.DOFade(1f,1f);
                    logo.gameObject.SetActive(false);
                });
            }
        }
    }

    void StartBlinking()
    {
        /*if (isBlinking || text == null)
            return;
*/
        isBlinking = true;
        Sequence sequence = DOTween.Sequence();

        sequence.Append(text.DOFade(blinkAlpha, blinkDuration)); // Fade in
        sequence.Append(text.DOFade(initialTextColor.a, blinkDuration)); // Fade out

        sequence.OnComplete(StartBlinking); 
    }

    void StopBlinking()
    {
        if (!isBlinking || text == null)
            return;

        isBlinking = false;
        text.DOKill();
        text.DOFade(initialTextColor.a, 0f); // Ensure the text is fully visible
    }
}
