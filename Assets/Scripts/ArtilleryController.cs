using DG.Tweening;
using System.Collections;
using UnityEngine;


public class ArtilleryController : MonoBehaviour
{
    private Transform bridgeRoad;
    private float undersideYPosition;
    private float topsideYPosition;

    [SerializeField] private GameObject targetAim;
    public string type = "Event";
    [SerializeField] private GameObject flare;
    [SerializeField] private GameObject artilleryBullet;
    [SerializeField] private Collider2D hitCollider;
    [SerializeField] private float YOffsetSpawnPointParent;
    [SerializeField] private Transform[] artilleryBulletSpawnPoints;

    private GameObject flareObject;
    private bool isFlareActive = false;
    private int bulletCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.rotation = Quaternion.identity;
        GameManager.instance.isTargeting = true;
        bridgeRoad = GameObject.FindGameObjectWithTag("Bridge Road").transform;
        undersideYPosition = bridgeRoad.position.y - bridgeRoad.localScale.y / 2f;
        topsideYPosition = bridgeRoad.position.y + bridgeRoad.localScale.y / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        TargetFollowMouseWithinBridge();


        if (Input.GetMouseButtonDown(0) && !isFlareActive)
        {
            var currentTargetPosition = targetAim.transform.position;
            isFlareActive = true;
            StartCoroutine(ShootFlare(currentTargetPosition));
        }
    }

    public IEnumerator ShootFlare(Vector3 currentTargetPosition)
    {
        targetAim.transform.position = currentTargetPosition;
        var droneAim = GameObject.FindGameObjectWithTag("Drone Aim").GetComponent<DroneAim>();
        flareObject = Instantiate(flare, droneAim.transform.position, droneAim.transform.rotation);
        flareObject.SetActive(true);
        flareObject.transform.DOMove(new Vector3(currentTargetPosition.x, currentTargetPosition.y, 0f),
            Vector2.Distance(flareObject.transform.position, currentTargetPosition) / 10f).SetEase(Ease.Linear);
        yield return new WaitForSeconds(2f);
        Destroy(flareObject);

        StartCoroutine(ShootArtillery(currentTargetPosition));
    }

    public IEnumerator ShootArtillery(Vector3 currentTargetPosition)
    {
        artilleryBulletSpawnPoints[1].parent.position = new Vector3(currentTargetPosition.x, currentTargetPosition.y + YOffsetSpawnPointParent, 0f);

        while (bulletCount < 5)
        {
            var randomSpawnPoint = artilleryBulletSpawnPoints[Random.Range(0, artilleryBulletSpawnPoints.Length)];
            var artilleryBulletInstance = Instantiate(artilleryBullet, randomSpawnPoint.position, Quaternion.identity);
            artilleryBulletInstance.SetActive(true);
            if (artilleryBulletInstance == null) continue;
            artilleryBulletInstance.transform.DOMove(new Vector3(currentTargetPosition.x, currentTargetPosition.y, 0f),
                Vector2.Distance(artilleryBulletInstance.transform.position, currentTargetPosition) / 10f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    hitCollider.enabled = true;
                    Destroy(artilleryBulletInstance);
                });
            yield return new WaitForSeconds(1f);
            bulletCount++;
        }
        GameManager.instance.isTargeting = false;
        Destroy(gameObject);
    }

    private void TargetFollowMouseWithinBridge()
    {
        if (isFlareActive || (targetAim == null)) return;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z - targetAim.transform.position.z;
        Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        targetPosition.y = Mathf.Clamp(targetPosition.y, undersideYPosition, topsideYPosition);
        targetAim.transform.position = new Vector3(targetPosition.x, targetPosition.y, targetAim.transform.position.z);
    }
}
