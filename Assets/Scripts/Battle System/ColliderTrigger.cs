using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : MonoBehaviour
{

    public event EventHandler OnPlayerEnterTrigger;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Player inside");

            
            OnPlayerEnterTrigger?.Invoke(this, EventArgs.Empty);
        }
    }
}
