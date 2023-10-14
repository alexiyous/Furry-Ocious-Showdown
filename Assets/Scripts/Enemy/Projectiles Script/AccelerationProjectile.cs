using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationProjectile : MonoBehaviour
{
    [SerializeField] private float startSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float accelerationTime;
    [SerializeField] private float currentSpeed;

    private float acceleration;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = startSpeed;
        acceleration = maxSpeed / accelerationTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(currentSpeed <= maxSpeed)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        
        rb.velocity = transform.right * currentSpeed;


        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            currentSpeed = 0;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
    }
}
