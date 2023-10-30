using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;




public class MachinegunTower : Tower
{
    public List<Transform> firingPoints;

    public override void UpgradeTower()
    {
        if (ScoreManager.instance.currentScore >= cost)
        {

            // IMPLEMENT SPAWN EFFECT

            // IMPLEMENT COIN DEDUCTION
            // This is Temporary, change it later
            ScoreManager.instance.currentScore -= cost;
            if (levelUpgrade == LevelUpgrade.Level3) return;
            levelUpgrade++;
            switch (levelUpgrade)
            {
                case LevelUpgrade.Level2:
                    rotationDefault.SetActive(false);
                    baseDeafult.SetActive(false);
                    baseObjects[0].SetActive(true);
                    rotationObjects[0].SetActive(true);
                    turretRotationPoint = rotationObjects[0].transform;
                    firingPoints.Clear();
                    for (int i = 0; i <= 2; i++)
                    {
                        firingPoints.Add(firingP[i]);
                    }
                    cost = towerUpgrade[0].cost;
                    bps = bps2;
                    upgradeButtonText.text = cost.ToString();
                    break;
                case LevelUpgrade.Level3:
                    baseObjects[0].SetActive(false);
                    rotationObjects[0].SetActive(false);
                    baseObjects[1].SetActive(true);
                    rotationObjects[1].SetActive(true);
                    turretRotationPoint = rotationObjects[1].transform;
                    firingPoints.Clear();
                    for (int i = 3; i <= 5; i++)
                    {
                        firingPoints.Add(firingP[i]);
                    }
                    bps = bps3;
                    upgradeNotif.SetActive(false);
                    break;
            }
        }
    }

    public override void Shoot()
    {
        AudioManager.instance.PlaySFXAdjusted(soundToPlay);

        int randomIndex = UnityEngine.Random.Range(0, firingPoints.Count);
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoints[randomIndex].position, turretRotationPoint.rotation);
        TowerBullet bulletScript = bulletObj.GetComponent<TowerBullet>();
        bulletScript.SetTarget(target);
    }
}
