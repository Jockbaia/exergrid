using Microsoft.MixedReality.Toolkit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonProperty : MonoBehaviour
{
    public bool buttonPressed;
    public GameObject group;

    public void SequencialPressure()
    {
        GameObject myEventSystem = GameObject.Find("Main Camera");
        
        if (buttonPressed == false)
        {
            if(!gameObject.FindAncestorComponent<Loader>().GetComponent<Loader>().firstStartup) GetComponentInParent<ButtonPress>().UnPress();
            buttonPressed = true;
        }
        else
        {
            myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
            group.GetComponent<ButtonPress>().Clean();
            buttonPressed = false;
        }
    }

    public void MixerPressure()
    {
        GameObject myEventSystem = GameObject.Find("Main Camera");

        
        if (buttonPressed == false) buttonPressed = true;
        else
        {
            buttonPressed = false;
            myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null);
            ColorBlock colorBlock = this.GetComponent<Button>().colors;
            colorBlock.normalColor = Color.white;
            this.GetComponent<Button>().colors = colorBlock;
        }
    }

}


