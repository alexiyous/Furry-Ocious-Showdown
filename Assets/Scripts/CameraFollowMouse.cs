using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CameraFollowMouse : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform player;
    public float cameraSpeed = 5f; // Adjust the speed to your preference

    private void Update()
    {
        // Get the mouse cursor position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Calculate the midpoint between the mouse cursor and the player
        Vector2 targetPosition = (mousePosition + (Vector2)player.position) / 2f;
        // Check if the mouse cursor is heading down screen
        if (mousePosition.y < player.position.y)
        {
            // If the mouse cursor is heading down screen, slightly follow it
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position,
                new Vector3(targetPosition.x, player.position.y, -10f), cameraSpeed * Time.deltaTime);
        }
        else
        {
            // Use Lerp to smoothly move the camera to the target position
            virtualCamera.transform.position = Vector3.Lerp(virtualCamera.transform.position,
                new Vector3(targetPosition.x, targetPosition.y, -10f), cameraSpeed * Time.deltaTime);
        }
    }
}
