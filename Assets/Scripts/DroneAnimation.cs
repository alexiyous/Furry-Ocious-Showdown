using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : DroneController
{
    public Transform orbitCenter; // The center of the orbit.
    public float orbitSpeed = 199f; // Speed of the orbit in degrees per second.
    public float orbitRadius = 0.8f; // Radius of the orbit.

    public bool isFacingRight = true;

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the desired position based on orbit.
        float orbitAngle = Time.time * orbitSpeed;
        Vector3 orbitPosition = Quaternion.Euler(0, 0, orbitAngle) * 
            (Vector3.right * orbitRadius);
        Vector3 desiredPosition = orbitCenter.position + orbitPosition;

        // Apply the orbiting position and maintain the relative offset to the parent.
        transform.position = desiredPosition + originalOffset;
        /*gameObject.transform.rotation = player.rotation;*/

        Vector3 direction = (mousePosition - transform.position).normalized;

        if(direction.x > 0 && !isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = true;
        }
        else if(direction.x < 0 && isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = false;
        }
    }
}
