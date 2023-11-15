using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser1 : MonoBehaviour
{
    public GameObject laserObject;

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player Damage").transform;
    }

    // Update is called once per frame
    void Update()
    {

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        laserObject.transform.localScale = new Vector3(distance * 2.83f, laserObject.transform.localScale.y, laserObject.transform.localScale.z);
    }
}
