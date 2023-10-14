using UnityEngine;

public class BridgePoleHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int armor;

    private BridgePoleHealthBar[] healthBar;

    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float currentHealth { get; set; }

    private float timer;
    [SerializeField] private float showTime;

    private bool isPlayerInside = false;

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
        gameObject.SetActive(false);
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
