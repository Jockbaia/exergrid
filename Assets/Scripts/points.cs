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
    public AudioSource point_01_sfx;
    public AudioSource point_02_sfx;
    public AudioSource point_03_sfx;
    public AudioSource point_04_sfx;
    public AudioSource point_05_sfx;
    public AudioSource point_06_sfx;
    public AudioSource point_error;
    private int[] last_mix;
    
    void Start()
    {
        pts = 0;
        negative_streak = 0;
        positive_streak = 0;
        mx = GameObject.FindGameObjectWithTag("MX").GetComponent<mixer>();
        update_points();
        audio_pos_mgmt();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            add_points();
            audio_pos_mgmt();
        }

        if (Input.GetKeyDown("w"))
        {
            mistake();
            audio_neg_mgmt();
        }

    }
    
    void add_points ()
    {
        if (positive_streak == 5)
        {
            mx.setMixer(last_mix);
        }
        
        pts++;
        positive_streak++;
        point_01_sfx.Play();
        negative_streak = 0;
        update_points();
    }
    
    void mistake()
    {
        negative_streak++;
        point_error.Play();
        positive_streak = 0;
        update_points();
        audio_neg_mgmt();
    }

    void update_points()
    {
        cur_pts.text = String.Format("{0} points",pts);
        if (positive_streak > 0) pos_streak.text = String.Format("{0} combo!", positive_streak);
        else pos_streak.text = "";
    }

    void audio_pos_mgmt()
    {
        if (pts == 0)
        { 
            int[] a = {1, 0, 0, 0, 0, 0, 0, 0};
            last_mix = a;
            mx.setMixer(a);
        }

        else if (pts == 10)
        { 
            int[] a = {1, 0, 1, 0, 0, 0, 0, 0}; 
            last_mix = a;
            mx.setMixer(a);
        }
        
        else if (pts == 20)
        { 
            int[] a = {1, 0, 1, 0, 1, 0, 0, 0}; 
            last_mix = a;
            mx.setMixer(a);
        }

        else if (pts == 30)
        { 
            int[] a = {1, 1, 1, 0, 1, 0, 0, 0}; 
            last_mix = a;
            mx.setMixer(a);
        }
        
        else if (pts == 40)
        { 
            int[] a = {1, 1, 1, 1, 1, 0, 0, 0}; 
            last_mix = a;
            mx.setMixer(a);
        }
        
        else if (pts == 50)
        { 
            int[] a = {1, 1, 1, 1, 1, 0, 1, 0}; 
            last_mix = a;
            mx.setMixer(a);
        }
    }

    void audio_neg_mgmt()
    {
        if (negative_streak == 5)
        {
            if (pts < 40)
            {
                int[] a = {0, 0, 1, 0, 0, 0, 1, 0};
                mx.setMixer(a);  
            }

            else
            {
                int[] a = {0, 0, 1, 0, 0, 1, 1, 0};
                mx.setMixer(a);
            }
            
        }
        
    }
    
}
