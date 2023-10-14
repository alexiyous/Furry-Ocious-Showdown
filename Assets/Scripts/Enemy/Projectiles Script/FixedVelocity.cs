using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedVelocity : MonoBehaviour
{
    public float fixedVelocity = 5f; // Desired fixed velocity for the projectile
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Calculate the current velocity magnitude
        float currentVelocity = rb.velocity.magnitude;

        // Calculate the normalized direction of the current velocity
        Vector2 normalizedVelocity = rb.velocity.normalized;

        // Update the velocity to maintain the fixed velocity
        rb.velocity = normalizedVelocity * fixedVelocity;
    }
}

