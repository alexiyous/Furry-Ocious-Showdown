using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MachinegunTower : Tower
{
    public Transform[] firingPoints;

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
                    firingPoint = firingP[0];
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
                    firingPoint = firingP[1];
                    bps = bps3;
                    upgradeNotif.SetActive(false);
                    break;
            }
        }
    }

    public override void Shoot()
    {
        int randomIndex = Random.Range(0, firingPoints.Length);
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoints[randomIndex].position, turretRotationPoint.rotation);
        TowerBullet bulletScript = bulletObj.GetComponent<TowerBullet>();
        bulletScript.SetTarget(target);
    }
}
