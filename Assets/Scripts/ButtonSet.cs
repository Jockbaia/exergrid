using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSet : MonoBehaviour
{
    private GameObject _pts;
    private int _curPts;
    public GameObject grid;
    public GameObject[] f = {null, null, null, null};

    
    void Start()
    {
         GetComponent<Loader>().ReadSavedStates();
         _pts = GameObject.FindGameObjectWithTag("PTS");
    }
    
    public void SetSteps(int step) {
        _pts.GetComponent<Points>().steps = step;
        RestartGame();
    }
    
    public void SetSpikes(String lvlQty) {
        int lvl = Int32.Parse(lvlQty.Substring(0,1));
        int qty = Int32.Parse(lvlQty.Substring(1,1));
        if (GameObject.Find("spike_" + lvl.ToString() + "_" + qty.ToString()).GetComponent<ButtonProperty>()
                .buttonPressed == false)
        {
            grid.GetComponent<Grid>().numSpikes[lvl - 1] = qty;
        }
        else
        {
            grid.GetComponent<Grid>().numSpikes[lvl - 1] = 0;
        }
        
        RestartGame();
        

    }
    
    public void SetYellows(String lvlQty) {
        int lvl = Int32.Parse(lvlQty.Substring(0,1));
        int qty = Int32.Parse(lvlQty.Substring(1,1));
        if (GameObject.Find("yellow_" + lvl.ToString() + "_" + qty.ToString()).GetComponent<ButtonProperty>()
                .buttonPressed == false)
        {
            grid.GetComponent<Grid>().yellowPercentage[lvl - 1] = qty;
        }
        else
        {
            grid.GetComponent<Grid>().yellowPercentage[lvl - 1] = 0;
        }
        
        RestartGame();
    }

    public void SetChannels(String lvlCh)
    {
            int lvl = Int32.Parse(lvlCh.Substring(0,1));
            int ch = Int32.Parse(lvlCh.Substring(1,1));
            if (GameObject.Find("channel_" + lvl.ToString() + "_" + ch.ToString()).GetComponent<ButtonProperty>()
                    .buttonPressed == true)
            {
                grid.GetComponent<Grid>().channels[lvl - 1,ch - 1] = 1;
            }
            else
            {
                grid.GetComponent<Grid>().channels[lvl - 1, ch - 1] = 0;
            }
        
            RestartGame();

    }

    public void SetLevels(int levels)
    {
        _pts.GetComponent<Points>().levels = levels;
        RestartGame();
    }

    private void RestartGame()
    {
        _pts.GetComponent<Points>().ptsCurrent = 0;
        grid.GetComponent<Grid>().ResetBoard();
        grid.GetComponent<Grid>().SetActive(4);
        grid.GetComponent<Grid>().LevelSwitch(0);
        _pts.GetComponent<Points>().level_handler();
        for(int i=0; i<4; i++) f[i].GetComponent<CubeShrink>().update_frame(0);
        GameObject.Find("mixer").GetComponent<Mixer>().UpdateUI();
    }
    
}
