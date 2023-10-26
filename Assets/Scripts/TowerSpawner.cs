using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private TowerShop towerSelectionUI;
    [SerializeField] private GameObject SpawnNotif;
    [SerializeField] private SpriteRenderer buildPlace;
    [SerializeField] private Transform towerSpawnPoint;
    private Vector3 originNotifPosition;
    private Vector3 originBuildPlacePosition;
    private bool isInRange = false;

    private void Start()
    {
        originBuildPlacePosition = buildPlace.transform.position;
        Vector3 _target = buildPlace.transform.position + Vector3.up * 0.5f;
        buildPlace.transform.DOMove(_target, 1f, false).SetLoops(-1, LoopType.Yoyo);
    }

    private void Update()
    {
        if (!isInRange)
        {
            buildPlace.gameObject.SetActive(true);
        }
        else
        {
            buildPlace.gameObject.SetActive(false);
            buildPlace.transform.position = originNotifPosition;
        }

        if (Input.GetKeyDown(KeyCode.B) && isInRange)
        {
            SetTower();
        }
    }

    public void SetTower()
    {
        towerSelectionUI.gameObject.SetActive(true);
        TowerShop.currentSpawnPoint = towerSpawnPoint;
        TowerShop.currentSpawner = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnNotif.SetActive(true);
            originNotifPosition = SpawnNotif.transform.position;
            Vector3 _target = SpawnNotif.transform.position + Vector3.up * 0.5f;
            SpawnNotif.transform.DOMove(_target, 0.5f, false).SetLoops(-1, LoopType.Yoyo);
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SpawnNotif.transform.DOKill();
            SpawnNotif.transform.position = originNotifPosition;
            SpawnNotif.SetActive(false);
            isInRange = false;
        }
    }
}
