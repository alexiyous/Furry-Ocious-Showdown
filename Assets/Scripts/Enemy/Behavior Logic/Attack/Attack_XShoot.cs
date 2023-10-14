using UnityEngine;
using DG.Tweening;


[CreateAssetMenu(fileName = "Attack-X Attack", menuName = "Enemy Logic/Attack Logic/X Shoot")]

public class Attack_XShoot : EnemyAttackSOBase
{
    [SerializeField] public Rigidbody2D bulletPrefab;

    private float _timer;
    [SerializeField] private float _timeBetweenShots = 3f;

    [SerializeField] private float _exitTimer;
    [SerializeField] private float _timeTillExit = 3f;

    [SerializeField] public float randomMovementRange = 1f;

    private Vector3 _targetPos;
    private Vector3 _direction;
    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();
        
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        _direction = (_targetPos - enemy.transform.position).normalized;

        enemy.MoveEnemyFloat(_direction * enemy.baseSpeed * enemy.slowAmount * .5f);

        if (_timer > _timeBetweenShots - 1.3f)
        {
            enemy.MoveEnemyFloat(Vector2.zero);
        }


        if (_timer  > _timeBetweenShots)
        {
            _timer = 0f;

            Shoot2();

            _targetPos = GetRandomPointInCircle();
            
        }

        _timer += Time.deltaTime * enemy.slowAmount;

        if (!enemy.isWithinAttackingRadius)
        {
            _exitTimer += Time.deltaTime;
            if (_exitTimer > _timeTillExit)
            {
                enemy.stateMachine.ChangeState(enemy.chaseState);
            }
        }
        else
        {
            _exitTimer = 0f;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);

    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    public void Shoot2()
    {
        Vector2 dir1 = new Vector2(Mathf.Cos(45f * Mathf.Deg2Rad), Mathf.Sin(45f * Mathf.Deg2Rad));
        Vector2 dir2 = new Vector2(Mathf.Cos(-45f * Mathf.Deg2Rad), Mathf.Sin(-45f * Mathf.Deg2Rad));
        Vector2 dir3 = new Vector2(Mathf.Cos(135f * Mathf.Deg2Rad), Mathf.Sin(135f * Mathf.Deg2Rad));
        Vector2 dir4 = new Vector2(Mathf.Cos(-135f * Mathf.Deg2Rad), Mathf.Sin(-135f * Mathf.Deg2Rad));

        Rigidbody2D bullet1 = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.Euler(0f, 0f, 45f));
        Rigidbody2D bullet3 = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.Euler(0f, 0f, 135f));
        Rigidbody2D bullet2 = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.Euler(0f, 0f, -45f));
        Rigidbody2D bullet4 = Instantiate(bulletPrefab, enemy.transform.position, Quaternion.Euler(0f, 0f, -135f));

        bullet1.velocity = dir1 * 6f;
        bullet2.velocity = dir2 * 6f;
        bullet3.velocity = dir3 * 6f;
        bullet4.velocity = dir4 * 6f;
    }

    private Vector3 GetRandomPointInCircle()
    {
        return enemy.transform.position + (Vector3)Random.insideUnitCircle * randomMovementRange;
    }

}
