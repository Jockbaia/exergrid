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
    private int _curPts, _curLvl, _curSps;
    public GameObject grid;
    public GameObject[] f = {null, null, null, null};

    
    void Start()
    {
         GetComponent<Loader>().ReadSavedStates();
         _pts = GameObject.FindGameObjectWithTag("PTS");
         _curSps = _pts.GetComponent<Points>().steps;
         _curLvl = _pts.GetComponent<Points>().levels;
         _curPts = _pts.GetComponent<Points>().ptsCurrent;
    }
    
    public void SetSteps(int step) {
        _curSps = step;
        RestartGame();
    }

    public void SetLevels(int levels)
    {
        _pts.GetComponent<Points>().levels = levels;
        RestartGame();
    }

    private void RestartGame()
    {
        _curPts = 0;
        grid.GetComponent<Grid>().ResetBoard();
        grid.GetComponent<Grid>().SetActive(4);
        for(int i=0; i<4; i++) f[i].GetComponent<CubeShrink>().update_frame(0);
    }
    
}
