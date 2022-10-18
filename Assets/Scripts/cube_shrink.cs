using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_shrink : MonoBehaviour
{
    private float min_shrink_x = 0.05f;
    private float max_shrink_x = 1.177f;
    private float min_shrink_y = 2.2f;
    private float max_shrink_y = 49.5f;
    public int max_points = 30;
    public Material victory_glass;
    private float cube_length;
    
    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = transform.localScale;
        if(tag == "frame_x") pos.x = 0;
        else if (tag == "frame_y") pos.x = 0;
        transform.localScale = pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void elongate(int pts)
    {
        if (pts <= max_points)
        {
            Vector3 pos = transform.localScale;
            if(tag == "frame_x") pos.x = (max_shrink_x/max_points)*pts;
            else if (tag == "frame_y") pos.x = (max_shrink_y/max_points)*pts;
            transform.localScale = pos;

            if (pts == max_points)
            {
                GetComponent<MeshRenderer> ().material = victory_glass;
            }
        } 

    }
}
