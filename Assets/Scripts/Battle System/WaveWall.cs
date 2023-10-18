using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveWall : MonoBehaviour
{
    [SerializeField] private GameObject walls;
    [SerializeField] private BattleSystem battleSystem;

    private void Start()
    {
        battleSystem.OnBattleStart += BattleSystem_OnBattleStart;
        battleSystem.OnBattleEnd += BattleSystem_OnBattleEnd;

        walls.SetActive(false);
    }

    private void BattleSystem_OnBattleEnd(object sender, EventArgs e)
    {
        walls.SetActive(false);
    }

    private void BattleSystem_OnBattleStart(object sender, EventArgs e)
    {
        walls.SetActive(true);
    }
}
