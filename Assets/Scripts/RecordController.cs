using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class RecordController : MonoBehaviour
{
    public string photoFolder = "Screenshots";
    public Image screenshotImage; // Reference to your UI Image component
    private GameObject screenshotPanel; // Reference to your panel containing the Image component

    private bool ableToSS = true;

    void Start()
    {
        // Ensure the recording folder exists
        System.IO.Directory.CreateDirectory(photoFolder);
        screenshotPanel = screenshotImage.gameObject.transform.parent.gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && ableToSS && PauseHandler.ableToPause && !PauseHandler.isPaused)
        { 
            string screenshotPath = Screenshoot();
            StartCoroutine(DisplayScreenshot(screenshotPath));
        }
    }

    public string Screenshoot()
    {
        string screenshotPath = photoFolder + "/screenshot.png";
        ScreenCapture.CaptureScreenshot(screenshotPath);
        return screenshotPath;
    }

    IEnumerator DisplayScreenshot(string imagePath)
    {
        ableToSS = false;
        Time.timeScale = 0f;

        Debug.Log("DisplayScreenshot coroutine started");

        // Load the screenshot into a Texture2D asynchronously
        yield return LoadTextureAsync(imagePath);
        yield return LoadTextureAsync(imagePath);

        yield return new WaitForSecondsRealtime(0.5f);
        screenshotPanel.gameObject.SetActive(true);
        screenshotPanel.transform.localScale = new Vector3(0, 0, 0);
        screenshotPanel.transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() => {
            string screenshotPath = photoFolder + "/screenshotWithTwibbon.png";
            ScreenCapture.CaptureScreenshot(screenshotPath);

        });

        Debug.Log("DisplayScreenshot coroutine finished");
    }

    IEnumerator LoadTextureAsync(string imagePath)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        // Load the screenshot into a Texture2D asynchronously
        byte[] fileData = System.IO.File.ReadAllBytes(imagePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData);

        // Wait until the texture is fully loaded
        while (!texture.isReadable)
        {
            yield return null;
        }

        // Assign the Texture2D to the UI Image component
        screenshotImage.sprite = null;
        screenshotImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        Debug.Log("Texture loaded successfully");
    }

    public void ExitScreenshot()
    {
        screenshotPanel.transform.DOScale(0, .3f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => {
            screenshotPanel.gameObject.SetActive(false);
            PauseHandler.ableToPause = true;
            Time.timeScale = 1f;
            ableToSS = true;
        });
    }
}