using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    public GameObject pauseMenu = null;

    [HideInInspector]
    public static bool isPaused = false;

    public static bool ableToPause = true;

    private void Update()
    {
        // Check if the "Esc" key is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !TutorialHandler.isTutorialActive && ableToPause)
        {
            if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        if(!ableToPause) return;
        if(TutorialHandler.isTutorialActive) return;

        AudioManager.instance.PlaySFXAdjusted(38);

        pauseMenu.SetActive(true);

        isPaused = true;

        Time.timeScale = 0;

    }

    public void Unpause()
    {
        AudioManager.instance.PlaySFXAdjusted(39);

        pauseMenu.SetActive(false);

        isPaused = false;

        Time.timeScale = 1;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        isPaused = false;
        SceneTransitionHandler.instance.EndTransition("Main Menu");
    }
}
