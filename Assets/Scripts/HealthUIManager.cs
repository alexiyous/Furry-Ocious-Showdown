using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIManager : MonoBehaviour
{
    public static HealthUIManager instance;

    [SerializeField] private Sprite origin;
    [SerializeField] private Sprite death;

    [SerializeField] private GameObject[] healthUI;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        HealthInit();
    }

    public void HealthInit()
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
    }


}
