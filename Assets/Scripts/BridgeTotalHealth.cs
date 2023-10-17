using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BridgeTotalHealth : MonoBehaviour
{
    public Slider healthBar;

    [SerializeField] private int maxHealth;
    private int currentHealth;

    private BridgePoleHealth[] bridgePoleHealths;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;

        UpdateHealthBar();
        bridgePoleHealths = FindObjectsOfType<BridgePoleHealth>();

        healthBar = GetComponent<Slider>();

        foreach (var bridgePoleHealth in bridgePoleHealths)
        {
            bridgePoleHealth.DieEvent += HandlePoleHealthDie;
        }

    }

    private void HandlePoleHealthDie(BridgePoleHealth bridgePoleHealth)
    {
        // Respond to the bridge pole health "die" event here.
        // You can reduce the total health or update the health bar.
        currentHealth -= 1;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            // Handle the game over or other logic here
        }
    }
}
