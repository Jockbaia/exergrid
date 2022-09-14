using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class points : MonoBehaviour
{
    mixer mx;
    public int pts;
    public int negative_streak;
    public int positive_streak;
    public Text cur_pts;
    public Text pos_streak;

    
    void Start()
    {
        pts = 0;
        negative_streak = 0;
        positive_streak = 0;
        mx = GameObject.FindGameObjectWithTag("MX").GetComponent<mixer>();
        update_points();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            add_points();
            int[] a = {1, 1, 1, 1, 1, 0, 0, 0};
            mx.setMixer(a);
        }

        if (Input.GetKeyDown("w"))
        {
            int[] a = {0, 0, 0, 0, 0, 1, 1, 0};
            mx.setMixer(a);
            mistake();
        }

    }
    
    void add_points ()
    {
        pts++;
        positive_streak++;
        negative_streak = 0;
        update_points();
    }
    
    void mistake()
    {
        negative_streak++;
        positive_streak = 0;
        update_points();
    }

    void update_points()
    {
        cur_pts.text = String.Format("{0} points",pts);
        if (positive_streak > 0) pos_streak.text = String.Format("{0} combo!", positive_streak);
        else pos_streak.text = "";
    }
    
}
