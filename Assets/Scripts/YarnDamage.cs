using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YarnDamage : MonoBehaviour
{
    [SerializeField] private int damage = 50;
    [SerializeField] private int armorPenetration = 10;
    [SerializeField] private float slowAmount = 0.5f;
    [SerializeField] private float slowDuration = 2f;
    [SerializeField] private float knockbackDistance = 1f;
    [SerializeField] private int knockbackCount = 7;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);
            collision.GetComponent<ISlowable>().ApplySlow(slowAmount, slowDuration, Color.cyan);

            if(knockbackCount <= 0)
            {
                return;
            }

            Vector3 knockbackDirection = Vector3.right;
            Vector3 knockbackPosition = collision.transform.position + knockbackDirection * knockbackDistance;
            collision.transform.position = knockbackPosition;
            knockbackCount--;
        }
    }
}
