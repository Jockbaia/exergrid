using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer : MonoBehaviour
{

    public float timeRemaining = 180;
    public bool timerIsRunning = false;

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
                GameObject.FindWithTag("PTS").GetComponent<Points>().zeroTime();
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    public void stopTimer()
    {
        timerIsRunning = false;
    }

    public void startTimer()
    {
        timerIsRunning = true;
    }

    public void resetTimer()
    {
        timeRemaining = 180;
    }
    
}
