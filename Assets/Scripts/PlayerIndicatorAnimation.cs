using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicatorAnimation : MonoBehaviour
{
    public Transform parent; // Reference to the parent object
    public float floatSpeed = 2f; // Speed of the up and down movement
    public float floatAmplitude = 0.5f; // Amplitude of the up and down movement

    private Vector3 initialPosition;
    private float timeOffset;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        timeOffset = UnityEngine.Random.Range(0f, 2f * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        if (parent != null)
        {
            // Follow the parent's X and Y position
            Vector3 newPosition = parent.position;
            newPosition.z = transform.position.z;
            transform.position = newPosition;

            // Apply the up and down movement
            float yOffset = floatAmplitude * Mathf.Sin((Time.time + timeOffset) * floatSpeed);
            transform.position = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        }
    }
}
