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
    public AudioSource[] _m1 = {null, null, null, null, null, null, null, null};
    public AudioSource[] _m2 = {null, null, null, null, null, null, null, null};
    public AudioSource[] _sil = {null, null, null, null, null, null, null, null};
    public int currentTrack;

    void Start()
    {
        _c[0] = _c[1] = _c[2] = _c[3] = _c[4] = _c[5] = _c[6] = _c[7] = false;
        _m1[0].volume = _m1[1].volume = _m1[2].volume = _m1[3].volume = _m1[4].volume = _m1[5].volume = _m1[6].volume = _m1[7].volume = 0;
        _m2[0].volume = _m2[1].volume = _m2[2].volume = _m2[3].volume = _m2[4].volume = _m2[5].volume = _m2[6].volume = _m2[7].volume = 0;
        _sil[0].volume = _sil[1].volume = _sil[2].volume = _sil[3].volume = _sil[4].volume = _sil[5].volume = _sil[6].volume = _sil[7].volume = 0;
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
                if (currentTrack == 1)
                {
                    StartCoroutine(FadeAudioSource.FadeIn(_m1[i], fadeInDuration));
                    StartCoroutine(FadeAudioSource.FadeOut(_m2[i], fadeOutDuration));
                    StartCoroutine(FadeAudioSource.FadeOut(_sil[i], fadeOutDuration));
                } else if (currentTrack == 2)
                {
                    StartCoroutine(FadeAudioSource.FadeIn(_m2[i], fadeInDuration));
                    StartCoroutine(FadeAudioSource.FadeOut(_m1[i], fadeOutDuration));
                    StartCoroutine(FadeAudioSource.FadeOut(_sil[i], fadeOutDuration));
                } else if (currentTrack == 0)
                {
                    StartCoroutine(FadeAudioSource.FadeIn(_sil[i], fadeInDuration));
                    StartCoroutine(FadeAudioSource.FadeOut(_m1[i], fadeOutDuration));
                    StartCoroutine(FadeAudioSource.FadeOut(_m2[i], fadeOutDuration));
                }
                
                _c[i] = true;
            } else
            {
                StartCoroutine(FadeAudioSource.FadeOut(_m1[i], fadeOutDuration));
                StartCoroutine(FadeAudioSource.FadeOut(_m2[i], fadeOutDuration));
                StartCoroutine(FadeAudioSource.FadeOut(_sil[i], fadeOutDuration));
                _c[i] = false;
            }
        }
        UpdateUI();
    }

}

