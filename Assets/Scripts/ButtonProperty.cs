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

    public void SequencialPressure()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        
        if (buttonPressed == false)
        {
            buttonPressed = true;
        }
        else
        {
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            group.GetComponent<ButtonPress>().Clean();
        }
    }
    
    public void MixerPressure()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        
        if (buttonPressed == false)
        {
            this.buttonPressed = true;
        }
        else
        {
            myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
            ColorBlock colorBlock = this.GetComponent<Button>().colors;
            colorBlock.normalColor = Color.white;
            this.GetComponent<Button>().colors = colorBlock;
            this.buttonPressed = false;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if(GetComponentInParent<ButtonPress>().isMultiple == false || GetComponentInParent<ButtonPress>().isSequence == true) buttonPressed = false;
    }
    
}


