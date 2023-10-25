using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteFadeAfterTimer : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;

    private float _initialAlpha;
    public float _fadeAlpha = 0f;
    public float duration = 2f;

    public float fadeTimer = 2f;

    // Start is called before the first frame update
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialAlpha = _spriteRenderer.color.a;
    }


    void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Color newColor = _spriteRenderer.color;
        newColor.a = _initialAlpha;  // Set the alpha to the initial alpha value
        _spriteRenderer.color = newColor;  // Apply the new color

        /*fadeSequence.Kill();  // Kill any previous sequences
        fadeSequence = DOTween.Sequence();
        fadeSequence.Append(_spriteRenderer.DOFade(_fadeAlpha, duration));*/

        StartCoroutine(FadeAferTimer());
    }

  

    IEnumerator FadeAferTimer()
    {
        yield return new WaitForSeconds(fadeTimer);

        _spriteRenderer.DOFade(_fadeAlpha, duration).SetEase(Ease.InQuad);
    }
}
