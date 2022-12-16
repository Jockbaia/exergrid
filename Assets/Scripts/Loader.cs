using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public void ReadSavedStates()
    {
        string path = Application.persistentDataPath + "/save.txt";
        StreamReader txt = new StreamReader(path); 
        string settings = txt.ReadLine();

        // SESSIONS
        string sessionsValue = settings.Substring(2, 1);
        GameObject.Find("session_" + sessionsValue).GetComponent<Button>().Select();
        GameObject.Find("sessions").GetComponent<ButtonPress>().ExternalPress(Int32.Parse(sessionsValue) - 1);
        GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().sessions = Int32.Parse(sessionsValue);
        
        // STEPS
        string stepsValue = settings.Substring(6, 1);
        GameObject.Find("steps_" + stepsValue).GetComponent<Button>().Select();
        GameObject.Find("steps").GetComponent<ButtonPress>().defaultPress = Int32.Parse(stepsValue) - 1;
        GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().steps = Int32.Parse(stepsValue)*5;
        
        // BREAKS
        string breaksValue = settings.Substring(14, 1);
        GameObject.Find("break_" + breaksValue).GetComponent<Button>().Select();
        GameObject.Find("breaks").GetComponent<ButtonPress>().ExternalPress(Int32.Parse(breaksValue) - 1);
        GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().breakTime = Int32.Parse(breaksValue)*15;
        
        // TRACKS
        int defaultTrack = Int32.Parse(settings.Substring(10, 1));
        if(defaultTrack == 1) GameObject.Find("Light").GetComponent<Button>().Select();
        else if(defaultTrack == 2) GameObject.Find("Heavy").GetComponent<Button>().Select();
        else GameObject.Find("Mute").GetComponent<Button>().Select();
        GameObject.Find("music").GetComponent<ButtonPress>().ExternalPress(defaultTrack);
        GameObject.Find("mixer").GetComponent<Mixer>().currentTrack = defaultTrack;
        
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
                GameObject.Find("yellows_" + currentLevel).GetComponent<ButtonPress>().ExternalPress(numYellows-1);
                GameObject.Find("yellow_" + currentLevel + "_" + numYellows).GetComponent<ButtonProperty>().SequencialPressure();
            }
            
            // CHANNELS
            
            int currentChannel = 0;
            for (int a = 0; a < 8; a++)
            {
                currentChannel = Int32.Parse(settings.Substring(11 + a, 1));
                if (currentChannel == 1)
                {
                    int b = a + 1;
                    GameObject.Find("grid").GetComponent<Grid>().channels[currentLevel-1,a] = 1;
                    GameObject.Find("channels_" + currentLevel).GetComponent<ButtonPress>().ExternalPress(a);
                    GameObject.Find("channel_" + currentLevel + "_" + b).GetComponent<ButtonProperty>().MixerPressure();
                }
            }
        }
    }

    public void UpdateSaveState(String xVal)
    {
        
        int x = Int32.Parse(xVal.Substring(0,1));
        int Val = Int32.Parse(xVal.Substring(1,1));
        
        string path = Application.persistentDataPath + "/save.txt";
        string text = File.ReadAllText(path);
        
        switch (x)
        {
            case 1:
                text = Regex.Replace(text, "SE.", "SE"+Val);
                break;
            case 2:
                text = Regex.Replace(text, "ST.", "ST"+Val);
                break;
            case 3:
                text = Regex.Replace(text, "MU.", "MU"+Val);
                break;
            case 4:
                text = Regex.Replace(text, "BR.", "BR"+Val);
                break;
            case 5:
                text = Regex.Replace(text, "NL.", "NL"+Val);
                break;
        }
        
        using (StreamWriter writer = new StreamWriter(path, false)){ 
            writer.Write(text);
        }
        
    }
    
    public void UpdateSpikes(String lvlVal)
    {
        
        int lvl = Int32.Parse(lvlVal.Substring(0,1));
        int Val = Int32.Parse(lvlVal.Substring(1,1));
        
        string path = Application.persistentDataPath + "/save.txt";
        string text = File.ReadAllText(path);
        
        switch (lvl)
        {
            case 1:
                text = Regex.Replace(text, "LV1.R.", "LV1.R"+Val);
                break;
            case 2:
                text = Regex.Replace(text, "LV2.R.", "LV2.R"+Val);
                break;
            case 3:
                text = Regex.Replace(text, "LV3.R.", "LV3.R"+Val);
                break;
            case 4:
                text = Regex.Replace(text, "LV4.R.", "LV4.R"+Val);
                break;
            case 5:
                text = Regex.Replace(text, "LV5.R.", "LV5.R"+Val);
                break;
            case 6:
                text = Regex.Replace(text, "LV6.R.", "LV6.R"+Val);
                break;
        }
        
        using (StreamWriter writer = new StreamWriter(path, false)){ 
            writer.Write(text);
        }
        
    }

}
