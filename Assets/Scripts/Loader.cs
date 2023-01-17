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
    private int currentChannel = 0;
    public bool firstStartup = true;

    private void Start()
    {
        String path = Application.persistentDataPath + "/save.txt";
        String report = Application.persistentDataPath + "/Reports";
        if (!File.Exists(path))
        {
            using(var sw = new StreamWriter(path, true))
            {
                sw.WriteLine("SE2.ST2.MU1.BT1.NL5");                     
                sw.WriteLine("LV1.R0.Y0.M10010001"); 
                sw.WriteLine("LV2.R1.Y0.M10110001");
                sw.WriteLine("LV3.R2.Y0.M10110101");                     
                sw.WriteLine("LV4.R3.Y0.M10111101"); 
                sw.WriteLine("LV5.R4.Y0.M11111111");
                sw.WriteLine("LV6.R0.Y0.M11111111"); 
                sw.WriteLine("BRK.##.##.M10000001"); 
            }
        }

        if (!Directory.Exists(report))
        {
            Directory.CreateDirectory(report);
        }

        GetComponent<Loader>().ReadSavedStates();
        firstStartup = false;
    }

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
            
            currentChannel = 0;
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
        
        settings = txt.ReadLine();
        
        currentChannel = 0;
        for (int a = 0; a < 8; a++)
        {
            currentChannel = Int32.Parse(settings.Substring(11 + a, 1));
            if (currentChannel == 1)
            {
                int b = a + 1;
                GameObject.Find("grid").GetComponent<Grid>().channels[6,a] = 1;
                GameObject.Find("channels_B").GetComponent<ButtonPress>().ExternalPress(a);
                GameObject.Find("channel_B_" + b).GetComponent<ButtonProperty>().MixerPressure();
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
                text = Regex.Replace(text, "BT.", "BT"+Val);
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
        string pattern = "LV" + lvl + ".R.";
        string replacement;
        string object_name = "spike_" + lvl + "_" + Val;
        
        if(!GameObject.Find(object_name).GetComponent<ButtonProperty>().buttonPressed) replacement = "LV" + lvl + ".R0";
        else replacement = "LV" + lvl + ".R" + Val;
        text = Regex.Replace(text, pattern, replacement);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
    }
    
    public void UpdateYellows(String lvlVal)
    {
        int lvl = Int32.Parse(lvlVal.Substring(0,1));
        int Val = Int32.Parse(lvlVal.Substring(1,1));
        
        string path = Application.persistentDataPath + "/save.txt";
        string text = File.ReadAllText(path);
        var replacement = Regex.Match(text, "LV" + lvl + ".R..Y");
        string replacementStr;
        
        string object_name = "yellow_" + lvl + "_" + Val;
        
        if(!GameObject.Find(object_name).GetComponent<ButtonProperty>().buttonPressed) replacementStr = replacement.Value + "0";
        else replacementStr = replacement.Value + Val;
        text = Regex.Replace(text, "LV" + lvl + ".R..Y.", replacementStr);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
        
    }
    
    public void UpdateChannels(String lvlVal)
    {
        int lvl = Int32.Parse(lvlVal.Substring(0,1));
        int Val = Int32.Parse(lvlVal.Substring(1,1));
        
        string path = Application.persistentDataPath + "/save.txt";
        string text = File.ReadAllText(path);
        String replacement = Regex.Match(text, "LV"+lvl+".R..Y..M........").Value;
        string replacementStr;
        
        string object_name = "channel_" + lvl + "_" + Val;

        if(!GameObject.Find(object_name).GetComponent<ButtonProperty>().buttonPressed) replacementStr = replacement.Remove(10+Val, 1).Insert(10+Val, "0");
        else replacementStr = replacement.Remove(10+Val, 1).Insert(10+Val, "1");
        text = Regex.Replace(text, "LV"+lvl+".R..Y..M........", replacementStr);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
        
    }
    
    public void UpdateBreakTime(String val)
    {
        string path = Application.persistentDataPath + "/save.txt";
        string text = File.ReadAllText(path);
        String replacement = Regex.Match(text, "BRK.##.##.M........").Value;
        string replacementStr;
        
        string object_name = "channel_B_" + val;

        if(!GameObject.Find(object_name).GetComponent<ButtonProperty>().buttonPressed) replacementStr = replacement.Remove(10+Int32.Parse(val), 1).Insert(10+Int32.Parse(val), "0");
        else replacementStr = replacement.Remove(10+Int32.Parse(val), 1).Insert(10+Int32.Parse(val), "1");
        text = Regex.Replace(text, "BRK.##.##.M........", replacementStr);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
        
    }

}
