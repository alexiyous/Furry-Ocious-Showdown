using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerSpawner : MonoBehaviour
{
    [HideInInspector] public bool isPlaced = false;

    [SerializeField] private TowerShop towerSelectionUI;
    [SerializeField] private Button button;
    [SerializeField] private Transform towerSpawnPoint;

    private void Start()
    {
        button.onClick.AddListener(SetTower);
    }

    public void SetTower()
    {
        towerSelectionUI.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlaced)
        {
            button.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPlaced)
        {
            button.gameObject.SetActive(false);
        }
    }
}
