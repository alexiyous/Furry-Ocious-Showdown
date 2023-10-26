using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBullet : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;
    public GameObject hitEffect;

    [Header("Attributes")]
    public float bulletSpeed = 5f;
    public int damage;
    public int damage2 = 0;
    public int damage3 = 0;
    public int armor;
    public int armor2 = 0;
    public int armor3 = 0;

    [HideInInspector] public Vector2 direction;
    /*[HideInInspector]*/ public LevelUpgrade bulletLevel = LevelUpgrade.Level1;

    [HideInInspector] public Transform target;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {
        direction = new Vector2();
    }


    // Update is called once per frame
    public void FixedUpdate()
    {
        Move();
    }
    public virtual void SetTarget(Transform _target)
    {
        target = _target;
    }   

    public abstract void Move();

    public virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().Damage(damage, armor);
            ObjectPoolManager.SpawnObject(hitEffect, transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            Destroy(gameObject);
        }
    }
}

public enum LevelUpgrade
{
    Level1,
    Level2,
    Level3
}