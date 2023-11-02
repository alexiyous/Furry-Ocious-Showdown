using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class BridgePoleHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int armor;

    private BridgePoleHealthBar[] healthBar;

    [field: SerializeField] public int maxHealth { get; set; }
    [field: SerializeField] public int currentHealth { get; set; }

    private float timer;
    [SerializeField] private float showTime;

    private bool isPlayerInside = false;

    public event Action<BridgePoleHealth> DieEvent;

    [SerializeField] private SpriteRenderer bridgeSR1;
    [SerializeField] private SpriteRenderer bridgeSR2;

    [SerializeField] private BoxCollider2D boxColliderbridge1;
    [SerializeField] private BoxCollider2D boxColliderbridge2;

    public GameObject bridgePole;
    public GameObject bridgePoleFront;
    public GameObject bridgePoleBack;

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;

        healthBar = GetComponentsInChildren<BridgePoleHealthBar>();
    }

    public void Damage(int damageAmount, int armorPenetration)
    {
        AudioManager.instance.PlaySFXAdjusted(32);

        float damage = damageAmount * ((float)armorPenetration / (float)armor);

        if(damage < 1)
        {
            damage = 1;
        }

        if (armorPenetration > armor)
        {
            damage = damageAmount;
        }

        currentHealth -= (int)damage;

        bridgePole.transform.DOShakePosition(.5f, new Vector3(.1f, .1f, 0f), 20, 40, false);

        //AudioManager.instance.PlaySFXAdjusted(13);
        if (currentHealth <= 0f && !isDead)
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
        AudioManager.instance.PlaySFXAdjusted(33);

        isDead = true;

        bridgeSR1.color = Color.red;
        bridgeSR2.color = Color.red;

        boxColliderbridge1.enabled = false;
        boxColliderbridge2.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = originalPosition - new Vector3(1.0f, 0.0f, 0.0f);

        OnDie();

        // Use DOTween to tween the position
        transform.DOMove(targetPosition, 1.0f)
            .SetEase(Ease.InBack);


        bridgePole.transform.DOLocalMoveY(-30f, 6f)
            .SetEase(Ease.InQuad);
        bridgePole.transform.DOLocalMoveX(UnityEngine.Random.Range(-2f, -5f), 6f)
            .SetEase(Ease.InQuad);
        bridgePoleFront.transform.DOLocalRotate(new Vector3(0f, 0f, UnityEngine.Random.Range(15f, 20f)), 5f)
            .SetEase(Ease.InQuad);
        bridgePoleBack.transform.DOLocalRotate(new Vector3(0f, 0f, UnityEngine.Random.Range(-15f, -20f)), 5f)
            .SetEase(Ease.InQuad);
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
