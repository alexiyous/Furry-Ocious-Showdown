using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttackHelicopter : Enemy
{
    public Transform shootPoint1;
    public Transform shootPoint2;

    public Transform[] movePoints;
    public Transform[] teleportPoints;

    public GameObject machineGun;

    public Transform playerTransform;

    public Transform[] buildingsTransform;

    public override void Start()
    {
        base.Start();

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        buildingsTransform = GameObject.FindGameObjectsWithTag("Building")
                            .Select(building => building.transform)
                            .ToArray();
    }

    public override void Die()
    {
        base.Die();

        machineGun.SetActive(false);
    }
}
