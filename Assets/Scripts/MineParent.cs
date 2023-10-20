using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineParent : MonoBehaviour
{
    private Transform bridgeRoad;

    private float undersideYPosition;
    private float topsideYPosition;

    public Transform[] landmines; // Assign the child landmine objects to this array in the Inspector.
    public float randomRange = 1.0f;

    public float moveSpeed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        bridgeRoad = GameObject.FindGameObjectWithTag("Bridge Road").transform;

        undersideYPosition = (bridgeRoad.position.y - bridgeRoad.localScale.y / 2f) + .5f;
        topsideYPosition = (bridgeRoad.position.y + bridgeRoad.localScale.y / 2f) - .5f;

        ShootMinesToRandomPosition();
    }


    void ShootMinesToRandomPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        foreach (Transform landmine in landmines)
        {
            float randomX = Random.Range(-randomRange, randomRange);
            float randomY = Random.Range(-randomRange, randomRange);

            Vector3 targetPosition = mousePosition + new Vector3(randomX, randomY, 0);

            targetPosition.y = Mathf.Clamp(targetPosition.y, undersideYPosition, topsideYPosition);
            targetPosition.z = 0;

            float distance = Vector3.Distance(landmine.position, targetPosition);
            float duration = distance / moveSpeed;

            landmine.DOMove(targetPosition, duration);
        }
    }
}
