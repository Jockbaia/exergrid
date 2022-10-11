using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public GameObject pawn;
    public bool isActive = false;
    public bool isHover = false;
    public GameObject grid;
    
    //Detect collisions between the GameObjects with Colliders attached

    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        if (collision.gameObject == pawn)
        {
            grid.GetComponent<grid>().hoverTiles++;
            isHover = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isActive && isHover && grid.GetComponent<grid>().hoverTiles == 1)
        {
            isActive = false;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            grid.GetComponent<grid>().pickNewGreen();
        }
    }

    void OnTriggerExit(Collider collision)
    {
        grid.GetComponent<grid>().hoverTiles--;
        isHover = false;
    }

    public void setActive()
    {
        isActive = true;
    }
}
