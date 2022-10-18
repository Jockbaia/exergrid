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
    public bool isOver = false;
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
        if ((isActive || (isShaky && !isDangerous)) && isHover && !isOver && grid.GetComponent<grid>().hoverTiles == 1)
        {
            
            grid.GetComponent<grid>().pickNewGreen(true);
            grid.GetComponent<grid>().changeSpikes();
            emptyTile();
            CancelInvoke();
        }

        // TILE IS YELLOW
        else if ((isShaky && isDangerous) && isHover && !isOver && grid.GetComponent<grid>().hoverTiles == 1)
        {
            grid.GetComponent<grid>().pickNewGreen(false);
            grid.GetComponent<grid>().changeSpikes();
            emptyTile();
            shakySFX.Stop();
            CancelInvoke();
        }

        // TILE IS RED
        else if (isSpiky && isHover && !isOver && grid.GetComponent<grid>().hoverTiles == 1)
        {
            grid.GetComponent<grid>().pickNewSpike(true);
            emptyTile();
        }

    }

    private void Update()
    {
        GetComponent<Animator>().SetBool("isActive", isActive);
        GetComponent<Animator>().SetBool("isShaky", isShaky);
        GetComponent<Animator>().SetBool("isDangerous", isDangerous);
        GetComponent<Animator>().SetBool("isSpiky", isSpiky);
        GetComponent<Animator>().SetBool("isOver", isOver);
    }

    void OnTriggerExit(Collider collision)
    {
        grid.GetComponent<grid>().hoverTiles--;
        isHover = false;
    }

    public void emptyTile()
    {
        isActive = false;
        isShaky = false;
        isSpiky = false;
        gameObject.GetComponent<Renderer>().material.color = default(Color);
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
        GetComponent<Renderer>().material.color = default(Color);
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

    public void end_level()
    {
        GetComponent<Animation>().Play("final");
        CancelInvoke();
        isActive = false;
        isSpiky = false;
        isShaky = false;
        isDangerous = false;
        isOver = true;
        GetComponent<Renderer>().material.color = Color.green;
        shakySFX.Stop();
    }
    
}
