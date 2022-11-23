using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    public float fadeInDuration, fadeOutDuration;
    public Text mixerGUI;
    public GameObject grid;
    private bool[] _c = {false, false, false, false, false, false, false, false};
    public AudioSource[] m1 = {null, null, null, null, null, null, null, null};
    public AudioSource[] m2 = {null, null, null, null, null, null, null, null};
    public AudioSource[] sil = {null, null, null, null, null, null, null, null};
    public int currentTrack;

    void Start()
    {
        _c[0] = _c[1] = _c[2] = _c[3] = _c[4] = _c[5] = _c[6] = _c[7] = false;
        m1[0].volume = m1[1].volume = m1[2].volume = m1[3].volume = m1[4].volume = m1[5].volume = m1[6].volume = m1[7].volume = 0;
        m2[0].volume = m2[1].volume = m2[2].volume = m2[3].volume = m2[4].volume = m2[5].volume = m2[6].volume = m2[7].volume = 0;
        sil[0].volume = sil[1].volume = sil[2].volume = sil[3].volume = sil[4].volume = sil[5].volume = sil[6].volume = sil[7].volume = 0;
    }
    
    public void UpdateUI ()
    {
        
        String myTxt = "";
        for (int i = 7; i >= 0; i--)
        {
            if(_c[i]) myTxt = myTxt.Insert(0, "[*]");
            else myTxt = myTxt.Insert(0, "[ ]");
        }
       
        mixerGUI.text = myTxt;
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
        }
        
        public static IEnumerator FadeOut(AudioSource audioSource, float fadeTime)
        {
            float startVolume = audioSource.volume;
 
            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
 
                yield return null;
            }
        }
        
    }

    public void SetMixer(int[] mixer)
    {
        for (int i = 0; i < 8; i++)
        {
            if (mixer[i] == 1) {
                
                switch(currentTrack) 
                {
                    case 1:
                        StartCoroutine(FadeAudioSource.FadeIn(m1[i], fadeInDuration));
                        StartCoroutine(FadeAudioSource.FadeOut(m2[i], fadeOutDuration));
                        StartCoroutine(FadeAudioSource.FadeOut(sil[i], fadeOutDuration));
                        break;
                    case 2:
                        StartCoroutine(FadeAudioSource.FadeIn(m2[i], fadeInDuration));
                        StartCoroutine(FadeAudioSource.FadeOut(m1[i], fadeOutDuration));
                        StartCoroutine(FadeAudioSource.FadeOut(sil[i], fadeOutDuration));
                        break;
                    case 0:
                        StartCoroutine(FadeAudioSource.FadeIn(sil[i], fadeInDuration));
                        StartCoroutine(FadeAudioSource.FadeOut(m1[i], fadeOutDuration));
                        StartCoroutine(FadeAudioSource.FadeOut(m2[i], fadeOutDuration));
                        break;
                }
                _c[i] = true;
            } else
            {
                StartCoroutine(FadeAudioSource.FadeOut(m1[i], fadeOutDuration));
                StartCoroutine(FadeAudioSource.FadeOut(m2[i], fadeOutDuration));
                StartCoroutine(FadeAudioSource.FadeOut(sil[i], fadeOutDuration));
                _c[i] = false;
            }
        }
        UpdateUI();
    }

}

