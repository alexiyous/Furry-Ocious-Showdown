using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveObject : MonoBehaviour
{
    public Enemy[] enemies;

    private void Awake()
    {
        enemies = GetComponentsInChildren<Enemy>(true);
    }
}
