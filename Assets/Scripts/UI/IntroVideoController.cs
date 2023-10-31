using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad;

    public GameObject skipButton;

    // Start is called before the first frame update
    void Start()
    {

        videoPlayer.loopPointReached += VideoEnded;
    }

    private void Update()
    {
        if(Input.anyKeyDown && !skipButton.activeInHierarchy)
        {
            skipButton.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && skipButton.activeInHierarchy)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    private void VideoEnded(VideoPlayer vp)
    {
        // Unsubscribe from the event to prevent multiple calls
        vp.loopPointReached -= VideoEnded;

        // Load the specified scene
        SceneManager.LoadScene(sceneToLoad);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
