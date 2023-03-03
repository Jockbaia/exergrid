using System;
using UnityEngine;

public class CubeShrink : MonoBehaviour
{
    private float max_shrink_x = 1.177f;
    private float max_shrink_y = 49.5f;
    private int _maxPoints;
    public Material victoryGlass;
    private Animation _anim;
   
    void Start()
    {
        Vector3 pos = transform.localScale;
        pos.x = 0.0f;
        gameObject.SetActive(false);
        transform.localScale = pos;
        _anim = gameObject.GetComponent<Animation>();
    }
    
    public void update_frame(int pts)
    {
        _maxPoints = GameObject.FindWithTag("PTS").GetComponent<Points>().ptsMax;
        if (pts == 0) { gameObject.SetActive(false); }  
        else if (pts <= _maxPoints)
        {
            gameObject.SetActive(true);
            Vector3 pos = transform.localScale;
            if(CompareTag("frame_x")) pos.x = (max_shrink_x/_maxPoints)*pts;
            else if (CompareTag("frame_y")) pos.x = (max_shrink_y/_maxPoints)*pts;
            transform.localScale = pos;
            if (pts == _maxPoints) GetComponent<MeshRenderer>().material = victoryGlass;
        }
    }
    
    public void glow_newlevel() { _anim.Play("frame_newlevel"); }
    public void glow_point() { _anim.Play("frame_point"); }
    public void glow_error() { _anim.Play("frame_error"); }

    public void glow_final() { _anim.Play("frame_final"); }

    public void glow_loading(bool isX, int time) 
    {
        if (!isX)
        {
            if (time == 15)
            {
                _anim.Play("frame_loadingY_15"); 
                Invoke(nameof(PostBreak), 15);
            } 
            else if (time == 30)
            {
                _anim.Play("frame_loadingY_30");
                Invoke(nameof(PostBreak), 30);
            }
            else if (time == 45)
            {
                _anim.Play("frame_loadingY_45");
                Invoke(nameof(PostBreak), 45);
            }
            else if (time == 60)
            {
                _anim.Play("frame_loadingY_60");
                Invoke(nameof(PostBreak), 60);
            }
        }
        else
        {
            if (time == 15)
            {
                _anim.Play("frame_loadingX_15"); 
                Invoke(nameof(PostBreak), 15);
                Invoke(nameof(TimerSound), 12);
            } 
            else if (time == 30)
            {
                _anim.Play("frame_loadingX_30");
                Invoke(nameof(PostBreak), 30);
                Invoke(nameof(TimerSound), 27);
            }
            else if (time == 45)
            {
                _anim.Play("frame_loadingX_45");
                Invoke(nameof(PostBreak), 45);
                Invoke(nameof(TimerSound), 42);
            }
            else if (time == 60)
            {
                _anim.Play("frame_loadingX_60");
                Invoke(nameof(PostBreak), 60);
                Invoke(nameof(TimerSound), 57);
            }
        }

    }

    void PostBreak()
    {
        GameObject.Find("grid").GetComponent<Grid>().LevelSwitch(0, true);
        GameObject.Find("grid").GetComponent<Grid>().LevelSwitch(0, true);
        GameObject.FindWithTag("PTS").GetComponent<Points>().level_handler();
        GameObject.Find("report").GetComponent<Report>().startSession = DateTime.Now;
        GameObject.Find("report").GetComponent<Report>().startLevel = DateTime.Now;
        GameObject.Find("timer_system").GetComponent<timer>().startTimer();
        gameObject.SetActive(false);
    }
    
    void TimerSound()
    {
        GameObject.Find("timer").GetComponent<AudioSource>().Play();
    }

    public void RestartInvokes()
    {
        CancelInvoke(nameof(PostBreak));
        CancelInvoke(nameof(TimerSound));
    }
    

}
