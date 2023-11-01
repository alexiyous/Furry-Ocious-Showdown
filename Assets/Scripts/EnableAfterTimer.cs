using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterTimer : MonoBehaviour
{
    public GameObject objectToEnable = null;

    public float timer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableAfter(timer));
    }

    public virtual IEnumerator EnableAfter(float time)
    {
        if(objectToEnable == null)
        {
            Debug.LogWarning("No object to enable!");
            yield break;
        }

        yield return new WaitForSeconds(time);

        objectToEnable.SetActive(true);
    }
}
