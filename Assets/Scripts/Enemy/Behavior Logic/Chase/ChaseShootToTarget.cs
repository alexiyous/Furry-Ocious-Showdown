using System.Collections;
/*using System.Threading.Tasks;*/
using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Shoot To Target", menuName = "Enemy Logic/Chase Logic/Shoot To Target")]
public class ChaseShootToTarget : EnemyChaseSOBase
{
    [SerializeField] private float reloadTime = 2f;
    private float timer;

    [SerializeField] private float fireRate = .2f;
    [SerializeField] private int bulletCount = 3;

    [SerializeField] private float inaccuracy = 1f;

    [SerializeField] private GameObject bulletPrefab;

    public Vector3 shootOffset;

    private Transform target;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        enemy.animator.Play("Aim");

        enemy.MoveEnemyFloat(Vector2.zero);

        timer = 1f;

        
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

        if(target!= null)
        {
            enemy.CheckForDirection((target.position - enemy.transform.position).normalized);
        }

        if(timer > reloadTime - 1f)
        {
            enemy.animator.Play("Aim");
        }

        if (timer > reloadTime)
        {
            timer = 0f;
            enemy.animator.Play("Shoot");

            if(target != null)
            {
                enemy.StartCoroutine(Shoot2());  
            }

        }

        timer += Time.deltaTime * enemy.slowAmount;

        if(!enemy.isAggroed)
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

    /*private async void Shoot()
    {

        for (int i = 0;i < bulletCount; i++)
        {
            Vector3 shootInaccuracy = new Vector3(0, Random.Range(-inaccuracy, inaccuracy), 0);

            Vector2 direction = (target.position - enemy.transform.position + shootInaccuracy).normalized;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the bullet to face the player's position
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            await Task.Delay((int)(fireRate * 1000));
        }
    }*/

    private IEnumerator Shoot2()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 shootInaccuracy = new Vector3(0, Random.Range(-inaccuracy, inaccuracy), 0);

            Vector2 direction = (target.position - enemy.transform.position + shootInaccuracy + shootOffset).normalized;

            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, transform.position + shootOffset, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the bullet to face the player's position
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            yield return new WaitForSeconds(fireRate);
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

                if(buildingTransform != null )
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
