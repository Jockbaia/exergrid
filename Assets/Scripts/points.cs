using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class points : MonoBehaviour
{
    public int pts;
    public int negative_streak;
    public int positive_streak;
    public Text cur_pts;
    public Text pos_streak;

    // Start is called before the first frame update
    void Start()
    {
        pts = 0;
        negative_streak = 0;
        positive_streak = 0;
        update_points();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q")) add_points();

        if (Input.GetKeyDown("w")) mistake();

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
