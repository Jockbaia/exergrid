using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    private Component _settings; 
    void Start()
    {
        _settings = GetComponent<ButtonSet>();
    }
    

    public void ReadSavedStates()
    {
        string path = "Assets/save.txt";
        StreamReader txt = new StreamReader(path); 
        string settings = txt.ReadLine();

        // STEPS
        string stepsValue = settings.Substring(6, 1);
        GameObject.Find("steps_" + stepsValue).GetComponent<Button>().Select();
        GameObject.Find("steps").GetComponent<ButtonPress>().defaultPress = Int32.Parse(stepsValue) - 1;
        GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().steps = Int32.Parse(stepsValue)*5;

        // LEVELS 
        string numOfLevels =  settings.Substring(18, 1);
        GameObject.Find("levels_" + numOfLevels).GetComponent<Button>().Select();
        GameObject.Find("levels").GetComponent<ButtonPress>().defaultPress = Int32.Parse(numOfLevels) - 1;
        GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().levels = Int32.Parse(numOfLevels);
        
        
        for (int i = 0; i < 6; i++)
        {
            settings = txt.ReadLine();
            int currentLevel = i + 1;

            // SPIKES
            int numSpikes = Int32.Parse(settings.Substring(5, 1));
            if (numSpikes != 0)
            {
                GameObject.Find("grid").GetComponent<Grid>().numSpikes[currentLevel - 1] = numSpikes;
                GameObject.Find("spikes_" + currentLevel).GetComponent<ButtonPress>().defaultPress = numSpikes-1;
                GameObject.Find("spike_" + currentLevel + "_" + numSpikes).GetComponent<ButtonProperty>().SequencialPressure();
            }
            
            // YELLOWS
            int numYellows = Int32.Parse(settings.Substring(8, 1));
            if (numYellows != 0)
            {
                GameObject.Find("grid").GetComponent<Grid>().yellowPercentage[currentLevel - 1] = numYellows;
                GameObject.Find("yellows_" + currentLevel).GetComponent<ButtonPress>().defaultPress = numYellows-1;
                GameObject.Find("yellow_" + currentLevel + "_" + numYellows).GetComponent<ButtonProperty>().SequencialPressure();
            }
            
            // CHANNELS

            int[] currentChannels = {0, 0, 0, 0, 0, 0, 0, 0};
            int currentChannel = 0;
            for (int a = 0; a < 7; a++)
            {
                currentChannel = Int32.Parse(settings.Substring(11 + a, 1));
                if (currentChannel == 1)
                {
                    
                    //GameObject.Find("channel_" + currentLevel + "_" + a).GetComponent<ButtonProperty>().MixerPressure();
                    Debug.Log(a);
                    GameObject.Find("grid").GetComponent<Grid>().channels[currentLevel-1,a] = 1;
                    //GameObject.Find("channels_" + currentLevel).GetComponent<ButtonPress>().Press(a);
                    // GameObject.Find("channels_" + currentLevel).GetComponent<ButtonPress>().defaultPress = 4;
                    
                    //
                    // GameObject.Find("channel_" + currentLevel + "_" + a+1).GetComponent<Button>().Select();
                }
            }
            
            
            
            
            
            

            
        }
    }
}
