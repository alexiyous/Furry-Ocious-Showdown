using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryBullet : MonoBehaviour
{
    [SerializeField] private Collider2D hitCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Instantiate Bomb Effect

            hitCollider.enabled = true;
            
        }
    }
    
}
