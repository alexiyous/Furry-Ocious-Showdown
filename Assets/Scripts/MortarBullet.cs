using UnityEngine;

public class MortarBullet : TowerBullet
{
    public float timeToHitTarget;
    public bool isTargeting;
    public CircleCollider2D hitZone;
    public float bombCollide;
    public float hitZone2;
    public float hitZone3;

    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject explosion3;

    private GameObject explosionToUse;

    private bool isHit = false;

    [Tooltip("Position we want to hit")]
    public Vector3 targetPos;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 10;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;

    public float maxSpeedIncrease = 1f;
    public float maxArcIncrease = 1f;


    Vector3 startPos;

    public override void Start()
    {
        base.Start();
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        startPos = transform.position;
    }


    public override void Move()
    {
        if (target = null) return;

        float nextX;
        // Compute the next position, with arc added in
        float x0 = startPos.x;
        float x1 = targetPos.x;
        float dist = x1 - x0;

        nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);


        float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
        float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
        var nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

        // Rotate to face the next position, and then move there
        
        if (target != null || targetPos != null)
        {

            transform.rotation = LookAt2D(nextPos - transform.position);
            transform.position = nextPos;
        } else
        {
            Destroy(gameObject);
        }

        switch (bulletLevel)
        {
            case LevelUpgrade.Level1:
                explosionToUse = explosion1;
                break;
            case LevelUpgrade.Level2:
                explosionToUse = explosion2;
                damage = damage2;
                armor = armor2;
                /*bombCollide = hitZone2;*/
                break;
            case LevelUpgrade.Level3:
                explosionToUse = explosion3;
                damage = damage3;
                armor = armor3;
                /*bombCollide = hitZone3;*/
                break;
        }

        // Do something when we reach the target
        if ((nextPos - targetPos).sqrMagnitude < 0.1f)
        {
            ObjectPoolManager.SpawnObject(explosionToUse, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            Destroy(gameObject); // Destroy the game object after timeToHit seconds

        }

    }

    /// 
	/// This is a 2D version of Quaternion.LookAt; it returns a quaternion
	/// that makes the local +X axis point in the given forward direction.
	/// 
	/// forward direction
	/// Quaternion that rotates +X to align with forward
	static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }

    /*public IEnumerator DestroyBulletIfMissed(float timeToHit)
    {
        if (this == null || !gameObject.activeInHierarchy) yield break; // Check if the bullet exists before continuing

        yield return new WaitForSeconds(timeToHit); // Wait for the calculated time + 0.5 seconds

        if (isTargeting)
        {
            if (gameObject != null)
            {
                *//*hitZone.radius = bombCollide;*//*
                ObjectPoolManager.SpawnObject(explosionToUse, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
                Destroy(gameObject); // Destroy the game object after timeToHit seconds
            }
        }
    }*/

    public override void OnBecameInvisible()
    {
        /*gameObject.SetActive(false);*/
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isHit)
        {
            isHit = true;
            isTargeting = false;
            /*hitZone.radius = bombCollide;*/
            collision.GetComponent<IDamageable>().Damage(damage, armor);
            ObjectPoolManager.SpawnObject(explosionToUse, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            Destroy(gameObject);
        }
    }
}
