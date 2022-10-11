using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class mixer : MonoBehaviour
{
    public float fadein_duration;
    public float fadeout_duration;
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
            if (CH1_ON) StartCoroutine(FadeAudioSource.FadeOut(m_1, fadeout_duration));
            else StartCoroutine(FadeAudioSource.FadeIn(m_1, fadein_duration));
            CH1_ON = !CH1_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("2"))
        {
            if (CH2_ON) StartCoroutine(FadeAudioSource.FadeOut(m_2, fadeout_duration));
            else StartCoroutine(FadeAudioSource.FadeIn(m_2, fadein_duration));
            CH2_ON = !CH2_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("3"))
        {
            if (CH3_ON) StartCoroutine(FadeAudioSource.FadeOut(m_3, fadeout_duration));
            else StartCoroutine(FadeAudioSource.FadeIn(m_3, fadein_duration));
            CH3_ON = !CH3_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("4"))
        {
            if (CH4_ON) StartCoroutine(FadeAudioSource.FadeOut(m_4, fadeout_duration));
            else StartCoroutine(FadeAudioSource.FadeIn(m_4, fadein_duration));
            CH4_ON = !CH4_ON;
            UpdateUI();
        }
        if (Input.GetKeyDown("5"))
        {
            if (CH5_ON) StartCoroutine(FadeAudioSource.FadeOut(m_5, fadeout_duration));
            else StartCoroutine(FadeAudioSource.FadeIn(m_5, fadein_duration));
            CH5_ON = !CH5_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("6"))
        {
            if (CH6_ON) StartCoroutine(FadeAudioSource.FadeOut(m_6, fadeout_duration));
            else StartCoroutine(FadeAudioSource.FadeIn(m_6, fadein_duration));
            CH6_ON = !CH6_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("7"))
        {
            if (CH7_ON) StartCoroutine(FadeAudioSource.FadeOut(m_7, fadeout_duration));
            else StartCoroutine(FadeAudioSource.FadeIn(m_7, fadein_duration));
            CH7_ON = !CH7_ON;
            UpdateUI();
        }
        
        if (Input.GetKeyDown("8"))
        {
            if (CH8_ON) StartCoroutine(FadeAudioSource.FadeOut(m_8, fadeout_duration));
            else StartCoroutine(FadeAudioSource.FadeIn(m_8, fadein_duration));
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

    public static class FadeAudioSource {
        public static IEnumerator FadeIn(AudioSource audioSource, float duration)
        {
            float currentTime = 0;
            float start = audioSource.volume;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(start, 1, currentTime / duration);
                yield return null;
            }
            yield break;
        }
        
        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;
 
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
 
                yield return null;
            }
 
            // audioSource.Stop();
            // audioSource.volume = startVolume;
        }
        
    }

    public void setMixer(int[] mixer)
    {

        if (mixer[0] == 1) {
            StartCoroutine(FadeAudioSource.FadeIn(m_1, fadein_duration));
            CH1_ON = true;
        } else
        {
            StartCoroutine(FadeAudioSource.FadeOut(m_1, fadeout_duration));
            CH1_ON = false;
        }
        
        if (mixer[1] == 1) {
            StartCoroutine(FadeAudioSource.FadeIn(m_2, fadein_duration));
            CH2_ON = true;
        } else
        {
            StartCoroutine(FadeAudioSource.FadeOut(m_2, fadeout_duration));
            CH2_ON = false;
        }
        
        if (mixer[2] == 1) {
            StartCoroutine(FadeAudioSource.FadeIn(m_3, fadein_duration));
            CH3_ON = true;
        } else
        {
            StartCoroutine(FadeAudioSource.FadeOut(m_3, fadeout_duration));
            CH3_ON = false;
        }
        
        if (mixer[3] == 1) {
            StartCoroutine(FadeAudioSource.FadeIn(m_4, fadein_duration));
            CH4_ON = true;
        } else
        {
            StartCoroutine(FadeAudioSource.FadeOut(m_4, fadeout_duration));
            CH4_ON = false;
        }
        
        if (mixer[4] == 1) {
            StartCoroutine(FadeAudioSource.FadeIn(m_5, fadein_duration));
            CH5_ON = true;
        } else
        {
            StartCoroutine(FadeAudioSource.FadeOut(m_5, fadeout_duration));
            CH5_ON = false;
        }
        
        if (mixer[5] == 1) {
            StartCoroutine(FadeAudioSource.FadeIn(m_6, fadein_duration));
            CH6_ON = true;
        } else
        {
            StartCoroutine(FadeAudioSource.FadeOut(m_6, fadeout_duration));
            CH6_ON = false;
        }
        
        if (mixer[6] == 1) {
            StartCoroutine(FadeAudioSource.FadeIn(m_7, fadein_duration));
            CH7_ON = true;
        } else
        {
            StartCoroutine(FadeAudioSource.FadeOut(m_7, fadeout_duration));
            CH7_ON = false;
        }
        
        if (mixer[7] == 1) {
            StartCoroutine(FadeAudioSource.FadeIn(m_8, fadein_duration));
            CH8_ON = true;
        } else
        {
            StartCoroutine(FadeAudioSource.FadeOut(m_8, fadeout_duration));
            CH8_ON = false;
        }

        UpdateUI();
        
    }

}

