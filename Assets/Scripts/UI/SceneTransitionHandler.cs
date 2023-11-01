using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitionHandler : MonoBehaviour
{
    public static SceneTransitionHandler instance;

    public RectTransform upper;
    public RectTransform lower;
    public CanvasGroup logo;

    public Ease easeType = Ease.InOutSine;

    public Vector3 upperStartPos = new Vector3(0, 570, 0);
    public Vector3 lowerStartPos = new Vector3(0, -570, 0);


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        upper = GameObject.Find("Upper").GetComponent<RectTransform>();
        lower = GameObject.Find("Lower").GetComponent<RectTransform>();
        logo = GameObject.Find("Logo Transition").GetComponent<CanvasGroup>();

        logo.alpha = 1;

        StartTransition();
    }


    public void StartTransition()
    {
        PauseHandler.ableToPause = false;

        logo.alpha = 1;
        /*upper.position = upperStartPos;
        lower.position = lowerStartPos;*/

        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(1f).SetUpdate(true);
        sequence.Append(upper.DOAnchorPosY(upperStartPos.y + 700, 1f).SetEase(easeType).SetUpdate(true));
        sequence.Join(lower.DOAnchorPosY(lowerStartPos.y - 700, 1f).SetEase(easeType).SetUpdate(true));
        sequence.Join(logo.DOFade(0, 1f).SetEase(easeType).SetUpdate(true)).OnComplete(() =>
        {
            /*PauseHandler.ableToPause = true;*/
        });
    }

    public void EndTransition(string sceneToLoad)
    {
        logo.alpha = 0;

        PauseHandler.ableToPause = false;
        /*upper.position = upperStartPos + new Vector3(0, 700, 0);
        lower.position = lowerStartPos + new Vector3(0, -700, 0);*/

        Sequence sequence = DOTween.Sequence();

        sequence.Append(upper.DOAnchorPosY(upperStartPos.y, 1f).SetEase(easeType).SetUpdate(true));
        sequence.Join(lower.DOAnchorPosY(lowerStartPos.y, 1f).SetEase(easeType).SetUpdate(true));
        sequence.Join(logo.DOFade(1, 1f).SetEase(easeType).SetUpdate(true));
        sequence.AppendInterval(1f).SetUpdate(true).OnComplete(() =>
        {
            Debug.Log("Loading scene: " + sceneToLoad);
            PauseHandler.ableToPause = true;
            SceneManager.LoadScene(sceneToLoad);

        });
    }
}
