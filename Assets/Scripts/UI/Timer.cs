using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeValue = 90f;
    public TextMeshProUGUI timeText;
    private bool timerStarted;

    // Update is called once per frame
    void Update()
    {
        if (timeValue > 0 && timerStarted)
        {
            timeValue -= Time.deltaTime;
        }
        else if (timerStarted)
        {
            timeValue = 0;
        }

        DisplayTime(timeValue);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public float GetTime()
    {
        return this.timeValue;
    }

    public void StartTimer()
    {
        this.timerStarted = true;
    }

    public void StopTimer()
    {
        this.timerStarted = false;
    }
}
