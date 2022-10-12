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
    public bool isSpiky = false;
    public GameObject grid;
    public AudioSource shakySFX;
    private bool isDangerous = false;

    private void Start()
    {
        
    }

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
        if ((isActive || (isShaky && !isDangerous)) && isHover && grid.GetComponent<grid>().hoverTiles == 1)
        {
            isActive = false;
            isShaky = false;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            grid.GetComponent<grid>().pickNewSpike(false);
            grid.GetComponent<grid>().pickNewGreen(true);
            CancelInvoke();
        }
        
        // TILE IS YELLOW
        else if ((isShaky && isDangerous) && isHover && grid.GetComponent<grid>().hoverTiles == 1)
        {
            isShaky = false;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            grid.GetComponent<grid>().pickNewSpike(false);
            grid.GetComponent<grid>().pickNewGreen(false);
            shakySFX.Stop();
            CancelInvoke();
        }
        
        // TILE IS RED
        
        else if (isSpiky && isHover && grid.GetComponent<grid>().hoverTiles == 1)
        {
            isSpiky = false;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            grid.GetComponent<grid>().pickNewSpike(true);
        }
        
    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("isActive", isActive);
        GetComponent<Animator>().SetBool("isShaky", isShaky);
        GetComponent<Animator>().SetBool("isDangerous", isDangerous);
        GetComponent<Animator>().SetBool("isSpiky", isSpiky);
    }

    void OnTriggerExit(Collider collision)
    {
        grid.GetComponent<grid>().hoverTiles--;
        isHover = false;
    }

    public void setActive()
    {
        isShaky = false;
        isSpiky = false;
        isActive = true;
        GetComponent<Renderer>().material.color = Color.green;
    }
    
    public void setShaky()
    {
        isActive = false;
        isSpiky = false;
        isShaky = true;
        InvokeRepeating("switchShaky", 0, 2.0f);
        // GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void setSpiky()
    {
        isActive = false;
        isSpiky = true;
        isShaky = false;
        GetComponent<Renderer>().material.color = Color.red;
    }
    
    public void setEmpty()
    {
        isActive = false;
        isSpiky = false;
        isShaky = false;
        GetComponent<Renderer>().material.color = Color.white;
    }

    void switchShaky()
    {
        if (isShaky && isDangerous)
        {
            shakySFX.Stop();
            GetComponent<Renderer>().material.color = Color.green;
            isDangerous = false;

        } else if (isShaky && !isDangerous)
        {
            shakySFX.Play();
            GetComponent<Renderer>().material.color = Color.yellow;
            isDangerous = true;
            
        }

    }
    
}
