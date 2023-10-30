using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class ArtilleryController : MonoBehaviour
{
    private Transform bridgeRoad;
    private float undersideYPosition;
    private float topsideYPosition;

    [SerializeField] private GameObject targetAim;
    [SerializeField] private GameObject flare;
    [SerializeField] private GameObject artilleryBullet;
    [SerializeField] private float YOffsetSpawnPointParent;
    [SerializeField] private Transform[] artilleryBulletSpawnPoints;

    public GameObject flareEffect;
    public GameObject explosionEffect;

    private GameObject flareObject;
    private bool isFlareActive = false;
    private int bulletCount = 0;

    [SerializeField] private float xRadius = 4f;
    [SerializeField] private float yRadius = 1.5f;

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
            Vector2.Distance(flareObject.transform.position, currentTargetPosition) / 30f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                Debug.Log("Flare Reached Target");
                ObjectPoolManager.SpawnObject(flareEffect, flareObject.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
            });
        GameManager.instance.isTargeting = false;
        yield return new WaitForSeconds(4f);
        StartCoroutine(ShootArtillery(currentTargetPosition));
    }

    public IEnumerator ShootArtillery(Vector3 currentTargetPosition)
    {
        artilleryBulletSpawnPoints[1].parent.position = new Vector3(currentTargetPosition.x, currentTargetPosition.y + YOffsetSpawnPointParent, 0f);

        while (bulletCount < 5)
        {
            AudioManager.instance.PlaySFXAdjusted(21);

            var randomSpawnPoint = artilleryBulletSpawnPoints[Random.Range(0, artilleryBulletSpawnPoints.Length)];
            var artilleryBulletInstance = Instantiate(artilleryBullet, randomSpawnPoint.position, Quaternion.identity);
            artilleryBulletInstance.SetActive(true);
            if (artilleryBulletInstance == null) continue;
            artilleryBulletInstance.transform.DOMove(new Vector3(currentTargetPosition.x + Random.Range(-xRadius, xRadius), currentTargetPosition.y + Random.Range(-yRadius, yRadius), 0f),
                Vector2.Distance(artilleryBulletInstance.transform.position, currentTargetPosition) / 30f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    ObjectPoolManager.SpawnObject(explosionEffect, artilleryBulletInstance.transform.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
                    StartCoroutine(DestroyAfterHit(artilleryBulletInstance));
                });
            yield return new WaitForSeconds(1f);
            bulletCount++;
        }
        Destroy(flareObject);
        Destroy(gameObject);
    }

    public IEnumerator DestroyAfterHit(GameObject artilleryBulletInstance)
    {
        if (artilleryBulletInstance == null) yield break;
        /*yield return new WaitForSeconds(1f);*/
        var sprite = artilleryBulletInstance.GetComponentInChildren<SpriteRenderer>();
        var t = artilleryBulletInstance.GetComponentInChildren<CircleCollider2D>();
        t.enabled = true;
        sprite.enabled = false;
        /*artilleryBulletInstance.GetComponentInChildren<CircleCollider2D>().enabled = true;*/
        Destroy(artilleryBulletInstance, 0.3f);
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
