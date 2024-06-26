using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BridgeTotalHealth : MonoBehaviour
{
    public Slider healthBar;

    [SerializeField] private int maxHealth;
    private int currentHealth;

    private BridgePoleHealth[] bridgePoleHealths;

    private RectTransform rectTransform;

    public float shakeIntensity = 6f;
    public float shakeDuration = .8f;

    public bool hasDied = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = maxHealth;

        rectTransform = GetComponent<RectTransform>();

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

        CinemachineShake.instance.ShakeCamera(shakeIntensity, shakeDuration);

        rectTransform.DOShakePosition(1f, new Vector3(30f, 3f, 0f), 20, 40, true);
    }

    private void UpdateHealthBar()
    {
        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            // Handle the game over or other logic here
            if(!hasDied)
            {
                hasDied = true;
                GameManager.instance.hasLost = true;
                Debug.Log("Game Over");
                StartCoroutine(GameManager.instance.Defeat());
            }

            
        }
    }
}
