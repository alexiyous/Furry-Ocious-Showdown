using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BridgePoleHealthBar : MonoBehaviour
{
    public Slider healthBar;
    private BridgePoleHealth bridgePoleHealth;

    public bool isShowing = false;

    private void Start()
    {
        gameObject.SetActive(false);
        bridgePoleHealth = GetComponentInParent<BridgePoleHealth>();
        healthBar = GetComponent<Slider>();

        healthBar.maxValue = bridgePoleHealth.maxHealth;
        healthBar.value = bridgePoleHealth.currentHealth;    
    }

    public void Show()
    {
        isShowing = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isShowing = false;
        gameObject.SetActive(false);
    }
}
