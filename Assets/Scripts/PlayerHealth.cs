using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float invulnerabilityDuration = 2.0f; // Duration of invulnerability after taking damage
    public float blinkInterval = 0.2f; // Interval for sprite blinking

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject countDownMenu;
    [SerializeField] private GameObject deathEffect;

    private Collider2D playerCollider;
    private Color originalColor;
    private bool isInvulnerable = false;

    [SerializeField] private GameObject parent;

    public int maxHealth = 5;
    public int currentHealth;

    private void Start()
    {
        playerCollider = GetComponent<Collider2D>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentHealth--;
            HealthUIManager.instance.HealthUpdate(currentHealth);
            if (currentHealth <= 0)
            {
                PlayerDead();
            }
        }

        if (isInvulnerable)
        {
            float t = Mathf.PingPong(Time.time, blinkInterval) / blinkInterval; // Calculate blink effect
            spriteRenderer.color = Color.Lerp(originalColor, Color.clear, t);
        }
        else
        {
            spriteRenderer.color = originalColor; // Restore original color
        }
    }

    public void BeImmortal()
    {
        StartCoroutine(Immortal());
    }

    IEnumerator Immortal()
    {
        playerCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        playerCollider.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            AudioManager.instance.PlaySFXAdjusted(5);
            isInvulnerable = true;
            playerCollider.enabled = false;
            currentHealth -= damage;
            HealthUIManager.instance.HealthUpdate(currentHealth);
            Invoke("EndInvulnerability", invulnerabilityDuration);
        }

        if (currentHealth <= 0)
        {
            PlayerDead();
        }
    }

    private void EndInvulnerability()
    {
        isInvulnerable = false;
        playerCollider.enabled = true;
    }

    private void PlayerDead()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        countDownMenu.SetActive(true);
        parent.SetActive(false);
    }
}
