using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject playerTarget { get; set; }
    public GameObject[] buildingTargets { get; set; }
    private Enemy _enemy;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        buildingTargets = GameObject.FindGameObjectsWithTag("Building");

        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == playerTarget)
        {
            _enemy.SetAggroStatus(true);
        }
        else
        {
            foreach (GameObject buildingTarget in buildingTargets)
            {
                if (collision.gameObject == buildingTarget)
                {
                    _enemy.SetAggroStatus(true);
                    break;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerTarget)
        {
            _enemy.SetAggroStatus(false);
        }
        else
        {
            foreach (GameObject buildingTarget in buildingTargets)
            {
                if (collision.gameObject == buildingTarget)
                {
                    _enemy.SetAggroStatus(false);
                    break;
                }
            }
        }
    }
}
