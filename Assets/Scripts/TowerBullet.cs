using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBullet : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D rb;

    [Header("Attributes")]
    public float bulletSpeed = 5f;
    public float damage;
    [HideInInspector] public Vector2 direction;
    public LevelUpgrade bulletLevel = LevelUpgrade.Level1;

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

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}

public enum LevelUpgrade
{
    Level1,
    Level2,
    Level3
}