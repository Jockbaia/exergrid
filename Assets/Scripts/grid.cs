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
    public int spikyTile;
    public int hoverTiles = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        setActive(1);
        activeTile = 1;
        g3.GetComponent<tile>().setSpiky();
        spikyTile = 3;

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
    
    public void setSpiky(int val)
    {
        if (val == 1)
        {
            g1.GetComponent<tile>().setSpiky();
        }
        else if (val == 2)
        {
            g2.GetComponent<tile>().setSpiky();
        }
        else if (val == 3)
        {
            g3.GetComponent<tile>().setSpiky();
        }
        else if (val == 4)
        {
            g4.GetComponent<tile>().setSpiky();
        }
        else if (val == 5)
        {
            g5.GetComponent<tile>().setSpiky();
        }
        else if (val == 6)
        {
            g6.GetComponent<tile>().setSpiky();
        }
        else if (val == 7)
        {
            g7.GetComponent<tile>().setSpiky();
        }
        else if (val == 8)
        {
            g8.GetComponent<tile>().setSpiky();
        }
        else if (val == 9)
        {
            g9.GetComponent<tile>().setSpiky();
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
    
    public void pickNewSpike(bool positive)
    {
        if(positive) ps.GetComponent<points>().mistake();
        
        int newRedNumber;

        do
        {
            newRedNumber = Random.Range(1, 10);
        } while (newRedNumber == activeTile);
        
        spikyTile = newRedNumber;
        setSpiky(newRedNumber);
    }

    public void moveSpike()
    {
        if (g1.GetComponent<tile>().isSpiky) g1.GetComponent<tile>().setEmpty();
        if (g2.GetComponent<tile>().isSpiky) g2.GetComponent<tile>().setEmpty();
        if (g3.GetComponent<tile>().isSpiky) g3.GetComponent<tile>().setEmpty();
        if (g4.GetComponent<tile>().isSpiky) g4.GetComponent<tile>().setEmpty();
        if (g5.GetComponent<tile>().isSpiky) g5.GetComponent<tile>().setEmpty();
        if (g6.GetComponent<tile>().isSpiky) g6.GetComponent<tile>().setEmpty();
        if (g7.GetComponent<tile>().isSpiky) g7.GetComponent<tile>().setEmpty();
        if (g8.GetComponent<tile>().isSpiky) g8.GetComponent<tile>().setEmpty();
        if (g9.GetComponent<tile>().isSpiky) g9.GetComponent<tile>().setEmpty();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
