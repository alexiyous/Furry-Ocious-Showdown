using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    public float rotationSpeed;
    private Vector2 direction;

    public float seekStopTime = 3f;
    private float timer = 0f;

    Rigidbody2D rb;

    public float moveSpeed;

    private Transform[] buildingsTransform;
    private Transform nullTransform;

    private Transform target;

    public bool isFlipped = false;

    private void Start()
    {
        nullTransform = GameObject.FindGameObjectWithTag("Null Transform").transform;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();

        if (isFlipped)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
        }

        timer = 0f;

        buildingsTransform = GameObject.FindGameObjectsWithTag("Building")
                            .Select(building => building.transform)
                            .ToArray();

        if(buildingsTransform.Length != 0)
        {
            int randomTarget = Random.Range(0, buildingsTransform.Length);
            /*Debug.Log(randomTarget);*/

            target = buildingsTransform[randomTarget];
        } else
        {
            target = nullTransform;
        }
        

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
