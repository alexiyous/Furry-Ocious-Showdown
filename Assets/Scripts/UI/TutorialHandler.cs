using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialHandler : MonoBehaviour
{
    public static bool isTutorialActive = false;
    public List<GameObject> tutorialPages;

    private int currentPage = 0;

    public GameObject nextButton;
    public GameObject prevButton;

    public Vector3 originalTransform = new Vector3(.95f,.95f,.95f);


    // Start is called before the first frame update
    void OnEnable()
    {
        isTutorialActive = true;
        PauseHandler.ableToPause = false;
        transform.localScale = new Vector3 (0, 0, 0);

        transform.DOScale(originalTransform, 1f).SetEase(Ease.OutBack).SetUpdate(true);

        
        Time.timeScale = 0;

        currentPage = 0;

        tutorialPages[0].SetActive(true);

        for(int i = 1; i < tutorialPages.Count; i++)
        {
            tutorialPages[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPage();
        } else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevPage();
        }


        if (currentPage == tutorialPages.Count - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }

        if(currentPage == 0)
        {
            prevButton.SetActive(false);
        }
        else 
        { 
            prevButton.SetActive(true); 
        }
    }


    public void NextPage()
    {
        if (currentPage >= tutorialPages.Count - 1) return;

        tutorialPages[currentPage].SetActive(false);
        currentPage++;
        tutorialPages[currentPage].SetActive(true);
    }

    public void PrevPage()
    {
        if (currentPage <= 0) return;

        tutorialPages[currentPage].SetActive(false);
        currentPage--;
        tutorialPages[currentPage].SetActive(true);
    }

    public void CloseTutorial()
    {
        if (isTutorialActive)
        {
           

            transform.DOScale(0, .3f).SetEase(Ease.InBack).SetUpdate(true). OnComplete(() => {

                PauseHandler.ableToPause = true;

                if (!PauseHandler.isPaused)
                {
                    Time.timeScale = 1;
                }
                
                isTutorialActive = false;

                gameObject.SetActive(false);
            } );
            
        }
    }

    public void OpenTutorial()
    {
        gameObject.SetActive(true);
    }
}
