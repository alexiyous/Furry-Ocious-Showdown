using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCollider : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true; // Ensure that it's a trigger collider.

        UpdateCollider();
    }

    void Update()
    {
        UpdateCollider();
    }

    void UpdateCollider()
    {
        // Get the positions of the LineRenderer
        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(linePositions);

        // Calculate the bounds of the Box Collider
        Bounds bounds = new Bounds(linePositions[0], Vector3.zero);
        for (int i = 1; i < linePositions.Length; i++)
        {
            bounds.Encapsulate(linePositions[i]);
        }

        // Update the Box Collider size and center
        boxCollider.size = new Vector2(bounds.size.x, bounds.size.y);
        boxCollider.offset = bounds.center - transform.position;
    }
}
