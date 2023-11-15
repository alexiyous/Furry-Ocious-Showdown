using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool hasWon = false;
    public bool hasLost = false;

    [HideInInspector] public bool beginGame;
    public int DebugCoin = 999999;
    public bool isTargeting = false;

    public bool isNight;

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            /*DontDestroyOnLoad(gameObject);*/
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Log(Time.timeScale);
        beginGame = true;
    }

    public IEnumerator Defeat()
    {
        if(!hasWon)
        {
            hasLost = true;
            PauseHandler.ableToPause = false;

            yield return new WaitForSecondsRealtime(1f);

            SceneTransitionHandler.instance.EndTransition("Defeat");
        }
    }
}
