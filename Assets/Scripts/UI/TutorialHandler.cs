using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    public static bool isTutorialActive = false;
    public List<GameObject> tutorialPages;

    public List<CanvasGroup> tutorialPages2;

    private int currentPage = 0;

    public GameObject nextButton;
    public GameObject prevButton;

    // Start is called before the first frame update
    void OnEnable()
    {
        isTutorialActive = true;
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
            Time.timeScale = 1;
            isTutorialActive = false;
            gameObject.SetActive(false);
            
        }
    }
}
