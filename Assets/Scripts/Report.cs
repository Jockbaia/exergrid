using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Report : MonoBehaviour
{

    public StreamWriter sw;
    public GameObject pts;
    public GameObject cube;
    public GameObject m1, m2, m3;
    public GameObject[] g = {null, null, null, null, null, null, null, null, null, null, null, null,null, null, null, null,null, null, null, null,null, null, null, null,null, null, null, null,null, null, null, null,null, null, null, null};
    private string _pathReport = "Assets/Reports/report.csv";
    private string _pathTracking = "Assets/Trackings/track.csv";
    public string nameFile, pathGrid;
    public int stepTracker;
    private List<string> _track = new List<string>();
    

    public DateTime deltaTime, startGame, startSession, startLevel, now;
    public bool newGame = true;
    public bool newSession = true;
    public bool newLevel = true;
    public TimeSpan deltaTile, deltaGame, deltaLevel, deltaSession;

    void Start()
    {
        stepTracker = 0;
    }

    public void TrackTile(int numTile, string message, bool isError)
    {
        now = DateTime.Now;
        
        if (newGame)
        {
            stepTracker = 0;
            _track.Clear(); 
            startGame = now;
            Directory.CreateDirectory(Application.persistentDataPath + "/Reports/");
            String time = GameObject.Find("timer_system").GetComponent<timer>().timeRemaining.ToString();
            String breakSeconds = GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().breakTime.ToString();
            nameFile = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_") + time + "t" + breakSeconds + "b";
            Directory.CreateDirectory(Application.persistentDataPath + "/Reports/" + nameFile);
            _pathReport = Application.persistentDataPath + "/Reports/" + nameFile + "/times.csv";
            _pathTracking = Application.persistentDataPath + "/Reports/" + nameFile + "/positions.csv";
            pathGrid = Application.persistentDataPath + "/Reports/" + nameFile + "/grid.csv";
            File.Copy(Application.persistentDataPath + "/P" + GameObject.Find("point_system").GetComponent<Points>().preset + ".txt", Application.persistentDataPath + "/Reports/" + nameFile + "/save.txt", true);
            if (File.Exists(_pathReport)) File.Delete(_pathReport);
            if (File.Exists(_pathTracking)) File.Delete(_pathTracking);
            if (File.Exists(pathGrid)) File.Delete(pathGrid);
            
            Invoke(nameof(Track), 0);
        
            using (sw = File.CreateText(_pathReport))
            {
                sw.WriteLine("Step,Tile,State,Tile ID,Session (current),Session (total),Level (current),Level (total),Delta Tile (s),Delta Level (s),Delta Session (s),Delta Total (s)");
            }
            using (sw = File.CreateText(_pathTracking))
            {
                sw.WriteLine("Step,Time,Cube_X,Cube_Y,Cube_Z,Cube_Rot_X,Cube_Rot_Y,Cube_Rot_Z,LHand_X,LHand_Y,LHand_Z,LHand_Rot_X,LHand_Rot_Y,LHand_Rot_Z,RHand_X,RHand_Y,RHand_Z,RHand_Rot_X,RHand_Rot_Y,RHand_Rot_Z,Camera_X,Camera_Y,Camera_Z,Gaze_X,Gaze_Y,Gaze_Z");
            }
            using (sw = File.CreateText(pathGrid))
            {
                sw.WriteLine("PosX,PosY,PosZ,Type,Marker");
                
                for (int i = 1; i <= 9; i++)
                {
                    sw.WriteLine(GameObject.Find(i.ToString()).GetComponent<Transform>().position.x + "," +
                        GameObject.Find(i.ToString()).GetComponent<Transform>().position.y + "," +
                        GameObject.Find(i.ToString()).GetComponent<Transform>().position.z + ",0");
                }

                DrawMarkers();

            }
        }
        
        stepTracker++;
        
        using (sw = File.AppendText(_pathReport))
        {

            String error;
            if (newGame) deltaTime = now; newGame = false;
            if (newSession) startSession = now; newSession = false;
            if (newLevel) startLevel = now; newLevel = false;
            
            deltaTile = now - deltaTime;
            deltaGame = now - startGame;
            deltaLevel = now - startLevel;
            deltaSession = now - startSession;

            if (isError) error = "ERROR";
            else error = "OK";

            // CALCOLI FINITI
            
            sw.WriteLine("{0},{1},{2},{3},{4},{5:D2}.{6:D2}:{7:D3},{8:D2}.{9:D2}:{10:D3},{11:D2}.{12:D2}:{13:D3},{14:D2}.{15:D2}:{16:D3}", 
                stepTracker,
                message, 
                error,
                numTile, 
                pts.GetComponent<Points>().ReportData(),
                deltaTile.Minutes, 
                deltaTile.Seconds, 
                deltaTile.Milliseconds,
                deltaLevel.Minutes,
                deltaLevel.Seconds, 
                deltaLevel.Milliseconds,
                deltaSession.Minutes,
                deltaSession.Seconds, 
                deltaSession.Milliseconds,
                deltaGame.Minutes,
                deltaGame.Seconds, 
                deltaGame.Milliseconds);
            
            deltaTime = now;

        }
        
        UpdateTrack();

    }

    public void ClearTrack()
    {
        _track.Clear();
    }

    public void UpdateTrack()
    {
        using (sw = File.AppendText(_pathTracking))
        {
            foreach(var t in _track)
            {
                sw.WriteLine(t);
            }
            _track.Clear(); 
        }
    }

    void Track()
    {
        _track.Add(stepTracker + "," + Current_Time() + "," + Cube_Position() + "," + Hands_Position() + "," + Gaze_Tracking());
        Invoke(nameof(Track), 0.1f);
    }

    String Gaze_Tracking()
    {
        string a1;
        string a2;

        GameObject gc = GameObject.Find("DefaultGazeCursor(Clone)");
        GameObject hd = GameObject.Find("Main Camera");
        
        if (gc && gc.activeSelf)
        {
            a1 = Math.Round((decimal)gc.GetComponent<Transform>().position.x, 3) 
                 + "," + Math.Round((decimal)gc.GetComponent<Transform>().position.y, 3) 
                 + "," + Math.Round((decimal)gc.GetComponent<Transform>().position.z, 3);
        } else { a1 = "NaN,NaN,NaN"; }
        
        if (hd && hd.activeSelf)
        {
            a2 = Math.Round((decimal)hd.GetComponent<Transform>().position.x, 3) 
                 + "," + Math.Round((decimal)hd.GetComponent<Transform>().position.y, 3) 
                 + "," + Math.Round((decimal)hd.GetComponent<Transform>().position.z, 3);
        } else { a2 = "NaN,NaN,NaN"; }

        return a2 + "," + a1;
    }

    String Hands_Position()
    {
        string lh;
        string rh;

        GameObject lhF = GameObject.Find("Left_ShellHandRayPointer(Clone)");
        GameObject rhF = GameObject.Find("Right_ShellHandRayPointer(Clone)");
        
        if (lhF && lhF.activeSelf)
        {
            lh = Math.Round((decimal)lhF.GetComponent<Transform>().position.x, 3) 
                + "," + Math.Round((decimal)lhF.GetComponent<Transform>().position.y, 3) 
                + "," + Math.Round((decimal)lhF.GetComponent<Transform>().position.z, 3)
                + "," + Math.Round((decimal)lhF.GetComponent<Transform>().rotation.x, 3)
                + "," + Math.Round((decimal)lhF.GetComponent<Transform>().rotation.y, 3)
                + "," + Math.Round((decimal)lhF.GetComponent<Transform>().rotation.z, 3);
        } else { lh = "NaN,NaN,NaN,NaN,NaN,NaN"; }
        
        if (rhF && rhF.activeSelf)
        {
            rh = Math.Round((decimal)rhF.GetComponent<Transform>().position.x, 3) 
                + "," + Math.Round((decimal)rhF.GetComponent<Transform>().position.y, 3) 
                + "," + Math.Round((decimal)rhF.GetComponent<Transform>().position.z, 3)
                + "," + Math.Round((decimal)rhF.GetComponent<Transform>().rotation.x, 3)
                + "," + Math.Round((decimal)rhF.GetComponent<Transform>().rotation.y, 3)
                + "," + Math.Round((decimal)rhF.GetComponent<Transform>().rotation.z, 3);
        } else { rh = "NaN,NaN,NaN,NaN,NaN,NaN"; }

        return lh + "," + rh;
    }
    
    String Cube_Position()
    {
        return Math.Round((decimal) cube.GetComponent<Transform>().position.x, 3) + "," +
               Math.Round((decimal) cube.GetComponent<Transform>().position.y, 3) + "," +
               Math.Round((decimal) cube.GetComponent<Transform>().position.z, 3) + "," +
               Math.Round((decimal) cube.GetComponent<Transform>().rotation.x, 3) + "," +
               Math.Round((decimal) cube.GetComponent<Transform>().rotation.y, 3) + "," +
               Math.Round((decimal) cube.GetComponent<Transform>().rotation.z, 3);
    }
    

    String Current_Time()
    {
        now = DateTime.Now;
        deltaGame = now - startGame;
        int millisecs = deltaGame.Milliseconds + deltaGame.Seconds * 1000 + deltaGame.Minutes * 60000;
        return millisecs.ToString();
    }

    void DrawMarkers()
    {
        sw.WriteLine(m1.GetComponent<Transform>().position.x + "," +
                     m1.GetComponent<Transform>().position.y + "," +
                     m1.GetComponent<Transform>().position.z + ",1");
        sw.WriteLine(m2.GetComponent<Transform>().position.x + "," +
                     m2.GetComponent<Transform>().position.y + "," +
                     m2.GetComponent<Transform>().position.z + ",1");
        sw.WriteLine(m3.GetComponent<Transform>().position.x + "," +
                     m3.GetComponent<Transform>().position.y + "," +
                     m3.GetComponent<Transform>().position.z + ",1");
        sw.WriteLine(m1.GetComponent<Transform>().position.x + "," +
                     m1.GetComponent<Transform>().position.y + "," +
                     m1.GetComponent<Transform>().position.z + ",1");

        for (int i = 0; i < 36; i=i+4)
        {
            sw.WriteLine(g[i].GetComponent<Transform>().position.x + "," +
                         g[i].GetComponent<Transform>().position.y + "," +
                         g[i].GetComponent<Transform>().position.z + ",2");
            sw.WriteLine(g[i+1].GetComponent<Transform>().position.x + "," +
                         g[i+1].GetComponent<Transform>().position.y + "," +
                         g[i+1].GetComponent<Transform>().position.z + ",2");
            sw.WriteLine(g[i+2].GetComponent<Transform>().position.x + "," +
                         g[i+2].GetComponent<Transform>().position.y + "," +
                         g[i+2].GetComponent<Transform>().position.z + ",2");
            sw.WriteLine(g[i+3].GetComponent<Transform>().position.x + "," +
                         g[i+3].GetComponent<Transform>().position.y + "," +
                         g[i+3].GetComponent<Transform>().position.z + ",2");
            sw.WriteLine(g[i].GetComponent<Transform>().position.x + "," +
                         g[i].GetComponent<Transform>().position.y + "," +
                         g[i].GetComponent<Transform>().position.z + ",2");
        }
        
    }
    
}
