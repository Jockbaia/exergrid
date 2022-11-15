using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonProperty : MonoBehaviour, IDeselectHandler
{
    public bool buttonPressed = false;
    public GameObject group;

    public void pressure()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        
        if (buttonPressed == false)
        {
            Debug.Log("AO!");
            buttonPressed = true;
        }
        else
        {
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            group.GetComponent<ButtonPress>().Clean();
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonPressed = false;
    }
    
}


