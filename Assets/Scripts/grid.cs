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
    public int activeTile;
    public int hoverTiles = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        setActive(1);
        activeTile = 1;
    }

    public void setActive(int val)
    {
        if (val == 1)
        {
            g1.GetComponent<tile>().setActive();
        }
        else if (val == 2)
        {
            g2.GetComponent<tile>().setActive();
        }
        else if (val == 3)
        {
            g3.GetComponent<tile>().setActive();
        }
        else if (val == 4)
        {
            g4.GetComponent<tile>().setActive();
        }
        else if (val == 5)
        {
            g5.GetComponent<tile>().setActive();
        }
        else if (val == 6)
        {
            g6.GetComponent<tile>().setActive();
        }
        else if (val == 7)
        {
            g7.GetComponent<tile>().setActive();
        }
        else if (val == 8)
        {
            g8.GetComponent<tile>().setActive();
        }
        else if (val == 9)
        {
            g9.GetComponent<tile>().setActive();
        }

    }
    
    public void setShaky(int val)
    {
        if (val == 1)
        {
            g1.GetComponent<tile>().setShaky();
        }
        else if (val == 2)
        {
            g2.GetComponent<tile>().setShaky();
        }
        else if (val == 3)
        {
            g3.GetComponent<tile>().setShaky();
        }
        else if (val == 4)
        {
            g4.GetComponent<tile>().setShaky();
        }
        else if (val == 5)
        {
            g5.GetComponent<tile>().setShaky();
        }
        else if (val == 6)
        {
            g6.GetComponent<tile>().setShaky();
        }
        else if (val == 7)
        {
            g7.GetComponent<tile>().setShaky();
        }
        else if (val == 8)
        {
            g8.GetComponent<tile>().setShaky();
        }
        else if (val == 9)
        {
            g9.GetComponent<tile>().setShaky();
        }

    }

    public void pickNewGreen(bool positive)
    {
        if(positive) ps.GetComponent<points>().add_points();
        else ps.GetComponent<points>().mistake();
        
        int newGreenNumber;

        do
        {
            newGreenNumber = Random.Range(1, 10);
        } while (newGreenNumber == activeTile);

        
        int isGreen = Random.Range(0, 4);
        activeTile = newGreenNumber;
        if(isGreen == 1) setShaky(newGreenNumber);
        else setActive(newGreenNumber);

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
