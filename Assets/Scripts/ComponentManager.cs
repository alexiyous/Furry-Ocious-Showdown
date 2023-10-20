using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class ComponentManager : MonoBehaviour
{
    public Sprite defaultSprite;
    public TextMeshProUGUI comboText;
    public List<Components> components;
    [SerializeField] private List<TokenSlotSO> tokenSOs;
    public List<Components> comboSlot;

    [HideInInspector] public int comboCount = 0;

    private DroneAim droneAim;

    // Start is called before the first frame update
    void Start()
    {
        droneAim = GameObject.FindGameObjectWithTag("Drone Aim").GetComponent<DroneAim>();
        Initialize();
        RandomizeBullet();
        PlayerController.Attack += CheckCombo;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDisable()
    {
        PlayerController.Attack -= CheckCombo;
    }

    private void Initialize()
    {

        foreach (Components compo in components)
        {
            compo.Unset(defaultSprite);
            compo.componentManager = this;
            Button button = compo.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    compo.SetCombo();
                });
            }

        }

        foreach (TokenSlotSO tokenSO in tokenSOs)
        {
            tokenSO.droneAim = this.droneAim;
        }

        foreach (Components compo in comboSlot)
        {
            compo.Unset(defaultSprite);
            compo.componentManager = this;
        }
    }

    private void RandomizeBullet()
    {
        foreach (var component in components)
        {
            if (component.shot)
            {
                component.tokenSO = tokenSOs[Random.Range(0, tokenSOs.Count)];
                component.Set();
            }
        }
    }

    public void CheckCombo()
    {
        if (comboSlot.Count < 2 || GameManager.instance.isTargeting) return;  // Added check for comboSlot count

        Debug.Log(comboSlot[0].componentType);
        if (comboSlot[0].componentType == ComponentType.None) return;

        var compoType = comboSlot[0].componentType;
        var countSameType = comboSlot.FindAll(compo => compo.componentType == compoType).Count;
        var countNone = comboSlot.FindAll(compo => compo.componentType == ComponentType.None).Count;

        if (countNone >= 1)
        {
            if (countSameType == 2 && countNone == 1)
            {
               comboSlot[0].tokenSO.Attack(countSameType);
            }
            else if (countSameType == 1 && countNone == 2)
            {
                comboSlot[0].tokenSO.Attack(countSameType);
            }
        }
        else
        {
            if (countSameType == 3)
            {
                comboSlot[0].tokenSO.Attack(countSameType);
            }
        }
        Debug.Log(comboText.text);
        UnsetCombo();
        RandomizeBullet();
    }

    public void CheckName()
    {
        var compoType = comboSlot[0].componentType;
        var countSameType = comboSlot.FindAll(compo => compo.componentType == compoType).Count;
        var countNone = comboSlot.FindAll(compo => compo.componentType == ComponentType.None).Count;

        if (countNone >= 1)
        {
            if (countSameType == 2 && countNone == 1)
            {
                comboText.text = comboSlot[0].tokenSO.ComboName(countSameType);
            }
            else if (countSameType == 1 && countNone == 2)
            {
                comboText.text = comboSlot[0].tokenSO.ComboName(countSameType);
            }
            else if ((comboSlot[2].componentType != comboSlot[0].componentType) && countSameType == 2)
            {
                comboText.text = "";
            }
            else
            {
                comboText.text = "";
            }
        }
        else
        {
            if (countSameType == 3)
            {
                comboText.text = comboSlot[0].tokenSO.ComboName(countSameType);
            }
            else
            {
                comboText.text = "";
            }
        }
    }

    public void UnsetCombo()
    {
        comboCount = 0;
        foreach (var component in comboSlot)
        {
            component.Unset(defaultSprite);
        }
        comboText.text = "";
    }
}
