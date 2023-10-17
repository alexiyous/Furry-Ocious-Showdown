using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BridgePoleHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int armor;

    private BridgePoleHealthBar[] healthBar;

    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float currentHealth { get; set; }

    private float timer;
    [SerializeField] private float showTime;

    private bool isPlayerInside = false;

    public event Action<BridgePoleHealth> DieEvent;

    [SerializeField] private SpriteRenderer bridgeSR1;
    [SerializeField] private SpriteRenderer bridgeSR2;

    private void Awake()
    {
        currentHealth = maxHealth;

        healthBar = GetComponentsInChildren<BridgePoleHealthBar>();
    }

    public void Damage(int damageAmount, int armorPenetration)
    {
        float damage = damageAmount * ((float)armorPenetration / (float)armor);

        if(damage < 1)
        {
            damage = 1;
        }

        currentHealth -= (int)damage;
        //AudioManager.instance.PlaySFXAdjusted(13);
        if (currentHealth <= 0f)
        {
            Die();
        }

        foreach(var item in healthBar)
        {
            item.healthBar.value = currentHealth;
            item.Show();
            timer = 0;
        }

        
    }

    private void FixedUpdate()
    {
        if(timer > showTime)
        {
            foreach(var item in healthBar)
            {
                item.Hide();
            }

        }

        if (healthBar[0].isShowing && !isPlayerInside)
        {
            timer += Time.deltaTime;
        }
    }

    public void Die()
    {
        bridgeSR1.color = Color.red;
        bridgeSR2.color = Color.red;

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition - new Vector3(1.0f, 0.0f, 0.0f);

        // Use DOTween to tween the position
        transform.DOMove(targetPosition, 1.0f)
            .SetEase(Ease.InOutSine) // Choose an ease type
            .OnComplete(() => OnDie()); // When the tween is complete, call OnDie
    }

    protected virtual void OnDie()
    {
        gameObject.SetActive(false);
        // Check if there are any subscribers to the DieEvent.
        DieEvent?.Invoke(this);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerInside = true;
            foreach (var item in healthBar)
            {
                item.healthBar.value = currentHealth;
                item.Show();
                timer = 0;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            isPlayerInside = false;
            foreach (var item in healthBar)
            {
                item.Hide();
            }
        }

        
    }
}
