using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("References")]
    public Transform turretRotationPoint;
    public LayerMask enemyLayerMask;
    public GameObject bulletPrefab;
    public Transform firingPoint;

    [Header("Attributes")]
    public float targetingRange;
    public float rotationSpeed = 5f;
    public float bps = 1f; // Bullets per second
    public Vector2 targetingSize = new Vector2(4f, 4f); // Adjust the size as needed
    public Vector3 centerOffset = new Vector3(0f, 0f, 0f); // Adjust the offset as needed

    [HideInInspector] public Transform target;
    [HideInInspector] public float timeUntilFire;

    // Update is called once per frame
    public void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        
        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    public virtual void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, turretRotationPoint.rotation);
        TowerBullet bulletScript = bulletObj.GetComponent<TowerBullet>();
        bulletScript.SetTarget(target);
    }

    public virtual void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + centerOffset, targetingSize, 0f, enemyLayerMask);

        if (colliders.Length > 0)
        {
            // Find the closest target within the box
            float closestDistance = float.MaxValue;
            Collider2D closestTarget = null;

            foreach (Collider2D collider in colliders)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = collider;
                }
            }

            if (closestTarget != null)
            {
                target = closestTarget.transform;
            }
        }
    }

    public virtual bool CheckTargetIsInRange()
    {
        if (target == null)
        {
            return false;
        }

        // Calculate the half-size of the box-shaped detection area
        Vector2 halfSize = targetingSize * 0.5f;

        // Calculate the bounds of the box-shaped detection area
        Bounds detectionBounds = new Bounds(transform.position + centerOffset, targetingSize);

        // Check if the target's position is within the detection bounds
        return detectionBounds.Contains(target.position);
    }

    public virtual void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y,
                       target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation,
            targetRotation, rotationSpeed * Time.deltaTime);

    }

    public virtual void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireCube(transform.position + centerOffset, targetingSize);
    }
    
}
