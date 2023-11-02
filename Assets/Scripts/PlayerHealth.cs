using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float invulnerabilityDuration = 2.0f; // Duration of invulnerability after taking damage
    public float blinkInterval = 0.2f; // Interval for sprite blinking

    [SerializeField] private List<SpriteRenderer> spriteRenderer;
    [SerializeField] private Animator anim;
    /*[SerializeField] private GameObject countDownMenu;
    [SerializeField] private GameObject deathEffect;*/
    [SerializeField] private int armor = 20;

    [SerializeField] private Collider2D playerCollider;
    private Color originalColor;
    private bool isInvulnerable = false;

    private bool hasDead = false;

    /*public int maxHealth = 200;
    public int currentHealth;*/

    [field: SerializeField] public int maxHealth { get; set; } = 200;
    [field: SerializeField] public int currentHealth { get; set; }

    private void Start()
    {
        /*playerCollider = GetComponent<Collider2D>();*/
        foreach (var sprite in spriteRenderer)
        {
            originalColor = sprite.color;
        }
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentHealth--;
            HealthUIManager.instance.UpdateHealthBar();
            if (currentHealth <= 0)
            {
                PlayerDead();
            }
        }

        if (isInvulnerable)
        {
            float t = Mathf.PingPong(Time.time, blinkInterval) / blinkInterval; // Calculate blink effect
            foreach (var sprite in spriteRenderer)
            {
                sprite.color = Color.Lerp(originalColor, Color.clear, t);
            }
        }
        else
        {
            foreach (var sprite in spriteRenderer)
            {
                sprite.color = originalColor; // Restore original color
            }

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
       /* if (!isInvulnerable)
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
        }*/
    }

    private void EndInvulnerability()
    {
        isInvulnerable = false;
        playerCollider.enabled = true;
    }

    [Button]
    private void PlayerDead()
    {
        if(!hasDead)
        {
            hasDead = true;
            GameManager.instance.hasLost = true;
            playerCollider.enabled = false;
            anim.SetBool("isDead", true);
            StartCoroutine(GameManager.instance.Defeat());
        }
        
    }

    public void Damage(int damageAmount, int armorPenetration)
    {
        AudioManager.instance.PlaySFXAdjusted(31);

        isInvulnerable = true;
        playerCollider.enabled = false;
        float damage = damageAmount * ((float)armorPenetration / (float)armor);

        if (armorPenetration > armor)
        {
            damage = damageAmount;
        }

        if (damage < 1)
        {
            damage = 1;
        }

        currentHealth -= (int)damage;
        HealthUIManager.instance.UpdateHealthBar();
        Invoke("EndInvulnerability", invulnerabilityDuration);

        if (currentHealth <= 0)
        {
            PlayerDead();
        }
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
