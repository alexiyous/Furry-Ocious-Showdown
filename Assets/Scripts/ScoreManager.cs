using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public static int currentScore;
    public static int highScore;

    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    /*public TextMeshProUGUI highScoreText;*/

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        } else
        {
            instance = this;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = currentScore.ToString("D4");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentScore > highScore)
        {
            highScore = currentScore;
        }

        scoreText.text = currentScore.ToString("D4");
    }

    public void AddScore(int score)
    {
        currentScore += score;

        scoreText.text = currentScore.ToString("D4");

        /*highScoreText.text = currentScore.ToString("D9");*/
    }

    public void RemoveScore()
    {
        currentScore = 0;

        scoreText.text = currentScore.ToString("D4");
    }
}
