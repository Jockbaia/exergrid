using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Mixer : MonoBehaviour
{
    public float fadeInDuration, fadeOutDuration;
    public AudioSource m1, m2, m3, m4, m5, m6, m7, m8;
    public Text mixerGUI;
    public GameObject grid;
    private bool[] _c = {false, false, false, false, false, false, false, false};
    private AudioSource[] _m = {null, null, null, null, null, null, null, null};

    void Start()
    {
        _c[0] = _c[1] = _c[2] = _c[3] = _c[4] = _c[5] = _c[6] = _c[7] = false;
        _m[0] = m1; _m[1] = m2; _m[2] = m3; _m[3] = m4; _m[4] = m5; _m[5] = m6; _m[6] = m7; _m[7] = m8;
        m1.volume = m2.volume = m3.volume = m4.volume = m5.volume = m6.volume = m7.volume = m8.volume = 0;
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
                StartCoroutine(FadeAudioSource.FadeIn(_m[i], fadeInDuration));
                _c[i] = true;
            } else
            {
                StartCoroutine(FadeAudioSource.FadeOut(_m[i], fadeOutDuration));
                _c[i] = false;
            }
        }
        UpdateUI();
    }

}

