using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public RectTransform uiElement;

    public Vector3 targetPosition; // The target position you want to move the UI element to
    public Vector3 tutorialPosition;

    public float duration = 1.0f; // Duration of the movement
    public Ease easeType = Ease.OutQuad; // Easing type (you can change this in the Inspector)
    public Vector3 hidePosition;

    private void Start()
    {
        /*AudioManager.instance.PlayMainMenuMusic();*/
    }
    public void playGame()
    {
        SceneManager.LoadScene(1);

    }

    public void quitGame()
    {
        Application.Quit();
    }
}
