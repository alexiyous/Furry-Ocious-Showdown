using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHandler : MonoBehaviour
{
    public List<GameObject> pages;

    private int currentPage = 0;

    public GameObject nextButton;
    public GameObject prevButton;

    public Vector3 originalTransform = new Vector3(.95f, .95f, .95f);


    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);

        transform.DOScale(originalTransform, 1f).SetEase(Ease.OutBack).SetUpdate(true);

        currentPage = 0;

        pages[0].SetActive(true);

        for (int i = 1; i < pages.Count; i++)
        {
            pages[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPage();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevPage();
        }


        if (currentPage == pages.Count - 1)
        {
            nextButton.SetActive(false);
        }
        else
        {
            nextButton.SetActive(true);
        }

        if (currentPage == 0)
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
        if (currentPage >= pages.Count - 1) return;

        pages[currentPage].SetActive(false);
        currentPage++;
        pages[currentPage].SetActive(true);
    }

    public void PrevPage()
    {
        if (currentPage <= 0) return;

        pages[currentPage].SetActive(false);
        currentPage--;
        pages[currentPage].SetActive(true);
    }

    public void Close()
    {
        transform.DOScale(0, .3f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => {
            gameObject.SetActive(false);
        });

    }

    public void Open()
    {
        gameObject.SetActive(true);
    }
}
