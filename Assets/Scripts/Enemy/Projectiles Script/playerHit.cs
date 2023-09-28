using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHit : MonoBehaviour
{
    public GameObject hitEffect;
    private bool isHit = false;

    public float expireTime = 7f;

    private void Start()
    {
        Destroy(gameObject, expireTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isHit)
        {
            return;
        }
        
        if (collision.CompareTag("Player Damage"))
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(1);
            isHit = true;
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
