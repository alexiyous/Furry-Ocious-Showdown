using UnityEngine;

[CreateAssetMenu(fileName = "Chase-Explode On Enter", menuName = "Enemy Logic/Chase Logic/Explode On Enter")]
public class ChaseExplodeOnEnter : EnemyChaseSOBase
{
    [SerializeField] private float explodeDelay = 2f;
    private float timer;

    [SerializeField] private GameObject explosionPrefab;

    public int soundToPlay;

    public override void DoAnimationTriggerEventLogic(Enemy.AnimationTriggerType triggerType)
    {
        base.DoAnimationTriggerEventLogic(triggerType);
    }

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        AudioManager.instance.PlaySFXAdjusted(soundToPlay);
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        enemy.MoveEnemyFloat(Vector2.zero);

        

    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();

        if (timer > explodeDelay)
        {
            timer = 0f;

            Explode();
        }

        timer += Time.deltaTime;
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private void Explode()
    {
        AudioManager.instance.StopSFX(soundToPlay);

        ObjectPoolManager.SpawnObject(explosionPrefab, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

        enemy.Die();
    }
}
