using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBubble : MonoBehaviour
{
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private float timeBetweenShots;
    
    private float timer;
    private int bulletIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        // If the timer has reached zero or below, fire a bullet and reset the timer.
        if (timer <= 0f)
        {
            if (bulletIndex >= bullets.Length) return;

            FireBullet();
            timer = timeBetweenShots; // Reset the timer.
        }
    }

    void FireBullet()
    {
        if (bulletIndex < bullets.Length)
        {
            GameObject bulletToFire = bullets[bulletIndex];
            bulletToFire.SetActive(true);
            AudioManager.instance.PlaySFXAdjusted(7);
            

            bulletIndex++; // Move to the next bullet in the array.
        }
        else
        {
            // If we've fired all bullets, reset the index to the beginning of the array.
            bulletIndex = 0;
        }
    }
}
