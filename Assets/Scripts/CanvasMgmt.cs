using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMgmt : MonoBehaviour
{
    public GameObject menus;
    private bool isActive = false;
    private int pressed = 0;
    void Start()
    {
        menus.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKey("q") && !isActive && pressed == 0)
        {
            menus.SetActive(true);
            isActive = true;
            pressed++;
        } else if (Input.GetKey("q") && isActive && pressed == 0)
        {
            menus.SetActive(false);
            isActive = false;
            pressed++;
        } else if (Input.GetKeyUp("q"))
        {
            pressed = 0;
        }
        
        
        
    }
}
