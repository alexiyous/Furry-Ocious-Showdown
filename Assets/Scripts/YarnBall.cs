using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class YarnBall : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;

    [SerializeField] private float moveSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

        float distance = Vector3.Distance(startPos.position, endPos.position);
        float duration = distance / moveSpeed;

        transform.position = startPos.position;
        transform.DOMove(endPos.position, duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
