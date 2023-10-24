using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnEnterFade : MonoBehaviour
{
    public float targetAlpha;

    private float currentAlpha;

    private SpriteRenderer spriteRenderer;

    public float duration;

    private int countInside = 0;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentAlpha = spriteRenderer.color.a;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            countInside++;
            spriteRenderer.DOFade(targetAlpha, duration);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            countInside--;

            if(countInside <= 0)
            {
                spriteRenderer.DOFade(currentAlpha, duration);
            }
            
        }
    }
}
