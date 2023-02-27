using System;
using UnityEngine;

public class ButtonSet : MonoBehaviour
{
    private GameObject _pts;
    public GameObject grid;
    public GameObject[] f = {null, null, null, null};
    public int exitFlag = 0;
    
    void Start()
    {
        _pts = GameObject.FindGameObjectWithTag("PTS");
    }

    public void SetSpikes(String lvlQty) {
        int lvl = Int32.Parse(lvlQty.Substring(0,1));
        int qty = Int32.Parse(lvlQty.Substring(1,1));
        
        if (GameObject.Find("spike_" + lvl.ToString() + "_" + qty.ToString()).GetComponent<ButtonProperty>().buttonPressed == false)
            grid.GetComponent<Grid>().numSpikes[lvl - 1] = qty;
        else grid.GetComponent<Grid>().numSpikes[lvl - 1] = 0;

        RestartGame();
    }
    
    public void SetYellows(String lvlQty) {
        int lvl = Int32.Parse(lvlQty.Substring(0,1));
        int qty = Int32.Parse(lvlQty.Substring(1,1));
        
        if (GameObject.Find("yellow_" + lvl.ToString() + "_" + qty.ToString()).GetComponent<ButtonProperty>().buttonPressed == false) 
            grid.GetComponent<Grid>().yellowPercentage[lvl - 1] = qty;
        else grid.GetComponent<Grid>().yellowPercentage[lvl - 1] = 0;

        RestartGame();
    }

    public void SetChannels(String lvlCh)
    {
            int lvl = Int32.Parse(lvlCh.Substring(0,1));
            int ch = Int32.Parse(lvlCh.Substring(1,1));
            
            if (GameObject.Find("channel_" + lvl.ToString() + "_" + ch.ToString()).GetComponent<ButtonProperty>().buttonPressed) 
                grid.GetComponent<Grid>().channels[lvl - 1,ch - 1] = 1;
            else grid.GetComponent<Grid>().channels[lvl - 1, ch - 1] = 0;
            
            RestartGame();
    }
    
    public void SetBreakChannels(String lvl)
    {
        if (GameObject.Find("channel_B_" + lvl.ToString()).GetComponent<ButtonProperty>().buttonPressed) 
            grid.GetComponent<Grid>().channels[6,Int32.Parse(lvl) - 1] = 1;
        else grid.GetComponent<Grid>().channels[6, Int32.Parse(lvl) - 1] = 0;
            
        RestartGame();
    }

    public void SetSteps(int step) {
        _pts.GetComponent<Points>().steps = step;
        RestartGame();
    }
    
    public void SetLevels(int levels)
    {
        _pts.GetComponent<Points>().levels = levels;
        RestartGame();
    }
    
    public void SetTrack(int track)
    {
        GameObject.Find("mixer").GetComponent<Mixer>().currentTrack = track;
        RestartGame();
    }
    
    public void SetSessions(int sessions)
    {
        _pts.GetComponent<Points>().sessions = sessions;
        RestartGame();
    }
    
    public void SetBreaks(int time)
    {
        _pts.GetComponent<Points>().breakTime = time;
        RestartGame();
    }
    
    public void SetTimer(int time)
    {
        GameObject.Find("timer_system").GetComponent<timer>().timeValue = time;
        RestartGame();
    }
    
    public void SetCube()
    {
        
        GameObject.Find("game").GetComponent<AudioSource>().Play();
        GameObject.Find("pawn").GetComponent<Transform>().SetPositionAndRotation(GameObject.Find("table").GetComponent<Transform>().position + new Vector3(0,0.05f,0), Quaternion.Euler(new Vector3(0.0f,180.0f,0.0f)));
    }

    public void RestartGame_fromBTN()
    {
        GameObject.Find("game").GetComponent<AudioSource>().Play();
        SetCube();
        RestartGame();
    }

    public void CloseGame()
    {
        GameObject.Find("game").GetComponent<AudioSource>().Play();
        if(exitFlag == 1) {
            #if UNITY_STANDALONE
            Application.Quit();
            #endif
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }        
        exitFlag = exitFlag + 1;
    }
    
    public void RestartGame()
    {
        GameObject.Find("timer_system").GetComponent<timer>().stopTimer();
        GameObject.Find("timer_system").GetComponent<timer>().resetTimer();
        _pts.GetComponent<Points>().ptsCurrent = _pts.GetComponent<Points>().snsCurrent = 0;
        _pts.GetComponent<Points>().negativeStreak = _pts.GetComponent<Points>().positiveStreak = 0;
        grid.GetComponent<Grid>().ResetBoard();
        grid.GetComponent<Grid>().SetActive(4);
        grid.GetComponent<Grid>().LevelSwitch(0, true);
        _pts.GetComponent<Points>().level_handler();
        GameObject.Find("mixer").GetComponent<Mixer>().UpdateUI();
        GameObject.Find("timer").GetComponent<AudioSource>().Pause();
        for (int i = 0; i < 4; i++)
        {
            f[i].GetComponent<CubeShrink>().update_frame(0);
            f[i].GetComponent<CubeShrink>().RestartInvokes();
        }
        GameObject.Find("report").GetComponent<Report>().newGame = true;
        GameObject.Find("report").GetComponent<Report>().newSession = true;
        GameObject.Find("report").GetComponent<Report>().newLevel = true;
    }
    

}
