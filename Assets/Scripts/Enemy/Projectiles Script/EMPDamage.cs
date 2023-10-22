using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPDamage : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private int armorPenetration = 0;
    [SerializeField] private float stunDuration = 3f;
    [SerializeField] private float slowAmount = 0;

    private SpriteRenderer spriteRenderer;

    private Color originalColor;
    [SerializeField] private float fadeDuration = 1f; // Duration of the fade in seconds
    private float currentFadeTime = 0f;

    // Start is called before the first frame update

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void OnEnable()
    {
        spriteRenderer.color = originalColor;
        currentFadeTime = 0;
       /* AudioManager.instance.PlaySFXAdjusted(6);*/
    }

    // Update is called once per frame
    void Update()
    {
        currentFadeTime += Time.deltaTime;
        float fadeProgress = currentFadeTime / fadeDuration;

        // Perform the fade
        float lerpedAlpha = Mathf.Lerp(1f, 0f, fadeProgress);
        Color lerpedColor = new Color(originalColor.r, originalColor.g, originalColor.b, lerpedAlpha);
        spriteRenderer.color = lerpedColor;

        // If the fade is complete, destroy the game object
        if (fadeProgress >= 1f)
        {
            Destroy(gameObject);

            /*ObjectPoolManager.ReturnObjectPool(gameObject);*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);
            collision.GetComponent<ISlowable>().ApplySlow(slowAmount, stunDuration, Color.blue);
        }
    }
}
