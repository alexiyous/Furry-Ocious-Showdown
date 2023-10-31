using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsHandler : MonoBehaviour
{
    public GameObject skipButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !skipButton.activeInHierarchy)
        {
            skipButton.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && skipButton.activeInHierarchy)
        {
            OpenMainScene();
        }
    }

    public void OpenMainScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
