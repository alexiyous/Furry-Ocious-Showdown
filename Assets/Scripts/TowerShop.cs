using UnityEngine;
using UnityEngine.UI;




public class TowerShop : MonoBehaviour
{
    [SerializeField] private GameObject[] towerPrefab;
    [SerializeField] private Button[] towerSelect;

    private Vector3[] originalSizes;

    public static Transform currentSpawnPoint;
    public static GameObject currentSpawner;

    // Start is called before the first frame update
    void Start()
    {
        originalSizes = new Vector3[towerSelect.Length];
        for (int i = 0; i < towerSelect.Length; i++)
        {
            int index = i;
            towerSelect[i].onClick.AddListener(() => SpawnTower(index));
            originalSizes[i] = towerSelect[i].GetComponent<RectTransform>().localScale;
        }
    }

    private void Update()
    {
        ChangeSizeButton();
    }

    private void ChangeSizeButton()
    {

        for (int i = 0; i < towerSelect.Length; i++)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(towerSelect[i].GetComponent<RectTransform>(), Input.mousePosition))
            {
                towerSelect[i].GetComponent<RectTransform>().localScale = new Vector3(1.1f, 1.1f, 1.1f);
            }
            else if (towerSelect[i].GetComponent<RectTransform>().localScale != originalSizes[i])
            {
                towerSelect[i].GetComponent<RectTransform>().localScale = originalSizes[i];
            }
        }
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }

    public void SpawnTower(int index)
    {
        Instantiate(towerPrefab[index], currentSpawnPoint.position, Quaternion.identity);
        currentSpawnPoint = null;
        Destroy(currentSpawner);
        currentSpawner = null;
        gameObject.SetActive(false);
    }
}
