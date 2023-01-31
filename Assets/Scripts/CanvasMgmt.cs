using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMgmt : MonoBehaviour
{
    public GameObject menus;
    private bool _isActive = false;
    private int _pressed = 0;
    void Start()
    {
        menus.SetActive(false);
    }
    void Update()
    {
        
        if (Input.GetKey("q") && !_isActive && _pressed == 0)
        {
            menus.SetActive(true);
            _isActive = true;
            _pressed++;
        } else if (Input.GetKey("q") && _isActive && _pressed == 0)
        {
            menus.SetActive(false);
            _isActive = false;
            _pressed++;
        } else if (Input.GetKeyUp("q"))
        {
            _pressed = 0;
        } else if (Input.GetKey("escape"))
        {
            #if UNITY_STANDALONE
            Application.Quit();
            #endif
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        
    }

    public void panelToogle()
    {
        GameObject.Find("game").GetComponent<AudioSource>().Play();
        if (!_isActive)
        {
            menus.SetActive(true);
            _isActive = true;
        } else if (_isActive)
        {
            menus.SetActive(false);
            _isActive = false;
        }
    }
}
