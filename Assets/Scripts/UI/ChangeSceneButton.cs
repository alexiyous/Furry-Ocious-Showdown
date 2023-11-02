using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public string sceneName;

    public void ChangeScene()
    {
        SceneTransitionHandler.instance.EndTransition(sceneName);
    }

    public void Reset()
    {
        GameManager.instance.hasWon = false;
        GameManager.instance.hasLost = false;
        PauseHandler.isPaused = false;
    }
}
