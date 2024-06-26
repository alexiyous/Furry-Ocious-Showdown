using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MortarTower : Tower
{
    private Vector2 Vo;

    private float timeToHitTarget; // Time to hit the target
    public float yOffset;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speedBullet = 10;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;

    Vector3 startPosBullet;

    public override void RotateTowardsTarget()
    {
        if (!target) return;

        Vo = CalculateVelocity(target.position, firingPoint.position, 1f);
        timeToHitTarget = CalculateTimeToHit(target.position, firingPoint.position, Vo);

        // Calculate the angle in radians between the current position and the target
        float angle = Mathf.Atan2(Vo.y + yOffset, Vo.x) - 360f;

        // Convert the angle to degrees and set it as the Z rotation of turretRotationPoint
        turretRotationPoint.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
    }

    public override void Shoot()
    {

        AudioManager.instance.PlaySFXAdjusted(soundToPlay);

        GameObject bulletObj = Instantiate(bulletPrefab, turretRotationPoint.position, Quaternion.identity);

        var mortarBullet = bulletObj.GetComponent<MortarBullet>();
        mortarBullet.targetPos = target.position;

        /* // Use DoTween to rotate transform.up to face the target position from the instantiated current rotation
         bulletObj.transform.DORotateQuaternion(Quaternion.LookRotation(Vector3.forward, target.position - bulletObj.transform.position), timeToHitTarget);*/
        var rb = bulletObj.GetComponent<Rigidbody2D>();
        /*rb.velocity = Vo;*/

        MortarBullet bulletScript = bulletObj.GetComponent<MortarBullet>();
        bulletScript.timeToHitTarget = timeToHitTarget;
        bulletScript.isTargeting = true;
        bulletScript.SetTarget(target);
        bulletScript.bulletLevel = levelUpgrade;

        /*if (bulletObj != null)
        {
            StartCoroutine(bulletScript.DestroyBulletIfMissed(timeToHitTarget));

        }*/
    }

    public override void UpgradeTower()
    {
        if (ScoreManager.instance.currentScore >= cost)
        {
            AudioManager.instance.PlaySFXAdjusted(34);
            // IMPLEMENT SPAWN EFFECT

            // IMPLEMENT COIN DEDUCTION
            // This is Temporary, change it later
            ScoreManager.instance.currentScore -= cost;
            if (levelUpgrade == LevelUpgrade.Level3) return;
            levelUpgrade++;
            switch (levelUpgrade)
            {
                case LevelUpgrade.Level2:
                    baseDeafult.SetActive(false);
                    rotationDefault.SetActive(false);
                    baseObjects[0].SetActive(true);
                    rotationObjects[0].SetActive(true);
                    turretRotationPoint = rotationObjects[0].transform;
                    firingPoint = firingP[0];
                    cost = towerUpgrade[0].cost;
                    upgradeButtonText.text = cost.ToString();
                    break;
                case LevelUpgrade.Level3:
                    baseObjects[0].SetActive(false);
                    rotationObjects[0].SetActive(false);
                    baseObjects[1].SetActive(true);
                    rotationObjects[1].SetActive(true);
                    turretRotationPoint = rotationObjects[1].transform;
                    firingPoint = firingP[1];
                    upgradeNotif.SetActive(false);
                    break;
            }
        }
    }

    float CalculateTimeToHit(Vector3 target, Vector3 origin, Vector2 initialVelocity)
    {
        Vector2 distance = target - origin;
        float timeToHit = distance.magnitude / initialVelocity.magnitude;
        return timeToHit;
    }

    Vector2 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector2 distance = target - origin; // Changed from Vector3 to Vector2

        float Sx = distance.x;
        float Sy = distance.y;

        float Vx = Sx / time;
        float Vy = (Sy + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time) / time;

        return new Vector2(Vx, Vy);
    }

}
