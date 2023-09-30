using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : DroneController
{
    public Transform orbitCenter; // The center of the orbit.
    public float orbitSpeed = 199f; // Speed of the orbit in degrees per second.
    public float orbitRadius = 0.8f; // Radius of the orbit.

    private Vector3 originalOffset;

    private void Start()
    {
        originalOffset = transform.position - orbitCenter.position;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Calculate the desired position based on orbit.
        float orbitAngle = Time.time * orbitSpeed;
        Vector3 orbitPosition = Quaternion.Euler(0, 0, orbitAngle) * 
            (Vector3.right * orbitRadius);
        Vector3 desiredPosition = orbitCenter.position + orbitPosition;

        // Apply the orbiting position and maintain the relative offset to the parent.
        transform.position = desiredPosition + originalOffset;
        gameObject.transform.rotation = player.rotation;
    }
}
