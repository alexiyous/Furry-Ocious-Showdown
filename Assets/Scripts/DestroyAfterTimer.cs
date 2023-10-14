using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DestroyAfterTimer : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 2f;


    private void OnEnable()
    {
        //ReturnToPoolAfterTime();

        StartCoroutine(ReturnToPoolAfterTimeCoroutine());

    }

    /*private void OnDisable()
    {
    }*/

    /*private async void ReturnToPoolAfterTime()
    {
        await Task.Delay((int)(timeToDestroy * 1000));
        ObjectPoolManager.ReturnObjectPool(gameObject);
    }*/

    private IEnumerator ReturnToPoolAfterTimeCoroutine()
    {
        yield return new WaitForSeconds(timeToDestroy);
        ObjectPoolManager.ReturnObjectPool(gameObject);
    }  
}
