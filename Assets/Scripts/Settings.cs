using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private GameObject _pts;
    public GameObject grid;
    public GameObject steps;
    public GameObject frame1, frame2, frame3, frame4;
    private GameObject[] f = {null, null, null, null};

    
    void Start()
    {
        ReadSavedStates();
         _pts = GameObject.FindGameObjectWithTag("PTS");
         f[0] = frame1; f[1] = frame2; f[2] = frame3; f[3] = frame4;
    }

    private void ReadSavedStates()
    {
        
        string path = "Assets/save.txt";
        StreamReader txt = new StreamReader(path); 
        string settings = txt.ReadLine();
        
        // DOVREBBE AGIRE COME SE IL BOTTONE VENISSE PREMUTO
        
        // STEPS
        string stepsValue =  settings.Substring(6, 1);
        GameObject.Find("steps_" + stepsValue).GetComponent<Button>().Select();
        GameObject.Find("steps").GetComponent<ButtonsMgmt>().defaultPress = Int32.Parse(stepsValue) - 1;
        // SetSteps(Int32.Parse(stepsValue) - 1);

        // LEVELS 
        string numOfLevels =  settings.Substring(18, 1);
        GameObject.Find("levels_" + numOfLevels).GetComponent<Button>().Select();
        GameObject.Find("levels").GetComponent<ButtonsMgmt>().defaultPress = Int32.Parse(numOfLevels) - 1;
        // SetLevels(Int32.Parse(numOfLevels));
        
    }

    public void SetSteps(int steps) {
        _pts.GetComponent<Points>().steps = steps;
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
        for(int i=0; i<4; i++) f[i].GetComponent<CubeShrink>().update_frame(0);
    }

}
