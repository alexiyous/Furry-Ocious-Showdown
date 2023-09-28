using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDamageProjectile : MonoBehaviour
{
    [SerializeField] private float explosionSize;
    [SerializeField] private float explosionTimer;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject explosionParticle;
    [SerializeField] private int damage = 400;
    [SerializeField] private int armorPenetration = 0;
    [SerializeField] private int explosionDamage = 150;
    [SerializeField] private int explosionArmorPenetration = 0;

    private bool isExploded;

    private CircleCollider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        isExploded = false;
        AudioManager.instance.PlaySFXAdjusted(2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isExploded)
        {
            explosionTimer -= Time.deltaTime;

            if (explosionTimer <= 0)
            {

                gameObject.SetActive(false);
                Instantiate(explosionParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _collider.radius = explosionSize;
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            if(!isExploded)
            {
                collision.GetComponent<IDamageable>().Damage(damage, armorPenetration);
                AudioManager.instance.PlaySFXAdjusted(9);
            } else
            {
                collision.GetComponent<IDamageable>().Damage(explosionDamage, explosionArmorPenetration);
            }

            collision.GetComponent<ISlowable>().ApplySlow(0.5f, 5f, Color.cyan);
            isExploded = true;
            explosionEffect.SetActive(true);
        } else if (collision.CompareTag("Tile"))
        {
            isExploded = true;
            explosionEffect.SetActive(true);
        }
    }

    private void OnDisable()
    {
        AudioManager.instance.PlaySFXAdjusted(9);
    }
}
