using System;
using System.Collections;
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
    private string _nameFile, _pathGrid;
    private int _stepTracker;
    private List<string> _track = new List<string>();
    

    public DateTime deltaTime, startGame, startSession, startLevel, now;
    public bool newGame = true;
    public bool newSession = true;
    public bool newLevel = true;
    public TimeSpan deltaTile, deltaGame, deltaLevel, deltaSession;

    void Start()
    {
        _stepTracker = 0;
    }

    public void TrackTile(int numTile, string message, bool isError)
    {
        now = DateTime.Now;
        

        if (newGame)
        {
            _stepTracker = 0;
            startGame = now;
            Directory.CreateDirectory(Application.persistentDataPath + "/Reports/");
            String time = GameObject.Find("timer_system").GetComponent<timer>().timeRemaining.ToString();
            String breakSeconds = GameObject.FindGameObjectWithTag("PTS").GetComponent<Points>().breakTime.ToString();
            _nameFile = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss_") + time + "t" + breakSeconds + "b";
            Directory.CreateDirectory(Application.persistentDataPath + "/Reports/" + _nameFile);
            _pathReport = Application.persistentDataPath + "/Reports/" + _nameFile + "/times.csv";
            _pathTracking = Application.persistentDataPath + "/Reports/" + _nameFile + "/positions.csv";
            _pathGrid = Application.persistentDataPath + "/Reports/" + _nameFile + "/grid.csv";
            File.Copy(Application.persistentDataPath + "/save.txt", Application.persistentDataPath + "/Reports/" + _nameFile + "/save.txt", true);
            if (File.Exists(_pathReport)) File.Delete(_pathReport);
            if (File.Exists(_pathTracking)) File.Delete(_pathTracking);
            if (File.Exists(_pathGrid)) File.Delete(_pathGrid);
            
            Invoke(nameof(Track), 0);
        
            using (sw = File.CreateText(_pathReport))
            {
                sw.WriteLine("Step,Tile,State,Tile ID,Session (current),Session (total),Level (current),Level (total),Delta Tile (s),Delta Level (s),Delta Session (s),Delta Total (s)");
            }
            using (sw = File.CreateText(_pathTracking))
            {
                sw.WriteLine("Step,Time,Cube_X,Cube_Y,Cube_Z");
            }
            using (sw = File.CreateText(_pathGrid))
            {
                sw.WriteLine("PosX,PosY,PosZ,Type,Marker");
                
                for (int i = 1; i <= 9; i++)
                {
                    sw.WriteLine(GameObject.Find(i.ToString()).GetComponent<Transform>().position.x.ToString() + "," +
                        GameObject.Find(i.ToString()).GetComponent<Transform>().position.y.ToString() + "," +
                        GameObject.Find(i.ToString()).GetComponent<Transform>().position.z.ToString()+ ",0");
                }

                drawMarkers();

            }
        }
        
        _stepTracker++;
        
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
                _stepTracker,
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
        _track.Add(_stepTracker + "," + Current_Time() + "," + Cube_Position());
        Invoke(nameof(Track), 0.1f);
    }

    String Cube_Position()
    {
        return Math.Round((decimal)cube.GetComponent<Transform>().position.x, 3) + "," + Math.Round((decimal)cube.GetComponent<Transform>().position.y, 3) + "," + Math.Round((decimal)cube.GetComponent<Transform>().position.z, 3);
    }

    String Current_Time()
    {
        now = DateTime.Now;
        deltaGame = now - startGame;
        int millisecs = deltaGame.Milliseconds + deltaGame.Seconds * 1000 + deltaGame.Minutes * 60000;
        return millisecs.ToString();
    }

    void drawMarkers()
    {
        sw.WriteLine(m1.GetComponent<Transform>().position.x.ToString() + "," +
                     m1.GetComponent<Transform>().position.y.ToString() + "," +
                     m1.GetComponent<Transform>().position.z.ToString()+ ",1");
        sw.WriteLine(m2.GetComponent<Transform>().position.x.ToString() + "," +
                     m2.GetComponent<Transform>().position.y.ToString() + "," +
                     m2.GetComponent<Transform>().position.z.ToString()+ ",1");
        sw.WriteLine(m3.GetComponent<Transform>().position.x.ToString() + "," +
                     m3.GetComponent<Transform>().position.y.ToString() + "," +
                     m3.GetComponent<Transform>().position.z.ToString()+ ",1");
        sw.WriteLine(m1.GetComponent<Transform>().position.x.ToString() + "," +
                     m1.GetComponent<Transform>().position.y.ToString() + "," +
                     m1.GetComponent<Transform>().position.z.ToString()+ ",1");

        for (int i = 0; i < 36; i=i+4)
        {
            sw.WriteLine(g[i].GetComponent<Transform>().position.x.ToString() + "," +
                         g[i].GetComponent<Transform>().position.y.ToString() + "," +
                         g[i].GetComponent<Transform>().position.z.ToString()+ ",2");
            sw.WriteLine(g[i+1].GetComponent<Transform>().position.x.ToString() + "," +
                         g[i+1].GetComponent<Transform>().position.y.ToString() + "," +
                         g[i+1].GetComponent<Transform>().position.z.ToString()+ ",2");
            sw.WriteLine(g[i+2].GetComponent<Transform>().position.x.ToString() + "," +
                         g[i+2].GetComponent<Transform>().position.y.ToString() + "," +
                         g[i+2].GetComponent<Transform>().position.z.ToString()+ ",2");
            sw.WriteLine(g[i+3].GetComponent<Transform>().position.x.ToString() + "," +
                         g[i+3].GetComponent<Transform>().position.y.ToString() + "," +
                         g[i+3].GetComponent<Transform>().position.z.ToString()+ ",2");
            sw.WriteLine(g[i].GetComponent<Transform>().position.x.ToString() + "," +
                         g[i].GetComponent<Transform>().position.y.ToString() + "," +
                         g[i].GetComponent<Transform>().position.z.ToString()+ ",2");
        }
        
    }
    
}
