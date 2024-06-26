using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour, IDamageable, IEnemyMovable, ITriggerCheckable, ISlowable
{
    [field: SerializeField] public int maxHealth { get; set; }
    [field: SerializeField] public int currentHealth { get; set; }
    [field: SerializeField] public int armor { get; set; }
    [field: SerializeField] public float baseSpeed { get; set; }
    public float slowAmount { get; set; }
    public float slowDuration { get; set; }
    public float currentSlowTime { get; set; }
    [field: SerializeField] public int score { get; set; }
    public Rigidbody2D RB { get; set; }
    public bool isFacingLeft { get; set; } = true;
    public bool isAggroed { get; set; }
    public bool isWithinAttackingRadius { get; set; }
    public bool isAlive { get; set; }
    public SpriteRenderer spriteRenderer { get; set; }
    public Color originalColor { get; set; }
    public Collider2D enemyCollider { get; set; }
    public Animator animator { get; set; }
    public SpriteRenderer[] sprites { get; set; }

    [field: SerializeField] public GameObject deathEffect { get; set; }
    [field: SerializeField] public int deathSound { get; set; }

    public Vector2 initialPosition { get; set; }
    public bool isHovering { get; set; }

    [field: SerializeField] public float killTimer { get; set; } = 1f;

    public ShadowCaster2D shadowCaster { get; set; }


    #region State Machine Variables

    public EnemyStateMachine stateMachine { get; set; }
    public EnemyIdleState idleState { get; set; }
    public EnemyChaseState chaseState { get; set; }
    public EnemyAttackState attackState { get; set; }


    #endregion

    #region SO Variables

    [InlineEditor] [SerializeField] private EnemyIdleSOBase EnemyIdleBase;
    [InlineEditor] [SerializeField] private EnemyChaseSOBase EnemyChaseBase;
    [InlineEditor] [SerializeField] private EnemyAttackSOBase EnemyAttackBase;

    public EnemyIdleSOBase EnemyIdleBaseInstance { get; set; }
    public EnemyChaseSOBase EnemyChaseBaseInstance { get; set; }
    public EnemyAttackSOBase EnemyAttackBaseInstance { get; set; }

    #endregion

    private void Awake()
    {
        EnemyIdleBaseInstance = Instantiate(EnemyIdleBase);
        EnemyChaseBaseInstance = Instantiate(EnemyChaseBase);
        EnemyAttackBaseInstance = Instantiate(EnemyAttackBase);

        stateMachine = new EnemyStateMachine();
        idleState = new EnemyIdleState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);

        isAlive = true;

    }

    public virtual void Start()
    {
        currentHealth = maxHealth;
        RB = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        shadowCaster = GetComponent<ShadowCaster2D>();

        originalColor = spriteRenderer.color;
        slowAmount = 1;
        isAlive = true;

        initialPosition = transform.position;

        EnemyIdleBaseInstance.Initialize(gameObject, this);
        EnemyChaseBaseInstance.Initialize(gameObject, this);
        EnemyAttackBaseInstance.Initialize(gameObject, this);

        stateMachine.Initialize(idleState);
    }

    public virtual void Update()
    {
        stateMachine.CurrentEnemyState.FrameUpdate();

        if(slowAmount != 1 && isAlive)
        {
            animator.speed = 1 * slowAmount;
        } else
        {
            animator.speed = 1;
        }

        

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentEnemyState.PhysicsUpdate();

        if (slowDuration > 0)
        {
            CheckSlowEffect();
        }
    }

    public void CheckSlowEffect()
    {
        if (currentSlowTime > 0)
        {

            currentSlowTime -= Time.deltaTime;

            if (currentSlowTime <= 0)
            {
                RemoveSlow();
            }
        }
    }

    #region Animation Triggers

    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        switch (triggerType)
        {
            case AnimationTriggerType.Attacking:
                animator.SetTrigger("AttackAnimation");
                break;
            case AnimationTriggerType.Chase:
                animator.SetTrigger("ChaseAnimation");
                break;

        }

        stateMachine.CurrentEnemyState.AnimationTriggerEvent(triggerType);
    }

    public enum AnimationTriggerType
    {
        EnemyDamaged,
        Attacking,
        Idle,
        Chase,
        Attack
    }

    #endregion

    #region Damage /Die Functions

    public virtual void Damage(int damageAmount, int armorPenetration)
    {
        float damage = damageAmount * ((float)armorPenetration / (float)armor);

        if (damage < 1)
        {
            damage = 1;
        }

        if(armorPenetration > armor)
        {
            damage = damageAmount;
        }

        StartCoroutine(ChangeColor(new Color(1,0.6f,0.6f)));

        currentHealth -= (int)damage;
        //AudioManager.instance.PlaySFXAdjusted(13);
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (isAlive)
        {
            AudioManager.instance.PlaySFXAdjusted(deathSound);

            enemyCollider.enabled = false;

            ScoreManager.instance.AddScore(score);

            animator.speed = 1;
            animator.Play("Death");
            shadowCaster.enabled = false;

            ObjectPoolManager.SpawnObject(deathEffect, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            stateMachine.ChangeState(attackState);
            Destroy(gameObject, killTimer);
            isAlive = false;
        }

        
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public IEnumerator ChangeColor(Color color)
    {
        
        foreach (SpriteRenderer sprite in sprites)
        {
            Color newColor = new Color(color.r, color.g, color.b, sprite.color.a);
            sprite.color = newColor;
        }

        Color newSpriteRendererColor = new Color(color.r, color.g, color.b, spriteRenderer.color.a);
        spriteRenderer.color = newSpriteRendererColor;
        yield return new WaitForSeconds(.05f);

        Color newSpriteRendererColor2 = new Color(Color.white.r, Color.white.g, Color.white.b, spriteRenderer.color.a);
        spriteRenderer.color = newSpriteRendererColor2;

        foreach (SpriteRenderer sprite in sprites)
        {
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, sprite.color.a);
            sprite.color = newColor;
        }
        yield return new WaitForSeconds(.05f);
    }

    #endregion

    #region Movement Functions

    public void MoveEnemy(Vector2 velocity)
    {
        
        // Apply the updated velocity to the Rigidbody2D
        RB.velocity = new Vector2(velocity.x, RB.velocity.y);

        // Check for direction (if needed)
        CheckForDirection(velocity);
    }

    public void MoveEnemyFloat(Vector2 velocity)
    {

        RB.velocity = velocity;

        // Check for direction (if needed)
        CheckForDirection(velocity);
    }

    public void MoveEnemyNoTurn(Vector2 velocity)
    {
        RB.velocity = velocity;
    }

    public void CheckForDirection(Vector2 velocity)
    {
        if (isFacingLeft && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingLeft = false;
        }
        else if (!isFacingLeft && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingLeft = true;
        }
    }



    #endregion

    #region Distance Checks

    public void SetAggroStatus(bool isAggroed)
    {
        this.isAggroed = isAggroed;
    }

    public void SetAttackingDistanceBool(bool isWithinAttackingRadius)
    {
        this.isWithinAttackingRadius = isWithinAttackingRadius;
    }


    #endregion


    #region Slow Effects

    public void ApplySlow(float amount, float duration, Color color)
    {
        slowAmount = amount;
        slowDuration = duration;
        currentSlowTime = duration;
        spriteRenderer.color = color;
    }

    public void RemoveSlow()
    {
        slowAmount = 1;
        slowDuration = 0;
        currentSlowTime = 0;
        spriteRenderer.color = originalColor;
    }

    #endregion
}
