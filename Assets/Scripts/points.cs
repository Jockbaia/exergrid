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
    public int max_pts = 30;
    public int points_lvlup = 6;
    public int negative_streak;
    public int positive_streak;
    public Text cur_pts;
    public Text pos_streak;
    public Text neg_streak;
    public Text mix_info;
    public AudioSource p01;
    public AudioSource p02;
    public AudioSource p03;
    public AudioSource p04;
    public AudioSource p05;
    public AudioSource p06;
    public AudioSource new_level;
    public AudioSource end_level;
    public AudioSource point_error;
    public GameObject grid;
    private int[] last_mix;
    private string last_text;
    
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    
    void Start()
    {
        pts = 0;
        negative_streak = 0;
        positive_streak = 0;
        mx = GameObject.FindGameObjectWithTag("MX").GetComponent<mixer>();
        update_points();
        audio_pos_mgmt();
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
        
        // updating 3D bar
        cube1.GetComponent<cube_shrink>().elongate(pts);
        cube2.GetComponent<cube_shrink>().elongate(pts);
        cube3.GetComponent<cube_shrink>().elongate(pts);
        cube4.GetComponent<cube_shrink>().elongate(pts);
        
        var rand = new Random();
        int val = rand.Next(0, 6);

        AudioSource[] sfx = {p01, p02, p03, p04, p05, p06};
        sfx[val].Play();
        
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
        cube1.GetComponent<cube_shrink>().red();
        cube2.GetComponent<cube_shrink>().red();
        cube3.GetComponent<cube_shrink>().red();
        cube4.GetComponent<cube_shrink>().red();
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
            cube1.GetComponent<cube_shrink>().glow();
            cube2.GetComponent<cube_shrink>().glow();
            cube3.GetComponent<cube_shrink>().glow();
            cube4.GetComponent<cube_shrink>().glow();
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
            cube1.GetComponent<cube_shrink>().glow();
            cube2.GetComponent<cube_shrink>().glow();
            cube3.GetComponent<cube_shrink>().glow();
            cube4.GetComponent<cube_shrink>().glow();
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
            cube1.GetComponent<cube_shrink>().glow();
            cube2.GetComponent<cube_shrink>().glow();
            cube3.GetComponent<cube_shrink>().glow();
            cube4.GetComponent<cube_shrink>().glow();
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
            cube1.GetComponent<cube_shrink>().glow();
            cube2.GetComponent<cube_shrink>().glow();
            cube3.GetComponent<cube_shrink>().glow();
            cube4.GetComponent<cube_shrink>().glow();
            new_level.Play();
            int[] a = {1, 1, 1, 1, 1, 0, 0, 0}; 
            grid.GetComponent<grid>().change_level(3);
            last_text = "last stage";
            last_mix = a;
            mix_info.text = String.Format(last_text);
            mx.setMixer(a);
        }
        
        else if (pts == max_pts)
        { 
            end_level.Play();
            cube1.GetComponent<cube_shrink>().final();
            cube2.GetComponent<cube_shrink>().final();
            cube3.GetComponent<cube_shrink>().final();
            cube4.GetComponent<cube_shrink>().final();
            grid.GetComponent<grid>().end_level();
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
