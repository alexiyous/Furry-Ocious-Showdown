using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float _timeToDestroy = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _timeToDestroy);
    }

}
