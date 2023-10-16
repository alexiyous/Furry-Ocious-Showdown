using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerUpgradeSO", menuName = "ScriptableObjects/TowerUpgradeSO", order = 2)]
public class TowerSO : ScriptableObject
{
    public Sprite neckSprite;
    public Sprite headSprite;
    public int cost;
}
