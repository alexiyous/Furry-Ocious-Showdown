using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float rotationSpeed;
    private Vector2 direction;

    public float seekStopTime = 3f;
    private float timer = 0f;

    Rigidbody2D rb;

    public float moveSpeed;

    [SerializeField] private GameObject target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        direction = target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        // Store the initial rotation for reference during updates
        direction = target.transform.position - transform.position;

        // Start moving the projectile immediately
        rb.velocity = transform.right * moveSpeed;
    }

    private void FixedUpdate()
    {
        if(timer < seekStopTime)
        {
            direction = target.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            timer += Time.deltaTime;
        }
        

        //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        Vector3 moveDirection = transform.right;
        /*transform.position += moveDirection * moveSpeed * Time.deltaTime;*/

        rb.velocity = moveDirection * moveSpeed;

        
    }

    
}
