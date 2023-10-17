using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    private Enemy _enemy;

   [field: SerializeField] public TargetType targetType { get; set; }

    public enum TargetType
    {
        player,
        building, 
        all
    }


    private void Awake()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (targetType)
        {
            case TargetType.player:
                if (collision.CompareTag("Player"))
                {
                    _enemy.SetAggroStatus(true);
                }
                break;
            case TargetType.building:
                if (collision.CompareTag("Building"))
                {
                    _enemy.SetAggroStatus(true);
                }
                break;
            case TargetType.all:
                if (collision.CompareTag("Building") || collision.CompareTag("Player"))
                {
                    _enemy.SetAggroStatus(true);
                }
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        switch (targetType)
        {
            case TargetType.player:
                if (collision.CompareTag("Player"))
                {
                    _enemy.SetAggroStatus(false);
                }
                break;
            case TargetType.building:
                if (collision.CompareTag("Building"))
                {
                    _enemy.SetAggroStatus(false);
                }
                break;
            case TargetType.all:
                if (collision.CompareTag("Building") || collision.CompareTag("Player"))
                {
                    _enemy.SetAggroStatus(false);
                }
                break;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (targetType)
        {
            case TargetType.player:
                if (collision.CompareTag("Player"))
                {
                    _enemy.SetAggroStatus(true);
                }
                break;
            case TargetType.building:
                if (collision.CompareTag("Building"))
                {
                    _enemy.SetAggroStatus(true);
                }
                break;
            case TargetType.all:
                if (collision.CompareTag("Building") || collision.CompareTag("Player"))
                {
                    _enemy.SetAggroStatus(true);
                }
                break;
        }
    }
}
