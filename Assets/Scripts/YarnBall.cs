using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YarnBall : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [SerializeField] private float moveSpeed = 10.0f;
    public float rotationSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = GameObject.Find("Yarn Start Position").transform;
        endPos = GameObject.Find("Yarn End Position").transform;

        float distance = Vector3.Distance(startPos.position, endPos.position);
        float duration = distance / moveSpeed;

        transform.position = startPos.position;
        transform.DOMove(endPos.position, duration);

        transform.DOLocalRotate(new Vector3(0, 0, -360 * rotationSpeed), duration, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Incremental)
            .SetSpeedBased(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
