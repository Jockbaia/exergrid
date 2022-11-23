using System;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Points : MonoBehaviour
{
    Mixer _mx;
    public int ptsCurrent;
    public int ptsMax;
    public int snsCurrent;
    public int steps = 10;
    public int levels = 4;
    public int sessions = 5;
    public int breakTime = 15;
    public int negativeStreak, positiveStreak;
    public Text ptsCurrentText, positiveStreakText, negativeStreakText;
    public Text channelsText;
    public AudioSource p01, p02, p03, p04, p05, p06;
    public AudioSource newLevelSfx, endLevelSfx, errorSfx;
    public GameObject grid;
    private int[] _lastMix;
    private string _lastText;
    
    public GameObject frame1, frame2, frame3, frame4;
    private GameObject[] f = {null, null, null, null};

    void Start()
    {
        ptsMax = steps * levels;
        ptsCurrent = 0;
        negativeStreak = 0;
        positiveStreak = 0;
        _mx = GameObject.FindGameObjectWithTag("MX").GetComponent<Mixer>();
        update_points();
        level_handler();
        f[0] = frame1;
        f[1] = frame2;
        f[2] = frame3;
        f[3] = frame4;
        grid.GetComponent<Grid>().SetActive(4);
    }

    public void add_points ()
    {
        ptsMax = steps * levels; // safety update
        
        // !chillmode
        if (positiveStreak == 5)
        {
            channelsText.text = String.Format(_lastText);
            _mx.SetMixer(_lastMix);
        }
        
        ptsCurrent++;
        positiveStreak++;
        
        // updating 3D bar
        for(int i=0; i<4; i++) f[i].GetComponent<CubeShrink>().update_frame(ptsCurrent);
        
        var rand = new Random();
        int val = rand.Next(0, 6);

        AudioSource[] sfx = {p01, p02, p03, p04, p05, p06};
        sfx[val].Play();
        
        negativeStreak = 0;
        update_points();
        level_handler();
    }
    
    public void Mistake()
    {
        negativeStreak++;
        errorSfx.Play();
        positiveStreak = 0;
        update_points();
        chillMode_handler();
        for(int i=0; i<4; i++) f[i].GetComponent<CubeShrink>().glow_error();
    }

    void update_points()
    {
        ptsCurrentText.text = String.Format("{0} points",ptsCurrent);
        if (positiveStreak > 0)
        {
            negativeStreakText.text = "";
            positiveStreakText.text = String.Format("{0} combo!", positiveStreak);
        }
        else if (negativeStreak > 0)
        {
            negativeStreakText.text = String.Format("{0} combo! :(", negativeStreak);
            positiveStreakText.text = "";
        }
        else
        {
            negativeStreakText.text = "";
            positiveStreakText.text = "";
        }
    }

    public void level_handler()
    {
        if (ptsCurrent == 0) // LVL 1
        {
            level_manager(0,false, grid.GetComponent<Grid>().GetChannel(0), "stage 1");
        } else if (ptsCurrent == steps) // LVL 2
        {
            level_manager(1,levels == 1, grid.GetComponent<Grid>().GetChannel(1), "stage 2");
        } else if (ptsCurrent == steps*2) // LVL 3
        {
            level_manager(2,levels == 2, grid.GetComponent<Grid>().GetChannel(2), "stage 3");
        } else if (ptsCurrent == steps*3) // LVL 4
        {
            level_manager(3,levels == 3, grid.GetComponent<Grid>().GetChannel(3), "stage 4");
        } else if (ptsCurrent == steps*4) // LVL 5
        {
            level_manager(4,levels == 4, grid.GetComponent<Grid>().GetChannel(4), "stage 5");
        } else if (ptsCurrent == steps*5) // LVL 5
        {
            level_manager(5,levels == 5, grid.GetComponent<Grid>().GetChannel(5), "stage 6");
        } else if (ptsCurrent == ptsMax)  // LVL 6
        {
            int[] a = {1, 1, 1, 1, 1, 0, 1, 0};
            level_manager(5,levels == 6, a, "stage '7'");
        }
    }

    void level_manager(int lvl, bool isLast, int[] channels, String text)
    {
        
        
            if (!isLast)
            {
                if (lvl != 0) newLevelSfx.Play();
                if (lvl != 0) for (int i = 0; i < 4; i++) f[i].GetComponent<CubeShrink>().glow_newlevel();
            }
            else
            {
                snsCurrent++;
                
                if (sessions == snsCurrent)
                {
                    grid.GetComponent<Grid>().end_level();
                    Debug.Log("YAYYY!");
                    endLevelSfx.Play();
                    for (int i = 0; i < 4; i++) f[i].GetComponent<CubeShrink>().glow_final();
                }
                else
                {
                    endLevelSfx.Play(); 
                    f[0].GetComponent<CubeShrink>().glow_loading(true, breakTime); 
                    f[1].GetComponent<CubeShrink>().glow_loading(true, breakTime); 
                    f[2].GetComponent<CubeShrink>().glow_loading(false, breakTime); 
                    f[3].GetComponent<CubeShrink>().glow_loading(false, breakTime);
                    grid.GetComponent<Grid>().ResetBoard();
                    ptsCurrent = 0;
                    update_points();
                    
                }
            }
            
            if (!isLast)
            {
                _lastText = text;
                _lastMix = channels;
                grid.GetComponent<Grid>().LevelSwitch(lvl, false);
                channelsText.text = String.Format(text);
                _mx.SetMixer(channels);
            }
        

        
    }



    void chillMode_handler()
    {
        if (negativeStreak == 5)
        {
            channelsText.text = "Chill mode";
            if (ptsCurrent < 40)
            {
                int[] a = {0, 0, 1, 0, 0, 0, 1, 0};
                _mx.SetMixer(a);  
            }
            else
            {
                int[] a = {0, 0, 1, 0, 0, 1, 1, 0};
                _mx.SetMixer(a);
            }
        }
    }

    public bool SessionFinished()
    {
        return ptsCurrent == ptsMax-1;
    }
    
}
