using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class points : MonoBehaviour
{
    mixer mx;
    public int pts;
    public int negative_streak;
    public int positive_streak;
    public Text cur_pts;
    public Text pos_streak;
    public Text neg_streak;
    public Text mix_info;
    public AudioSource point_01_sfx;
    public AudioSource point_02_sfx;
    public AudioSource point_03_sfx;
    public AudioSource point_04_sfx;
    public AudioSource point_05_sfx;
    public AudioSource point_06_sfx;
    public AudioSource new_level;
    public AudioSource point_error;
    public GameObject grid;
    private int[] last_mix;
    private string last_text;
    public int points_lvlup;
    
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
    
    public void add_points ()
    {
        // exiting CHILL MODE
        if (positive_streak == 5)
        {
            mix_info.text = String.Format(last_text);
            mx.setMixer(last_mix);
        }
        
        pts++;
        positive_streak++;
        
        var rand = new Random();
        int val = rand.Next(0, 5);

        switch (val)
        {
            case 0: point_01_sfx.Play();
                break;
            case 1: point_02_sfx.Play();
                break;
            case 2: point_03_sfx.Play();
                break;
            case 3: point_04_sfx.Play();
                break;
            case 4: point_05_sfx.Play();
                break;
            case 5: point_06_sfx.Play();
                break;
        }
        
        negative_streak = 0;
        update_points();
        audio_pos_mgmt();
    }
    
    public void mistake()
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
        if (positive_streak > 0)
        {
            neg_streak.text = "";
            pos_streak.text = String.Format("{0} combo!", positive_streak);
        }
        else if (negative_streak > 0)
        {
            neg_streak.text = String.Format("{0} combo! :(", negative_streak);
            pos_streak.text = "";
        }
        else
        {
            neg_streak.text = "";
            pos_streak.text = "";
        }
    }

    void audio_pos_mgmt()
    {
        if (pts == 0)
        {
            int[] a = {1, 0, 0, 0, 0, 0, 0, 0};
            grid.GetComponent<grid>().change_level(0);
            last_text = "stage 1/5";
            last_mix = a;
            mix_info.text = String.Format(last_text);
            mx.setMixer(a);
        }

        else if (pts == points_lvlup)
        { 
            new_level.Play();
            int[] a = {1, 0, 1, 0, 0, 0, 0, 0}; 
            grid.GetComponent<grid>().change_level(1);
            last_text = "stage 2/5";
            last_mix = a;
            mix_info.text = String.Format(last_text);
            mx.setMixer(a);
        }
        
        else if (pts == points_lvlup*2)
        { 
            new_level.Play();
            int[] a = {1, 0, 1, 0, 1, 0, 0, 0}; 
            grid.GetComponent<grid>().change_level(2);
            last_text = "stage 3/5";
            last_mix = a;
            mix_info.text = String.Format(last_text);
            mx.setMixer(a);
        }

        else if (pts == points_lvlup*3)
        { 
            new_level.Play();
            int[] a = {1, 1, 1, 0, 1, 0, 0, 0}; 
            grid.GetComponent<grid>().change_level(3);
            last_text = "stage 4/5";
            last_mix = a;
            mix_info.text = String.Format(last_text);
            mx.setMixer(a);
        }
        
        else if (pts == points_lvlup*4)
        { 
            new_level.Play();
            int[] a = {1, 1, 1, 1, 1, 0, 0, 0}; 
            grid.GetComponent<grid>().change_level(3);
            last_text = "last stage";
            last_mix = a;
            mix_info.text = String.Format(last_text);
            mx.setMixer(a);
        }
        
        else if (pts == points_lvlup*5)
        { 
            new_level.Play();
            int[] a = {1, 1, 1, 1, 1, 0, 1, 0}; 
            last_text = "completed!";
            last_mix = a;
            mix_info.text = String.Format(last_text);
            mx.setMixer(a);
        }
    }

    void audio_neg_mgmt()
    {
        if (negative_streak == 5)
        {
            mix_info.text = String.Format("Chill mode");
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
