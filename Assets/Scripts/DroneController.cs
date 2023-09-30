using UnityEngine;

public class DroneController : MonoBehaviour
{
    [HideInInspector] public Transform player; // Reference to the player's transform.
    public float followSpeed = 2.0f; // Speed at which the object follows the player.
    public float followDistance = 2.0f; // Minimum distance to maintain from the player.

    private void Awake()
    {
        // Find the player in the scene.
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        // Calculate the direction from the object to the player.
        Vector3 directionToPlayer = player.position - transform.position;

        // Calculate the distance to the player.
        float distanceToPlayer = directionToPlayer.magnitude;

        // Only follow if the distance is greater than the follow distance.
        if (distanceToPlayer > followDistance)
        {
            // Calculate the movement direction.
            Vector3 moveDirection = directionToPlayer.normalized;

            // Calculate the movement amount.
            float moveAmount = Mathf.Min(followSpeed * Time.deltaTime, distanceToPlayer - followDistance);

            // Move the object toward the player.
            transform.position += moveDirection * moveAmount;
        }
    }
}
