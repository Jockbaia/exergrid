// Code :)
using UnityEngine;
using System.Collections;

public class dragObject : MonoBehaviour
{
    
    private Vector3 dragPosition;
    public Camera myCamera;
     
    void OnMouseDrag()
    {
        float distance_to_screen = myCamera.WorldToScreenPoint(gameObject.transform.position).z;
        Vector3 pos_move = myCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
        transform.position = new Vector3( pos_move.x, transform.position.y, pos_move.z );
          
    }
}