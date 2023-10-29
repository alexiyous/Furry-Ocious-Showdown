using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private float randomTime;

    [SerializeField] private GameObject objectToSpawn;

    private float randomTimeBetweenSpawn;

    private bool hasSpawned = false;

    private float timer = 0;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= randomTimeBetweenSpawn && !hasSpawned)
        {
            hasSpawned = true;
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

        randomTimeBetweenSpawn = Random.Range(timeBetweenSpawn - randomTime, timeBetweenSpawn + randomTime);

        hasSpawned = false;
    }
}
