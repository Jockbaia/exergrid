using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonProperty : MonoBehaviour, IDeselectHandler
{
    public bool buttonPressed = false;
    public GameObject group;

    public void SequencialPressure()
    {
        GameObject myEventSystem = GameObject.Find("Main Camera");
        
        
        
        if (buttonPressed == false)
        {
            if(!gameObject.FindAncestorComponent<Loader>().GetComponent<Loader>().firstStartup) GetComponentInParent<ButtonPress>().dePress();
            buttonPressed = true;
        }
        else
        {
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            group.GetComponent<ButtonPress>().Clean();
            buttonPressed = false;
        }
    }
    
    public void MixerPressure()
    {
        GameObject myEventSystem = GameObject.Find("Main Camera");

        
        if (buttonPressed == false)
        {
            // if(!gameObject.FindAncestorComponent<Loader>().GetComponent<Loader>().firstStartup) GetComponentInParent<ButtonPress>().dePress();
            buttonPressed = true;
        }
        else
        {
            buttonPressed = false;
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            ColorBlock colorBlock = this.GetComponent<Button>().colors;
            colorBlock.normalColor = Color.white;
            this.GetComponent<Button>().colors = colorBlock;
        }
    }
    
    public void OnDeselect(BaseEventData eventData)
    {
        // Debug.Log("DESEL" + this.name);
        // if(GetComponentInParent<ButtonPress>().isMultiple == false || GetComponentInParent<ButtonPress>().isSequence == true) buttonPressed = false;
    }
    
}


