using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI textScore;

    public void IncrementScore()
    {
        score++;
        textScore.text = score.ToString();
    }

    public void IncrementScoreByValue(int points)
    {
        score += points;
        textScore.text = score.ToString();
    }

    public void DecrementScore()
    {
        if (score != 0)
        {
            score--;
            textScore.text = score.ToString();
        }
    }
}
