using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    public int numOfButtons;
    public bool isMultiple;
    public bool isSequence;
    public Color selected;
    public Color deselected;
    public int defaultPress = -1;
    public Button b1, b2, b3, b4, b5, b6, b7, b8;
    private Button[] b = {null, null, null, null, null, null, null, null};

    void Start()
    {
        
        b[0] = b1; b[1] = b2; b[2] = b3; b[3] = b4;
        b[4] = b5; b[5] = b6; b[6] = b7; b[7] = b8;
        
        if(defaultPress!=-1 && (!isSequence)) Press(defaultPress);
        else if(defaultPress!=-1 && isSequence) Sequence(defaultPress);
    }
    public void Press(int id)
    {
        if (!isMultiple)for (int i = 0; i < numOfButtons; i++) Release(i);

        ColorBlock colorBlock = b[id].colors;
        colorBlock.normalColor = selected;
        b[id].colors = colorBlock;
    
    }

    public void Clean()
    {
        for (int i = 0; i < numOfButtons; i++) Release(i);
    }

    public void Sequence(int id)
    {
        for (int i = 0; i <= id; i++) Press(i);
        for (int i = id + 1; i < numOfButtons; i++) Release(i);
    }


    public void Release(int id)
    {
        ColorBlock colorBlock = b[id].colors;
        colorBlock.normalColor = deselected;
        b[id].colors = colorBlock;
    }

    public void ExternalPress(int id)
    {
        b[0] = b1; b[1] = b2; b[2] = b3; b[3] = b4;
        b[4] = b5; b[5] = b6; b[6] = b7; b[7] = b8;
        
        ColorBlock colorBlock = b[id].colors;
        colorBlock.normalColor = selected;
        b[id].colors = colorBlock;
    }
}
