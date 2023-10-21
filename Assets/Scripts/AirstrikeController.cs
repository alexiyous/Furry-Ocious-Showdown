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

    private GameObject flareObject;
    private bool isFlareActive = false;

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
        GameManager.instance.isTargeting = false;
        StartCoroutine(ShootArtillery(currentTargetPosition));
    }

    public IEnumerator ShootArtillery(Vector3 currentTargetPosition)
    {
        airstrikeBulletSpawnPoints[1].parent.position = new Vector3(currentTargetPosition.x + OffsetSpawnPointParent.x, currentTargetPosition.y + OffsetSpawnPointParent.y, 0f);

        for (int i = 0; i < airstrikeBulletTargetHits1.Length; i++)
        {
            List<GameObject> airstrikeBulletInstance = new List<GameObject>();
            airstrikeBulletInstance.Add(Instantiate(airstrikeBullet, airstrikeBulletSpawnPoints[0].position, Quaternion.identity));
            airstrikeBulletInstance.Add(Instantiate(airstrikeBullet, airstrikeBulletSpawnPoints[1].position, Quaternion.identity));
            foreach (GameObject airstrikeInstance in airstrikeBulletInstance)
            {
                airstrikeInstance.SetActive(true);
            }
            airstrikeBulletInstance[0].transform.DOMove(new Vector3(airstrikeBulletTargetHits1[i].position.x, airstrikeBulletTargetHits1[i].position.y, 0f),
                Vector2.Distance(airstrikeBulletInstance[0].transform.position, airstrikeBulletTargetHits1[i].position) / 30f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Destroy(airstrikeBulletInstance[0], 0.1f);
                    hitCollider.enabled = true;
                });
            airstrikeBulletInstance[1].transform.DOMove(new Vector3(airstrikeBulletTargetHits2[i].position.x, airstrikeBulletTargetHits2[i].position.y, 0f),
                Vector2.Distance(airstrikeBulletInstance[0].transform.position, airstrikeBulletTargetHits2[i].position) / 30f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    Destroy(airstrikeBulletInstance[1], 0.1f);
                    hitCollider.enabled = true;
                });
            yield return new WaitForSeconds(0.1f);
        }

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
