using System.Collections;
using UnityEngine;


[CreateAssetMenu(fileName = "Chase-Two Shoot To Target", menuName = "Enemy Logic/Chase Logic/Two Shoot To Target")]
public class ChaseTwoShootToTarget : EnemyChaseSOBase
{
    [SerializeField] private float reloadMainTime = 4f;
    private float timerMain;
    [SerializeField] private float fireRateMain = 1f;
    [SerializeField] private int bulletCountMain = 1;
    [SerializeField] private float inaccuracyMain = 1f;
    [SerializeField] private GameObject bulletPrefabMain;

    [SerializeField] private float reloadSecondaryTime = 2f;
    private float timerSecondary;
    [SerializeField] private float fireRateSecondary = .2f;
    [SerializeField] private int bulletCountSecondary = 5;
    [SerializeField] private float inaccuracySecondary = 1.9f;
    [SerializeField] private GameObject bulletPrefabSecondary;

    public Vector3 offsetMain;
    public Vector3 offsetSecondary;

    public int soundToPlayMain;
    public int soundToPlaySecondary;

    private Transform target;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        enemy.MoveEnemyFloat(Vector2.zero);

        enemy.animator.StopPlayback();

        timerMain = 1f;
        timerSecondary = 1f;


    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        enemy.MoveEnemyFloat(Vector2.zero);

        GetTarget();
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        if (target != null)
        {
            enemy.CheckForDirection((target.position - enemy.transform.position).normalized);
        }

        

        if (timerMain > reloadMainTime)
        {
            timerMain = 0f;

            enemy.animator.Play("Shoot", - 1, 0f);

            if (target != null)
            {
                enemy.StartCoroutine(ShootMain());
            }

        }

        if (timerSecondary > reloadSecondaryTime)
        {
            timerSecondary = 0f;

            if (target != null)
            {
                enemy.StartCoroutine(ShootSecondary());
            }

        }

        timerMain += Time.deltaTime * enemy.slowAmount;
        timerSecondary += Time.deltaTime * enemy.slowAmount;

        if (!enemy.isAggroed)
        {
            enemy.stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private IEnumerator ShootMain()
    {
        for (int i = 0; i < bulletCountMain; i++)
        {
            AudioManager.instance.PlaySFXAdjusted(soundToPlayMain);

            Vector3 shootInaccuracy = new Vector3(0, Random.Range(-inaccuracyMain, inaccuracyMain), 0);

            Vector3 shootOffsetFinal = offsetMain;

            if (!enemy.isFacingLeft)
            {
                shootOffsetFinal = new Vector3(-offsetMain.x, offsetMain.y, offsetMain.z);
            }

            Vector2 direction = (target.position - (enemy.transform.position + shootOffsetFinal) + shootInaccuracy).normalized;

            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefabMain, transform.position + shootOffsetFinal, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the bullet to face the player's position
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            yield return new WaitForSeconds(fireRateMain);
        }
    }


    private IEnumerator ShootSecondary()
    {
        for (int i = 0; i < bulletCountSecondary; i++)
        {
            AudioManager.instance.PlaySFXAdjusted(soundToPlaySecondary);

            Vector3 shootInaccuracy = new Vector3(0, Random.Range(-inaccuracySecondary, inaccuracySecondary), 0);

            Vector3 shootOffsetFinal = offsetSecondary;

            if (!enemy.isFacingLeft)
            {
                shootOffsetFinal = new Vector3(-offsetSecondary.x, offsetSecondary.y, offsetSecondary.z);
            }

            Vector2 direction = (target.position - (enemy.transform.position + shootOffsetFinal) + shootInaccuracy).normalized;

            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefabSecondary, transform.position + shootOffsetFinal, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the bullet to face the player's position
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            yield return new WaitForSeconds(fireRateSecondary);
        }
    }

    private void GetTarget()
    {
        float playerDistance = Vector3.Distance(transform.position, playerTransform.position);
        float closestBuildingDistance = Mathf.Infinity;

        // Loop through all building transforms to find the closest one
        foreach (Transform buildingTransform in buildingTransforms)
        {
            float distance = Vector3.Distance(transform.position, buildingTransform.position);
            if (distance < closestBuildingDistance && buildingTransform.gameObject.activeInHierarchy)
            {
                closestBuildingDistance = distance;

                if (buildingTransform != null)
                {
                    target = buildingTransform;
                }

            }
        }

        // Compare distances and set the target accordingly
        if (playerDistance < closestBuildingDistance)
        {
            target = playerTransform; // Player is closer
        }
    }
}
