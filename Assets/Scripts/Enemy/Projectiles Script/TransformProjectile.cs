using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformProjectile : MonoBehaviour
{
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void FixedUpdate()
    {
        // Calculate the new position based on the transform's right direction and speed
        Vector3 newPosition = transform.position + transform.right * speed * Time.deltaTime;

        // Update the position of the transform
        transform.position = newPosition;
    }
}
