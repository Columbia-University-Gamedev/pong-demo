using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float width;

    public float height;

    int p1score = 0;

    int p2score = 0;

    public Text p1ScoreText;
    public Text p2ScoreText;

    void Start()
    {
        UpdateScoreText();
    }

    public void ScorePoint(bool isPlayer1Scoring)
    {
        if(isPlayer1Scoring)
        {
            p1score++;
        }
        else
        {
            p2score++;
        }
        
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        p1ScoreText.text = $"P1 Score: {p1score}";
        p2ScoreText.text = "P2 Score: " + p2score.ToString();
    }
}
