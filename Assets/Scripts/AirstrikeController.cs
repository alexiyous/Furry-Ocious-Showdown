using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirstrikeController : MonoBehaviour
{
    private Transform bridgeRoad;
    private float undersideYPosition;
    private float topsideYPosition;

    [SerializeField] private GameObject targetAim;
    [SerializeField] private GameObject flare;
    [SerializeField] private GameObject airstrikeBullet;
    [SerializeField] private Collider2D hitCollider;
    [SerializeField] private Vector2 OffsetSpawnPointParent;
    [SerializeField] private Transform[] airstrikeBulletSpawnPoints;
    [SerializeField] private Transform[] airstrikeBulletTargetHits1;
    [SerializeField] private Transform[] airstrikeBulletTargetHits2;

    public GameObject flareEffect;

    public GameObject hitEffect;

    private GameObject flareObject;
    private bool isFlareActive = false;

    public float moveSpeed = 5f;

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
        AudioManager.instance.PlaySFXAdjusted(22);

        airstrikeBulletSpawnPoints[1].parent.position = new Vector3(currentTargetPosition.x + OffsetSpawnPointParent.x, currentTargetPosition.y + OffsetSpawnPointParent.y, 0f);
        List<GameObject> airstrikeBulletInstance = new List<GameObject>();
        int i = 0;
        while ( i < airstrikeBulletTargetHits1.Length)
        {

            airstrikeBulletInstance.Add(Instantiate(airstrikeBullet, airstrikeBulletSpawnPoints[0].position, Quaternion.identity));
            airstrikeBulletInstance.Add(Instantiate(airstrikeBullet, airstrikeBulletSpawnPoints[1].position, Quaternion.identity));
            foreach (GameObject airstrikeInstance in airstrikeBulletInstance)
            {
                airstrikeInstance.SetActive(true);
                var t = airstrikeInstance.GetComponentInChildren<CircleCollider2D>();
                t.enabled = true;
            }
            airstrikeBulletInstance[0].transform.DOMove(new Vector3(airstrikeBulletTargetHits1[i].position.x, airstrikeBulletTargetHits1[i].position.y, 0f),
                Vector2.Distance(airstrikeBulletInstance[0].transform.position, airstrikeBulletTargetHits1[i].position) / 100f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    

                });

            airstrikeBulletInstance[1].transform.DOMove(new Vector3(airstrikeBulletTargetHits2[i].position.x, airstrikeBulletTargetHits2[i].position.y, 0f),
                Vector2.Distance(airstrikeBulletInstance[1].transform.position, airstrikeBulletTargetHits2[i].position) / 100f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    
                });

            yield return new WaitForSeconds(0.1f);
            StartCoroutine(DestroyAfterHit(airstrikeBulletInstance[0], airstrikeBulletTargetHits1[i]));
            StartCoroutine(DestroyAfterHit(airstrikeBulletInstance[1], airstrikeBulletTargetHits1[i]));
            airstrikeBulletInstance.Clear();
            i++;
            
        }
        Destroy(flareObject);
        Destroy(gameObject);
    }

    public IEnumerator DestroyAfterHit(GameObject artilleryBulletInstance, Transform airstrikeBulletTargetHits)
    {
        var artilTemp = artilleryBulletInstance;
        var artilTarget = airstrikeBulletTargetHits;
        ObjectPoolManager.SpawnObject(hitEffect, artilTarget.position, Quaternion.identity, ObjectPoolManager.PoolType.ParticleSystem);
        if (artilTemp == null) yield break;
        /*artilleryBulletInstance.GetComponentInChildren<CircleCollider2D>().enabled = true;*/
        Destroy(artilTemp, 1f);
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
