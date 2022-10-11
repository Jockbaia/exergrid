using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    public GameObject pawn;
    public bool isActive = false;
    public bool isShaky = false;
    public bool isHover = false;
    public GameObject grid;

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject == pawn)
        {
            grid.GetComponent<grid>().hoverTiles++;
            isHover = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
        // TILE IS GREEN
        if ((isActive) && isHover && grid.GetComponent<grid>().hoverTiles == 1)
        {
            isActive = false;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            grid.GetComponent<grid>().pickNewGreen(true);
        }
        
        // TILE IS YELLOW
        else if ((isShaky) && isHover && grid.GetComponent<grid>().hoverTiles == 1)
        {
            isShaky = false;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            grid.GetComponent<grid>().pickNewGreen(false);
        }
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("isActive", !isActive);
        GetComponent<Animator>().SetBool("isShaky", isShaky);
    }

    void OnTriggerExit(Collider collision)
    {
        grid.GetComponent<grid>().hoverTiles--;
        isHover = false;
    }

    public void setActive()
    {
        isShaky = false;
        isActive = true;
        GetComponent<Renderer>().material.color = Color.green;
    }
    
    public void setShaky()
    {
        isActive = false;
        isShaky = true;
        GetComponent<Renderer>().material.color = Color.yellow;
    }
    
}
