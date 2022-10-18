using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_shrink : MonoBehaviour
{
    private float min_shrink_x = 0.05f;
    private float max_shrink_x = 1.177f;
    private float min_shrink_y = 2.2f;
    private float max_shrink_y = 49.5f;
    private int max_points;
    public Material victory_glass;
    private float cube_length;
    private Animation anim;
    void Start()
    {
        Vector3 pos = transform.localScale;
        pos.x = 0.0f;
        gameObject.SetActive(false);
        transform.localScale = pos;
        anim = gameObject.GetComponent<Animation>();
        max_points = GameObject.FindWithTag("PTS").GetComponent<points>().max_pts;
        
    }
    
    public void elongate(int pts)
    {
        if (pts <= max_points)
        {
            gameObject.SetActive(true);
            Vector3 pos = transform.localScale;
            if(tag == "frame_x") pos.x = (max_shrink_x/max_points)*pts;
            else if (tag == "frame_y") pos.x = (max_shrink_y/max_points)*pts;
            transform.localScale = pos;

            if (pts == max_points)
            {
                GetComponent<MeshRenderer>().material = victory_glass;
            }
        }
    }

    public void glow()
    {
        anim.Play("frame-newlevel");
    }
    
    public void red()
    {
        anim.Play("frame_error");
    }
    
    public void final()
    {
        anim.Play("final");
    }
    
}
