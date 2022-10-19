// clean

using UnityEngine;
using System.Collections;

public class DragObject : MonoBehaviour
{
    
    public Camera myCamera;
     
    void OnMouseDrag()
    {
        float distanceToScreen = myCamera.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 posMove = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceToScreen ));
        var t = transform;
        t.position = new Vector3( posMove.x, t.position.y, posMove.z );
          
    }
}