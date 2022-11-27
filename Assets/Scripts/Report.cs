using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Report : MonoBehaviour
{
    
    public DateTime startTime;
    public DateTime deltaTime;
    public DateTime endTime;
    public StreamWriter sw;
    
    private string path = "Assets/report.txt";
    private bool _isFirst = true;
    
    void Start()
    {
        // Check if file already exists. If yes, delete it.     
        if (File.Exists(path)) File.Delete(path);    

        using (sw = File.CreateText(path))    
        {
            sw.WriteLine("REPORT {0}", DateTime.Now.ToString("yy-MM-dd hh:mm"));
        }  
        
    }

    public void trackGreen()
    {
        
        using (sw = File.AppendText(path))
        {
            DateTime timestamp = DateTime.Now;
            TimeSpan interval;
            
            if (!_isFirst)
            {
                interval = timestamp - deltaTime;
            }
            if (_isFirst)
            {
                startTime = timestamp;
                sw.WriteLine("Start");
                _isFirst = false;
            }
            
            sw.WriteLine("Green, {0}, {1}", timestamp.ToString("hh:mm:ss.fff"), interval);

                deltaTime = timestamp;

        } 
    }
}
