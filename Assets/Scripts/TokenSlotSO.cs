using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TokenSlotSO", menuName = "ScriptableObjects/TokenSlotSO", order = 1)]
public class TokenSlotSO : ScriptableObject
{
    public Sprite sprite;
    public ComponentType bulletType;
    public GameObject[] attackTechniques;
    public string[] attackNames;
    [HideInInspector] public DroneAim droneAim;

    public void Attack(int index)
    {
        Instantiate(attackTechniques[index - 1], droneAim.transform.position, droneAim.transform.rotation);
    }

    public string ComboName(int index)
    {
        var name = attackNames[index - 1];
        return name;
    }
}

public enum ComponentType
{
    None,
    Baut,
    Gear,
    GunPowder
}