using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentManager : MonoBehaviour
{
    public Sprite defaultSprite;
    public List<Components> components;
    [SerializeField] private List<TokenSlotSO> tokenSOs;
    public List<Components> comboSlot;

    [HideInInspector] public int comboCount = 0;

    private DroneAim droneAim;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        droneAim = GameObject.FindGameObjectWithTag("Drone Aim").GetComponent<DroneAim>();
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

    private void CheckCombo()
    {
        if (comboSlot.Count < 2) return;  // Added check for comboSlot count

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
        UnsetCombo();
        RandomizeBullet();
    }

    public void UnsetCombo()
    {
        comboCount = 0;
        foreach (var component in comboSlot)
        {
            component.Unset(defaultSprite);
        }
    }
}
