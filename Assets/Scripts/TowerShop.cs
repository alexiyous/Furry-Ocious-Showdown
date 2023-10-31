using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using DG.Tweening;


public class TowerShop : MonoBehaviour
{
    [ListDrawerSettings(ShowIndexLabels = true)]
    [SerializeField] private GameObject[] towerPrefab;
    [ListDrawerSettings(ShowIndexLabels = true)]
    [SerializeField] private Button[] towerSelect;
    [ListDrawerSettings(ShowIndexLabels = true)]
    [SerializeField] private int[] towerCost;
    [ListDrawerSettings(ShowIndexLabels = true)]
    [SerializeField] private TextMeshProUGUI[] towerCostText;

    private Vector3[] originalSizes;
    private Vector3 originPosition;

    public static Transform currentSpawnPoint;
    public static GameObject currentSpawner;
    public Transform destinationPosition;
    public Image dimBackgeound;

    public TextMeshProUGUI notificationText;
    private Tween notificationTextTween;

    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
        originalSizes = new Vector3[towerSelect.Length];
        for (int i = 0; i < towerSelect.Length; i++)
        {
            int index = i;
            SetCost(index);
            towerSelect[i].onClick.AddListener(() => SpawnTower(index));
            originalSizes[i] = towerSelect[i].GetComponent<RectTransform>().localScale;
        }
    }

    private void Update()
    {
        ChangeSizeButton();
    }

    private void OnEnable()
    {
        dimBackgeound.gameObject.SetActive(true);
        PlayerController.canMove = false;
        transform.DOMove(destinationPosition.position, 0.5f).SetUpdate(true);
        dimBackgeound.DOFade(0.5f, 0.5f).From(0f);
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
        PlayerController.canMove = true;
        dimBackgeound.DOFade(0f, 0.5f);
        transform.DOMove(originPosition, 0.5f).SetUpdate(true).OnComplete(() => 
        { 
            gameObject.SetActive(false);
            dimBackgeound.gameObject.SetActive(false);
        });
    }

    public void SpawnTower(int index)
    {
        if (ScoreManager.instance.currentScore < towerCost[index])
        {
            if (notificationTextTween != null)
            {
                // If a previous animation is in progress, stop it and set alpha to fully visible
                notificationTextTween.Kill();

            }
            notificationText.color = new Color(notificationText.color.r, notificationText.color.g, notificationText.color.b, 1f);

            notificationTextTween = notificationText.DOFade(0f, 3f);

            return;
        }
        ScoreManager.instance.currentScore -= towerCost[index];
        PlayerController.canMove = true;
        Instantiate(towerPrefab[index], currentSpawnPoint.position, Quaternion.identity);
        AudioManager.instance.PlaySFXAdjusted(34);
        currentSpawnPoint = null;
        Destroy(currentSpawner);
        currentSpawner = null;
        dimBackgeound.DOFade(0f, 0.5f);
        transform.DOMove(originPosition, 0.5f).SetUpdate(true).OnComplete(() =>
        {
            gameObject.SetActive(false);
            dimBackgeound.gameObject.SetActive(false);
        });
    }

    public void SetCost(int index)
    {
        towerCostText[index].text += towerCost[index].ToString();
    }

    
}
