using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Points : MonoBehaviour
{
    Mixer _mx;
    public GameObject grid, menus, report;
    public GameObject[] f = {null, null, null, null};

    public int preset = 1;
    public int steps = 10;
    public int levels = 4;
    public int sessions = 5;
    public int breakTime = 15;
    public int mistakes;
    
    public int ptsCurrent;
    public int snsCurrent;
    private int _lvlCurrent = -1;
    public int ptsMax;
    
    public AudioSource p01, p02, p03, p04, p05, p06;
    public AudioSource newLevelSfx, endLevelSfx, errorSfx;

    public int mySteps, myTimeleft, myPoints;
    public string myDirectory; 

    void Start()
    {
        ptsMax = steps * levels;
        ptsCurrent = 0;
        _mx = GameObject.FindGameObjectWithTag("MX").GetComponent<Mixer>();
        level_handler();
        grid.GetComponent<Grid>().SetActive(4);
    }

    public void add_points ()
    {
        ptsMax = steps * levels; // safety update
        ptsCurrent++;
        myPoints++;
        
        // updating 3D bar
        for (int i = 0; i < 4; i++) f[i].GetComponent<CubeShrink>().glow_point();
        for(int i=0; i<4; i++) f[i].GetComponent<CubeShrink>().update_frame(ptsCurrent);
        
        var rand = new Random();
        int val = rand.Next(0, 6);

        AudioSource[] sfx = {p01, p02, p03, p04, p05, p06};
        sfx[val].Play();
        level_handler();
    }
    
    public void Mistake()
    {
        errorSfx.Play();
        mistakes++;
        for(int i=0; i<4; i++) f[i].GetComponent<CubeShrink>().glow_error();
    }
    
    public void level_handler()
    {
        if(ptsCurrent == 1) GameObject.Find("timer_system").GetComponent<timer>().StartTimer();
        if (ptsCurrent == 0) level_manager(0,false, grid.GetComponent<Grid>().GetChannel(0));
        else if (ptsCurrent == steps) level_manager(1,levels == 1, grid.GetComponent<Grid>().GetChannel(1));
        else if (ptsCurrent == steps * 2) level_manager(2,levels == 2, grid.GetComponent<Grid>().GetChannel(2));
        else if (ptsCurrent == steps * 3) level_manager(3,levels == 3, grid.GetComponent<Grid>().GetChannel(3));
        else if (ptsCurrent == steps * 4) level_manager(4,levels == 4, grid.GetComponent<Grid>().GetChannel(4));
        else if (ptsCurrent == steps * 5) level_manager(5,levels == 5, grid.GetComponent<Grid>().GetChannel(5));
        else if (ptsCurrent == ptsMax)  { int[] a = {1, 1, 1, 1, 1, 0, 1, 0}; level_manager(6,levels == 6, a); }
    }

    
    void level_manager(int lvl, bool isLast, int[] channels)
    {
        GameObject.Find("report").GetComponent<Report>().newLevel = true;   
        menus.GetComponent<ButtonSet>().GlowRestart(false);
        _lvlCurrent = lvl;
        if (!isLast)
            {
                if (lvl != 0) newLevelSfx.Play();
                if (lvl != 0) for (int i = 0; i < 4; i++) f[i].GetComponent<CubeShrink>().glow_newlevel();
            }
            else
            {
                // stopping timer
                GameObject.Find("timer_system").GetComponent<timer>().StopTimer();
                snsCurrent++;
                GameObject.Find("report").GetComponent<Report>().newSession = true;                
                
                if (sessions == snsCurrent)
                {
                    report.GetComponent<Report>().UpdateTrack();
                    UpdateResults();
                    menus.GetComponent<ButtonSet>().GlowRestart(true);
                    grid.GetComponent<Grid>().end_level();
                    endLevelSfx.Play();
                    grid.GetComponent<Grid>().ResetBoard();
                    for (int i = 0; i < 4; i++) f[i].GetComponent<CubeShrink>().glow_final();
                    
                }
                else
                {
                    endLevelSfx.Play(); 
                    ptsCurrent = 0;
                    f[0].GetComponent<CubeShrink>().glow_loading(true, breakTime); f[1].GetComponent<CubeShrink>().glow_loading(true, breakTime); 
                    f[2].GetComponent<CubeShrink>().glow_loading(false, breakTime); f[3].GetComponent<CubeShrink>().glow_loading(false, breakTime);
                    grid.GetComponent<Grid>().ResetBoard();
                    _mx.SetMixer(grid.GetComponent<Grid>().GetChannel(6));
                }
            }
            
            if (!isLast)
            {
                grid.GetComponent<Grid>().LevelSwitch(lvl, false);
                _mx.SetMixer(channels);
            }

    }
    
    public void ZeroTime()
    {
        report.GetComponent<Report>().UpdateTrack();
        UpdateResults();
        menus.GetComponent<ButtonSet>().GlowRestart(true);
        grid.GetComponent<Grid>().end_level();
        endLevelSfx.Play();
        grid.GetComponent<Grid>().ResetBoard();
        for (int i = 0; i < 4; i++) f[i].GetComponent<CubeShrink>().glow_final();
    }

    public bool SessionFinished()
    {
        return ptsCurrent == ptsMax-1;
    }

    public string ReportData()
    {
        int sns = snsCurrent + 1;
        int lvl = _lvlCurrent + 1;
        return sns.ToString() + "," + sessions.ToString() + "," + lvl.ToString() + "," + levels.ToString();
    }

    public void ChangePreset(int value)
    {
        preset = value;
        ResetAllPresses();
        GameObject.Find("menus").GetComponent<Loader>().ReadSavedStates(preset);
        GameObject.Find("menus").GetComponent<ButtonSet>().RestartGame();
        
    }

    private void ResetAllPresses()
    {
        GameObject.Find("levels").GetComponent<ButtonPress>().Clean();
        GameObject.Find("sessions").GetComponent<ButtonPress>().Clean();
        GameObject.Find("music").GetComponent<ButtonPress>().Clean();
        GameObject.Find("breaks").GetComponent<ButtonPress>().Clean();
        GameObject.Find("steps").GetComponent<ButtonPress>().Clean(); 
        GameObject.Find("gametime").GetComponent<ButtonPress>().Clean(); 
        GameObject.Find("channels_B").GetComponent<ButtonPress>().UnPress();
        GameObject.Find("channels_B").GetComponent<ButtonPress>().Clean();
        for (int i = 1; i < 7; i++)
        {
           GameObject.Find("spikes_" + i).GetComponent<ButtonPress>().UnPress();
           GameObject.Find("spikes_" + i).GetComponent<ButtonPress>().Clean();
           GameObject.Find("yellows_" + i).GetComponent<ButtonPress>().UnPress();
           GameObject.Find("yellows_" + i).GetComponent<ButtonPress>().Clean();
           GameObject.Find("channels_" + i).GetComponent<ButtonPress>().UnPress();
           GameObject.Find("channels_" + i).GetComponent<ButtonPress>().Clean();
        }
    }
    
    public void UpdateResults() {
        GameObject.Find("settings").GetComponent<CanvasMgmt>().ResultsToogle(true);
        mySteps = GameObject.Find("report").GetComponent<Report>().stepTracker;
        myTimeleft = Convert.ToInt32(GameObject.Find("timer_system").GetComponent<timer>().timeRemaining);
        myDirectory = GameObject.Find("report").GetComponent<Report>().nameFile;
        GameObject.Find("value_1_text").GetComponent<Text>().text = mySteps.ToString();
        GameObject.Find("value_2_text").GetComponent<Text>().text = mistakes.ToString();
        GameObject.Find("value_3_text").GetComponent<Text>().text = myTimeleft.ToString();
        GameObject.Find("value_5_text").GetComponent<Text>().text = myPoints.ToString();
        GameObject.Find("value_5_text_perc").GetComponent<Text>().text = "(" + Math.Round(((double)myPoints/mySteps*100),2) + "%)"; 
        GameObject.Find("value_2_text_perc").GetComponent<Text>().text = "(" + Math.Round(((double)mistakes/mySteps*100),2) + "%)";
        GameObject.Find("label_4_value").GetComponent<Text>().text = myDirectory;
    }
    
    

}
