using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    private int _currentChannel;
    public bool firstStartup = true;

    private void Start()
    {

        String report = Application.persistentDataPath + "/Reports";
        
        String path = Application.persistentDataPath + "/P1.txt";
        if (!File.Exists(path))
        {
            using(var sw = new StreamWriter(path, true))
            {
                sw.WriteLine("SE2.ST2.TM1.BT1.NL5");                     
                sw.WriteLine("LV1.R0.Y0.M00001100"); 
                sw.WriteLine("LV2.R1.Y0.M00101100");
                sw.WriteLine("LV3.R2.Y1.M00111100");                     
                sw.WriteLine("LV4.R3.Y1.M00111111"); 
                sw.WriteLine("LV5.R4.Y2.M11111111");
                sw.WriteLine("LV6.R4.Y2.M11111111"); 
                sw.WriteLine("###.##.M1.B00000101"); 
            }
        }
        
        path = Application.persistentDataPath + "/P2.txt";
        if (!File.Exists(path))
        {
            using(var sw = new StreamWriter(path, true))
            {
                sw.WriteLine("SE2.ST3.TM2.BT1.NL6");                     
                sw.WriteLine("LV1.R4.Y0.M00001100"); 
                sw.WriteLine("LV2.R4.Y0.M00101100");
                sw.WriteLine("LV3.R4.Y0.M00111100");                     
                sw.WriteLine("LV4.R4.Y0.M00111111"); 
                sw.WriteLine("LV5.R4.Y0.M10111111");
                sw.WriteLine("LV6.R4.Y0.M11111111"); 
                sw.WriteLine("###.##.M1.B00000101"); 
            }
        }
        
        path = Application.persistentDataPath + "/P3.txt";
        if (!File.Exists(path))
        {
            using(var sw = new StreamWriter(path, true))
            {
                sw.WriteLine("SE2.ST2.TM2.BT1.NL6");                     
                sw.WriteLine("LV1.R0.Y2.M00001100"); 
                sw.WriteLine("LV2.R0.Y3.M00101100");
                sw.WriteLine("LV3.R0.Y4.M00111100");                     
                sw.WriteLine("LV4.R0.Y4.M00111111"); 
                sw.WriteLine("LV5.R0.Y4.M10111111");
                sw.WriteLine("LV6.R0.Y4.M11111111"); 
                sw.WriteLine("###.##.M1.B00000101"); 
            }
        }
        
        path = Application.persistentDataPath + "/P4.txt";
        if (!File.Exists(path))
        {
            using(var sw = new StreamWriter(path, true))
            {
                sw.WriteLine("SE2.ST2.TM2.BT1.NL6");                     
                sw.WriteLine("LV1.R3.Y2.M00001100"); 
                sw.WriteLine("LV2.R3.Y3.M00101100");
                sw.WriteLine("LV3.R3.Y3.M00111100");                     
                sw.WriteLine("LV4.R4.Y4.M00111111"); 
                sw.WriteLine("LV5.R4.Y4.M10111111");
                sw.WriteLine("LV6.R4.Y4.M11111111"); 
                sw.WriteLine("###.##.M1.B00000101"); 
            }
        }
        
        path = Application.persistentDataPath + "/P5.txt";
        if (!File.Exists(path))
        {
            using(var sw = new StreamWriter(path, true))
            {
                sw.WriteLine("SE4.ST6.TM4.BT2.NL6");                     
                sw.WriteLine("LV1.R0.Y0.M00001100"); 
                sw.WriteLine("LV2.R1.Y0.M00101100");
                sw.WriteLine("LV3.R2.Y1.M00111100");                     
                sw.WriteLine("LV4.R3.Y1.M00111111"); 
                sw.WriteLine("LV5.R4.Y1.M10111111");
                sw.WriteLine("LV6.R4.Y1.M11111111"); 
                sw.WriteLine("###.##.M1.B00000101"); 
            }
        }
        
        if (!Directory.Exists(report))
        {
            Directory.CreateDirectory(report);
        }
        
        GetComponent<Loader>().ReadSavedStates(GameObject.Find("point_system").GetComponent<Points>().preset);
        firstStartup = false;
    }

    public void ReadSavedStates(int preset)
    {
        string path = Application.persistentDataPath + "/P" + preset + ".txt";
        StreamReader txt = new StreamReader(path); 
        string settings = txt.ReadLine();

        // SESSIONS
        if (settings != null)
        {
            string sessionsValue = settings.Substring(2, 1);
            GameObject.Find("session_" + sessionsValue).GetComponent<Button>().Select();
            GameObject.Find("sessions").GetComponent<ButtonPress>().ExternalPress(Int32.Parse(sessionsValue) - 1);
            GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().sessions = Int32.Parse(sessionsValue);
            
            // STEPS
            string stepsValue = settings.Substring(6, 1);
            GameObject.Find("steps_" + stepsValue).GetComponent<Button>().Select();
            GameObject.Find("steps").GetComponent<ButtonPress>().ExternalPress(Int32.Parse(stepsValue) - 1);
            GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().steps = Int32.Parse(stepsValue)*5;
        
            // BREAKS
            string breaksValue = settings.Substring(14, 1);
            GameObject.Find("break_" + breaksValue).GetComponent<Button>().Select();
            GameObject.Find("breaks").GetComponent<ButtonPress>().ExternalPress(Int32.Parse(breaksValue) - 1);
            GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().breakTime = Int32.Parse(breaksValue)*15;
        
            // TIMER
            int timerValue = Int32.Parse(settings.Substring(10, 1));
            GameObject.Find("gametime_" + timerValue).GetComponent<Button>().Select();
            GameObject.Find("gametime").GetComponent<ButtonPress>().ExternalPress(timerValue - 1);
            GameObject.Find("timer_system").GetComponent<timer>().timeValue = timerValue*90;
            GameObject.Find("timer_system").GetComponent<timer>().ResetTimer();
        
            // LEVELS 
            string numOfLevels =  settings.Substring(18, 1);
            GameObject.Find("levels_" + numOfLevels).GetComponent<Button>().Select();
            GameObject.Find("levels").GetComponent<ButtonPress>().defaultPress = Int32.Parse(numOfLevels) - 1;
            GameObject.Find("levels").GetComponent<ButtonPress>().DefaultPressMode();
            GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().levels = Int32.Parse(numOfLevels);
        
            for (int i = 0; i < 6; i++)
            {
                settings = txt.ReadLine();
                int currentLevel = i + 1;

                // SPIKES
                if (settings != null)
                {
                    int numSpikes = Int32.Parse(settings.Substring(5, 1));
                    if (numSpikes != 0)
                    {
                        GameObject.Find("grid").GetComponent<Grid>().numSpikes[currentLevel - 1] = numSpikes;
                        GameObject.Find("spikes_" + currentLevel).GetComponent<ButtonPress>().defaultPress = numSpikes-1;
                        GameObject.Find("spikes_" + currentLevel).GetComponent<ButtonPress>().DefaultPressMode();
                        GameObject.Find("spike_" + currentLevel + "_" + numSpikes).GetComponent<ButtonProperty>().SequencialPressure();
                    } else GameObject.Find("grid").GetComponent<Grid>().numSpikes[currentLevel - 1] = 0;

                    // YELLOWS
                    int numYellows = Int32.Parse(settings.Substring(8, 1));
                    if (numYellows != 0)
                    {
                        GameObject.Find("grid").GetComponent<Grid>().yellowPercentage[currentLevel - 1] = numYellows;
                        GameObject.Find("yellows_" + currentLevel).GetComponent<ButtonPress>().ExternalPress(numYellows-1);
                        GameObject.Find("yellow_" + currentLevel + "_" + numYellows).GetComponent<ButtonProperty>().SequencialPressure();
                    } else GameObject.Find("grid").GetComponent<Grid>().yellowPercentage[currentLevel - 1] = 0;
            
                    // CHANNELS
                    _currentChannel = 0;
                    for (int a = 0; a < 8; a++)
                    {
                        _currentChannel = Int32.Parse(settings.Substring(11 + a, 1));
                        if (_currentChannel == 1)
                        {
                            int b = a + 1;
                            GameObject.Find("grid").GetComponent<Grid>().channels[currentLevel-1,a] = 1;
                            GameObject.Find("channels_" + currentLevel).GetComponent<ButtonPress>().ExternalPress(a);
                            GameObject.Find("channel_" + currentLevel + "_" + b).GetComponent<ButtonProperty>().MixerPressure();
                        } else GameObject.Find("grid").GetComponent<Grid>().channels[currentLevel-1,a] = 0;
                    }
                }
            }
        
            settings = txt.ReadLine();
        
            // MUTE AUDIO
            if (settings != null)
            {
                int muteValue = Int32.Parse(settings.Substring(8, 1));
                GameObject.Find("music").GetComponent<ButtonPress>().ExternalPress(muteValue);
                GameObject.Find("mixer").GetComponent<Mixer>().currentTrack = muteValue;
        

                // BREAK CHANNELS
                _currentChannel = 0;
                for (int a = 0; a < 8; a++)
                {
                    _currentChannel = Int32.Parse(settings.Substring(11 + a, 1));
                    if (_currentChannel == 1)
                    {
                        int b = a + 1;
                        GameObject.Find("grid").GetComponent<Grid>().channels[6,a] = 1;
                        GameObject.Find("channels_B").GetComponent<ButtonPress>().ExternalPress(a);
                        GameObject.Find("channel_B_" + b).GetComponent<ButtonProperty>().MixerPressure();
                    } else GameObject.Find("grid").GetComponent<Grid>().channels[6,a] = 0;
                }
            }
        }
        
    }

    public void UpdateSaveState(String xVal)
    {
        int x = Int32.Parse(xVal.Substring(0,1));
        int val = Int32.Parse(xVal.Substring(1,1));
        
        string path = Application.persistentDataPath + "/P" + GameObject.Find("point_system").GetComponent<Points>().preset + ".txt";
        string text = File.ReadAllText(path);
        
        switch (x)
        {
            case 1:
                text = Regex.Replace(text, "SE.", "SE"+val);
                break;
            case 2:
                text = Regex.Replace(text, "ST.", "ST"+val);
                break;
            case 3:
                text = Regex.Replace(text, "TM.", "TM"+val);
                break; 
            case 4:
                text = Regex.Replace(text, "BT.", "BT"+val);
                break;
            case 5:
                text = Regex.Replace(text, "NL.", "NL"+val);
                break;
        }
        
        using (StreamWriter writer = new StreamWriter(path, false)){ 
            writer.Write(text);
        }
        
    }
    
    public void UpdateSpikes(String lvlVal)
    {
        int lvl = Int32.Parse(lvlVal.Substring(0,1));
        int val = Int32.Parse(lvlVal.Substring(1,1));
        
        string path = Application.persistentDataPath + "/P" + GameObject.Find("point_system").GetComponent<Points>().preset + ".txt";
        string text = File.ReadAllText(path);
        string pattern = "LV" + lvl + ".R.";
        string replacement;
        string objectName = "spike_" + lvl + "_" + val;
        
        if(!GameObject.Find(objectName).GetComponent<ButtonProperty>().buttonPressed) replacement = "LV" + lvl + ".R0";
        else replacement = "LV" + lvl + ".R" + val;
        text = Regex.Replace(text, pattern, replacement);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
    }
    
    public void UpdateYellows(String lvlVal)
    {
        int lvl = Int32.Parse(lvlVal.Substring(0,1));
        int val = Int32.Parse(lvlVal.Substring(1,1));
        
        string path = Application.persistentDataPath + "/P" + GameObject.Find("point_system").GetComponent<Points>().preset + ".txt";
        string text = File.ReadAllText(path);
        var replacement = Regex.Match(text, "LV" + lvl + ".R..Y");
        string replacementStr;
        
        string objectName = "yellow_" + lvl + "_" + val;
        
        if(!GameObject.Find(objectName).GetComponent<ButtonProperty>().buttonPressed) replacementStr = replacement.Value + "0";
        else replacementStr = replacement.Value + val;
        text = Regex.Replace(text, "LV" + lvl + ".R..Y.", replacementStr);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
        
    }
    
    public void UpdateChannels(String lvlVal)
    {
        int lvl = Int32.Parse(lvlVal.Substring(0,1));
        int val = Int32.Parse(lvlVal.Substring(1,1));
        
        string path = Application.persistentDataPath + "/P" + GameObject.Find("point_system").GetComponent<Points>().preset + ".txt";
        string text = File.ReadAllText(path);
        String replacement = Regex.Match(text, "LV"+lvl+".R..Y..M........").Value;
        string replacementStr;
        
        string objectName = "channel_" + lvl + "_" + val;

        if(!GameObject.Find(objectName).GetComponent<ButtonProperty>().buttonPressed) replacementStr = replacement.Remove(10+val, 1).Insert(10+val, "0");
        else replacementStr = replacement.Remove(10+val, 1).Insert(10+val, "1");
        text = Regex.Replace(text, "LV"+lvl+".R..Y..M........", replacementStr);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
        
    }
    
    public void UpdateBreakTime(String val)
    {
        string path = Application.persistentDataPath + "/P" + GameObject.Find("point_system").GetComponent<Points>().preset + ".txt";
        string text = File.ReadAllText(path);
        String replacement = Regex.Match(text, "###.##.M..B........").Value;
        string replacementStr;
        
        string objectName = "channel_B_" + val;

        if(!GameObject.Find(objectName).GetComponent<ButtonProperty>().buttonPressed) replacementStr = replacement.Remove(10+Int32.Parse(val), 1).Insert(10+Int32.Parse(val), "0");
        else replacementStr = replacement.Remove(10+Int32.Parse(val), 1).Insert(10+Int32.Parse(val), "1");
        text = Regex.Replace(text, "###.##.M..B........", replacementStr);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
        
    }
    
    public void UpdateMute(int val)
    {
        string path = Application.persistentDataPath + "/P" + GameObject.Find("point_system").GetComponent<Points>().preset + ".txt";
        string text = File.ReadAllText(path);
        String replacement = Regex.Match(text, "###.##.M..B........").Value;
        string replacementStr;
        
        if(val == 1) GameObject.Find("Light").GetComponent<Button>().Select();
        else GameObject.Find("Mute").GetComponent<Button>().Select();
        GameObject.Find("music").GetComponent<ButtonPress>().ExternalPress(val);
        GameObject.Find("mixer").GetComponent<Mixer>().currentTrack = val; 
            
        replacementStr = replacement.Remove(8, 1).Insert(8, val.ToString());
        text = Regex.Replace(text, "###.##.M..B........", replacementStr);
        
        using (StreamWriter writer = new StreamWriter(path, false)) writer.Write(text);
    }

}