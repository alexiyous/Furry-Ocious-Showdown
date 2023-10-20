using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryBombHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // IMPLEMENT TAKEDAMAGE TO ENEMY

            Debug.Log("Hit");
            Destroy(gameObject.GetComponentInParent<Transform>().gameObject, 0.5f);
        }
    }
}
