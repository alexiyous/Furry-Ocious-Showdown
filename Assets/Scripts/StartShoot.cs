using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartShoot : MonoBehaviour
{
    [SerializeField] private float fireRate = .5f;
    [SerializeField] private int bulletCountMin = 3;
    [SerializeField] private int bulletCountMax = 8;
    [SerializeField] private float inaccuracy = 1f;
    [SerializeField] private GameObject bulletPrefab;

    private Transform target;

    private int bulletCount;

    [SerializeField] private Transform shootPoint;

    [SerializeField] private float timer = 2f;

    private bool hasShot = false;

    private void OnEnable()
    {

        hasShot = false;
        timer = 2f;

        bulletCount = Random.Range(bulletCountMin, bulletCountMax);

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && !hasShot)
        {
            hasShot = true;

            StartCoroutine(Shoot2());

            timer = 2f;
        }
    }

    private IEnumerator Shoot2()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 shootInaccuracy = new Vector3(0, Random.Range(-inaccuracy, inaccuracy), 0);

            Vector2 direction = (target.position - shootPoint.position + shootInaccuracy).normalized;

            GameObject bullet = ObjectPoolManager.SpawnObject(bulletPrefab, shootPoint.position, Quaternion.identity, ObjectPoolManager.PoolType.Gameobject);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the bullet to face the player's position
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            yield return new WaitForSeconds(fireRate);
        }
    }
}
