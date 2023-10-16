using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerShop : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefab;
    [SerializeField] private Button[] towerSelect;

    public static Transform currentSpawnPoint;
    public static GameObject currentSpawner;

    // Start is called before the first frame update
    void Start()
    {
        for (int i= 0; i < towerSelect.Length; i++)
        {
            towerSelect[i].onClick.AddListener(() => SpawnTower(i));
        }
    }

    private void SpawnTower(int index)
    {
        Instantiate(towerPrefab[index], currentSpawnPoint.position, Quaternion.identity);
        currentSpawnPoint = null;
        Destroy(currentSpawner);
        currentSpawner = null;
    }
}
