using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class mixer : MonoBehaviour
{
    public AudioSource m_1;
    public AudioSource m_2;
    public AudioSource m_3;
    public AudioSource m_4;
    public AudioSource m_5;
    public AudioSource m_6;
    public AudioSource m_7;
    public AudioSource m_8;
    public Text mixerGUI;
    private bool CH1_ON = false;
    private bool CH2_ON = false;
    private bool CH3_ON = false;
    private bool CH4_ON = false;
    private bool CH5_ON = false;
    private bool CH6_ON = false;
    private bool CH7_ON = false;
    private bool CH8_ON = false;
    void Start()
    {
        // Silencing all channels
        m_1.volume = 0;
        m_2.volume = 0;
        m_3.volume = 0;
        m_4.volume = 0;
        m_5.volume = 0;
        m_6.volume = 0;
        m_7.volume = 0;
        m_8.volume = 0;
    }

    void Update()
    {
        
        if (Input.GetKeyDown("1"))
        {
            print("1");
            if (CH1_ON) FadeOut(m_1);
            else FadeIn(m_1);
            CH1_ON = !CH1_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("2"))
        {
            if (CH2_ON) FadeOut(m_2);
            else FadeIn(m_2);
            CH2_ON = !CH2_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("3"))
        {
            if (CH3_ON) FadeOut(m_3);
            else FadeIn(m_3);
            CH3_ON = !CH3_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("4"))
        {
            if (CH4_ON) FadeOut(m_4);
            else FadeIn(m_4);
            CH4_ON = !CH4_ON;
            UpdateUI();
        }
        if (Input.GetKeyDown("5"))
        {
            if (CH5_ON) FadeOut(m_5);
            else FadeIn(m_5);
            CH5_ON = !CH5_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("6"))
        {
            if (CH6_ON) FadeOut(m_6);
            else FadeIn(m_6);
            CH6_ON = !CH6_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("7"))
        {
            if (CH7_ON) FadeOut(m_7);
            else FadeIn(m_7);
            CH7_ON = !CH7_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("8"))
        {
            if (CH8_ON) FadeOut(m_8);
            else FadeIn(m_8);
            CH8_ON = !CH8_ON;
            UpdateUI();
        }
        
    }
    
    void UpdateUI ()
    {
        
        String my_txt = "";
        
        if(CH8_ON) my_txt = my_txt.Insert(0, "[*]");
        else my_txt = my_txt.Insert(0, "[ ]");
        if(CH7_ON) my_txt = my_txt.Insert(0, "[*]");
        else my_txt = my_txt.Insert(0, "[ ]");
        if(CH6_ON) my_txt = my_txt.Insert(0, "[*]");
        else my_txt = my_txt.Insert(0, "[ ]");
        if(CH5_ON) my_txt = my_txt.Insert(0, "[*]");
        else my_txt = my_txt.Insert(0, "[ ]");
        my_txt = my_txt.Insert(0, "\n");
        if(CH4_ON) my_txt = my_txt.Insert(0, "[*]");
        else my_txt = my_txt.Insert(0, "[ ]");
        if(CH3_ON) my_txt = my_txt.Insert(0, "[*]");
        else my_txt = my_txt.Insert(0, "[ ]");
        if(CH2_ON) my_txt = my_txt.Insert(0, "[*]");
        else my_txt = my_txt.Insert(0, "[ ]");
        if(CH1_ON) my_txt = my_txt.Insert(0, "[*]");
        else my_txt = my_txt.Insert(0, "[ ]");

        mixerGUI.text = my_txt;
    }

    void FadeOut (AudioSource audioSource) {
        float startVolume = audioSource.volume;
        print("[FADING OUT]");
        while (audioSource.volume > 0) {
            audioSource.volume -= 0.001f;
        }
    }
    
    void FadeIn (AudioSource audioSource) {
        float startVolume = audioSource.volume;
        print("[FADING IN]");
        while (audioSource.volume < 1) {
            audioSource.volume += 0.001f;
        }
    }
    
}

