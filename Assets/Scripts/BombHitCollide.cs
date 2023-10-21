using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHitCollide : MonoBehaviour
{
    public int damage;
    public int armorPenetration;
    public GameObject parent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // IMPLEMENT TAKEDAMAGE TO ENEMY

            collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);

            Debug.Log("Hit");
        }
    }
}
