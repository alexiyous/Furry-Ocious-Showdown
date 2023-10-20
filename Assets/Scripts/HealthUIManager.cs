using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthUIManager : MonoBehaviour
{
    public Slider healthBar;

    private PlayerHealth playerHealth;

    public static HealthUIManager instance;

    private RectTransform rectTransform;

    /*[SerializeField] private Sprite origin;
    [SerializeField] private Sprite death;

    [SerializeField] private GameObject[] healthUI;*/


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        playerHealth = FindObjectOfType<PlayerHealth>();

        playerHealth.currentHealth = playerHealth.maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        healthBar = GetComponent<Slider>();

        healthBar.maxValue = playerHealth.maxHealth;
        healthBar.value = playerHealth.maxHealth;
        /*HealthInit();*/
    }

    private void Update()
    {
        /*UpdateHealthBar();*/
    }

    /*public void HealthInit()
    {
        foreach(GameObject health in healthUI)
        {
            health.GetComponent<Image>().sprite = origin;
        }
    }

    public void HealthUpdate(int health)
    {
        for(int i = 0; i < healthUI.Length; i++)
        {
            if(i < health)
            {
                healthUI[i].GetComponent<Image>().sprite = origin;
            }
            else
            {
                healthUI[i].GetComponent<Image>().sprite = death;
            }
        }
    }*/
    public void UpdateHealthBar()
    {
        healthBar.value = playerHealth.currentHealth;
       
        rectTransform.DOShakePosition(1f, new Vector3(20f, 3f, 0f), 20, 40, true);

        if (playerHealth.currentHealth <= 0)
        {
            // Handle the game over or other logic here
            Debug.Log("Game Over");
        }
    }

}
