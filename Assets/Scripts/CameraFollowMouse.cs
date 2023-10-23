using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform player;
    public float cameraSpeed = 5f; // Adjust the speed to your preference
    public BoxCollider2D cameraBounds; // Add a reference to the box collider

    private void Update()
    {
        // Get the mouse cursor position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the midpoint between the mouse cursor and the player
        Vector2 targetPosition = (mousePosition + (Vector2)player.position) / 2f;

        // Clamp the target position to the camera bounds
        float cameraHalfHeight = virtualCamera.m_Lens.OrthographicSize;
        float cameraHalfWidth = cameraHalfHeight * virtualCamera.m_Lens.Aspect;
        float clampedX = Mathf.Clamp(targetPosition.x, cameraBounds.bounds.min.x + cameraHalfWidth, cameraBounds.bounds.max.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(targetPosition.y, cameraBounds.bounds.min.y + cameraHalfHeight, cameraBounds.bounds.max.y - cameraHalfHeight);
        Vector2 clampedPosition = new Vector2(clampedX, clampedY);

        // Check if the mouse cursor is heading down screen
        if (mousePosition.y < player.position.y)
        {
            // If the mouse cursor is heading down screen, slightly follow it
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position,
                new Vector3(clampedPosition.x, player.position.y, -10f), cameraSpeed * Time.deltaTime);
        }
        else
        {
            // Use Lerp to smoothly move the camera to the target position
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position,
                new Vector3(clampedPosition.x, clampedPosition.y, -10f), cameraSpeed * Time.deltaTime);
        }
    }
}
