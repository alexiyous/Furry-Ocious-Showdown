using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShoot : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float bulletSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * bulletSpeed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
