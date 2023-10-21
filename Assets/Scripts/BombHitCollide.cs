using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHitCollide : MonoBehaviour
{
    public int damage;
    public GameObject parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // IMPLEMENT TAKEDAMAGE TO ENEMY

            Debug.Log("Hit");
        }
    }
}
