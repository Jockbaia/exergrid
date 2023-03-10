using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{

    public float timeRemaining;
    public float timeValue;
    public bool timerIsRunning = false;
    public int minutes;
    public int seconds;
    public string time = "00:00";

    void Start()
    {
        SendTime();
    }
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                GameObject.FindWithTag("PTS").GetComponent<Points>().ZeroTime();
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
            SendTime();
        }
    }

    public void SendTime()
    {
        minutes = (int) (timeRemaining / 60);
        seconds = (int) (timeRemaining - minutes * 60);
        if(GameObject.Find("GUI_clock") != null) GameObject.Find("GUI_clock").GetComponent<Text>().text = minutes.ToString("D2") + ":" + seconds.ToString("D2"); 
    }
    
    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    public void ResetTimer()
    {
        timeRemaining = timeValue;
        SendTime();
    }
    
}
