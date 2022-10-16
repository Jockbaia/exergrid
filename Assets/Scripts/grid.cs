using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Random;

public class grid : MonoBehaviour
{
    public GameObject ps;
    public GameObject g1;
    public GameObject g2;
    public GameObject g3;
    public GameObject g4;
    public GameObject g5;
    public GameObject g6;
    public GameObject g7;
    public GameObject g8;
    public GameObject g9;
    public int hoverTiles = 0;
    public bool yellowTiles;
    public bool redTiles = false;
    
   
    public void setActive(int val)
    {

        GameObject[] g = {g1, g2, g3, g4, g5, g6, g7, g8, g9};
        g[val-1].GetComponent<tile>().setActive();
        
    }
    
    public void setShaky(int val)
    {
        GameObject[] g = {g1, g2, g3, g4, g5, g6, g7, g8, g9};
        g[val-1].GetComponent<tile>().setShaky();

    }
    
    public void setSpiky(int val)
    {
        GameObject[] g = {g1, g2, g3, g4, g5, g6, g7, g8, g9};
        g[val-1].GetComponent<tile>().setSpiky();

    }

    public void pickNewGreen(bool positive)
    {
        GameObject[] g = {g1, g2, g3, g4, g5, g6, g7, g8, g9};
        int newGreenNumber;
        
        if(positive) ps.GetComponent<points>().add_points();
        else ps.GetComponent<points>().mistake();
        
        do newGreenNumber = Random.Range(0, 9);
        while (g[newGreenNumber].GetComponent<tile>().isActive ||
               g[newGreenNumber].GetComponent<tile>().isShaky ||
               g[newGreenNumber].GetComponent<tile>().isSpiky);


        int isGreen = Random.Range(0, 4);
        if(isGreen == 2 && yellowTiles) {setShaky(newGreenNumber+1);}
        else setActive(newGreenNumber+1);

    }
    
    public void pickNewSpike(bool positive)
    {
        GameObject[] g = {g1, g2, g3, g4, g5, g6, g7, g8, g9};
        if(positive) ps.GetComponent<points>().mistake();
        
        int newRedNumber;

        do newRedNumber = Random.Range(0, 9);
        while (g[newRedNumber].GetComponent<tile>().isActive ||
               g[newRedNumber].GetComponent<tile>().isShaky ||
               g[newRedNumber].GetComponent<tile>().isSpiky);
        
        setSpiky(newRedNumber+1);
    }

    public void changeSpikes()
    {
        GameObject[] g = {g1, g2, g3, g4, g5, g6, g7, g8, g9};

        for (int i = 0; i < 9; i++)
        {
            if(g[i].GetComponent<tile>().isSpiky)
            {
                pickNewSpike(false);
                g[i].GetComponent<tile>().emptyTile();
            }
        }

    }

    public void change_level(int level)
    {
        if (level == 0)
        {
            yellowTiles = false;
            setActive(4);
        }
        if (level == 1)
        {
            pickNewSpike(false);
        }
        if (level == 2)
        {
            yellowTiles = true;
            pickNewSpike(false);
        }
        if (level == 3)
        {
            pickNewSpike(false);
        }
        if (level == 4)
        {
            pickNewSpike(false);
        }
    }

}
