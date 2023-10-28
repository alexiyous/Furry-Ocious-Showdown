using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;




public class Tower : MonoBehaviour
{
    [Header("References")]
    public Transform turretRotationPoint;
    public LayerMask enemyLayerMask;
    public GameObject bulletPrefab;
    public Transform firingPoint;
    public GameObject upgradeNotif;
    public GameObject baseDeafult;
    public GameObject rotationDefault;
    public GameObject[] baseObjects;
    public GameObject[] rotationObjects;
    public Transform[] firingP;
    public TowerSO[] towerUpgrade;
    [HideInInspector] public TextMeshProUGUI upgradeButtonText;

    [Header("Attributes")]
    public float targetingRange;
    public float rotationSpeed = 5f;
    public float bps = 1f; // Bullets per second
    public float bps2 = 0f;
    public float bps3 = 0f;
    [HideInInspector] public bool inUpgradeZone;
    public int cost;
    public Vector2 targetingSize = new Vector2(4f, 4f); // Adjust the size as needed
    public float thresholdLineGizmo = 0f;
    private float threshold = 0f;
    public Vector3 centerOffset = new Vector3(0f, 0f, 0f); // Adjust the offset as needed
    private Vector3 originNotifPosition;
    [HideInInspector] public LevelUpgrade levelUpgrade;

    [HideInInspector] public Transform target;
    [HideInInspector] public float timeUntilFire;

    public void Start()
    {
        
        levelUpgrade = LevelUpgrade.Level1;
        upgradeButtonText = upgradeNotif.GetComponentInChildren<TextMeshProUGUI>();
        upgradeButtonText.text = cost.ToString();
    }



    // Update is called once per frame
    public void Update()
    {
        threshold = transform.position.x + centerOffset.x + thresholdLineGizmo - targetingSize.x / 2f;

        if (Input.GetKeyDown(KeyCode.B) && inUpgradeZone)
        {
            UpgradeTower();
        }

        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }

    }

    public virtual void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
        TowerBullet bulletScript = bulletObj.GetComponent<TowerBullet>();
        bulletScript.bulletLevel = levelUpgrade;
        bulletScript.SetTarget(target);
    }

    public virtual void FindTarget()
    {
        Collider2D[] allColliders = Physics2D.OverlapBoxAll(transform.position + centerOffset, targetingSize, 0f, enemyLayerMask);
        List<Collider2D> collidersAboveThreshold = new List<Collider2D>();

        foreach (Collider2D collider in allColliders)
        {
            if (collider.transform.position.x > threshold)
            {
                collidersAboveThreshold.Add(collider);
            }
        }

        if (collidersAboveThreshold.Count > 0)
        {
            // Find the closest target within the box
            float closestDistance = float.MaxValue;
            Collider2D closestTarget = null;

            foreach (Collider2D collider in collidersAboveThreshold)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = collider;
                }
            }

            if (closestTarget != null)
            {
                target = closestTarget.transform;
            }
        }
    }


    public virtual bool CheckTargetIsInRange()
    {
        Collider2D[] allColliders = Physics2D.OverlapBoxAll(transform.position + centerOffset, targetingSize, 0f, enemyLayerMask);
        List<Collider2D> collidersAboveThreshold = new List<Collider2D>();

        foreach (Collider2D collider in allColliders)
        {
            if (collider.transform.position.x > threshold)
            {
                collidersAboveThreshold.Add(collider);
            }
        }

        if (collidersAboveThreshold.Count > 0)
        {
            foreach (Collider2D collider in collidersAboveThreshold)
            {
                if (collider.transform.position.x >= threshold)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public virtual void RotateTowardsTarget()
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = target.position - turretRotationPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);
    }

    public virtual void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireCube(transform.position + centerOffset, targetingSize);
        Handles.DrawLine(new Vector3(transform.position.x + centerOffset.x + thresholdLineGizmo - targetingSize.x / 2f,
            transform.position.y + centerOffset.y - targetingSize.y/2f,
            transform.position.z), new Vector3(transform.position.x + centerOffset.x + thresholdLineGizmo - targetingSize.x / 2f,
            transform.position.y + centerOffset.y + targetingSize.y / 2f, transform.position.z));
    }

    public virtual void UpgradeTower()
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
                    cost = towerUpgrade[0].cost;
                    baseDeafult.SetActive(false);
                    rotationDefault.SetActive(false);
                    baseObjects[0].SetActive(true);
                    rotationObjects[0].SetActive(true);
                    turretRotationPoint = rotationObjects[0].transform;
                    firingPoint = firingP[0];
                    upgradeButtonText.text = cost.ToString();
                    break;
                case LevelUpgrade.Level3:
                    bps = bps3;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (levelUpgrade < LevelUpgrade.Level3)
            {
                upgradeNotif.SetActive(true);
                originNotifPosition = upgradeNotif.transform.position;
                Vector3 _target = upgradeNotif.transform.position + Vector3.up * 0.3f;
                upgradeNotif.transform.DOMove(_target, 0.5f, false).SetLoops(-1, LoopType.Yoyo);
            }

            inUpgradeZone = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            upgradeNotif.transform.DOKill();
            upgradeNotif.transform.position = originNotifPosition;
            upgradeNotif.gameObject.SetActive(false);
            inUpgradeZone = false;
        }
    }

}
