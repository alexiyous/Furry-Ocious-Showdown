using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float randomTime;

    [SerializeField] private GameObject objectToSpawn;

    private float timer;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= timeBetweenSpawn)
        {
            timer = 0;

            Spawn();
        }
    }

    public Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
            );
    }

    public void Spawn()
    {
        Vector2 spawnPoint = RandomPointInBounds(boxCollider2D.bounds);

        ObjectPoolManager.SpawnObject(objectToSpawn, spawnPoint, Quaternion.identity);

        float randomTimeBetweenSpawn = Random.Range(timeBetweenSpawn - randomTime, timeBetweenSpawn + randomTime);

        timeBetweenSpawn = randomTimeBetweenSpawn;
    }
}
