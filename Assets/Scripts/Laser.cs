using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform laserFirePoint;

    // Start is called before the first frame update
    void Start()
    {
        laserFirePoint = GameObject.FindGameObjectWithTag("Drone Aim").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0) return;

        ShootLaser();
    }

    void ShootLaser()
    {
        transform.position = laserFirePoint.position;
        transform.rotation = laserFirePoint.rotation;
    }
}
