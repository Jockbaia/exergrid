using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    private GameObject pts;
    public GameObject grid;
    public GameObject frame1, frame2, frame3, frame4;
    private GameObject[] f = {null, null, null, null};
    
    void Start()
    {
         pts = GameObject.FindGameObjectWithTag("PTS");
         f[0] = frame1;
         f[1] = frame2;
         f[2] = frame3;
         f[3] = frame4;
    }

    public void setSteps(int steps) {
        pts.GetComponent<Points>().steps = steps;
        pts.GetComponent<Points>().ptsCurrent = 0;
        
        // reset tiles
        grid.GetComponent<Grid>().ResetBoard();
        grid.GetComponent<Grid>().SetActive(4);
        
        // reset 3D bar
        for(int i=0; i<4; i++) f[i].GetComponent<CubeShrink>().update_frame(0);
    }

}
