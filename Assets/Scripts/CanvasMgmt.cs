using UnityEngine;

public class CanvasMgmt : MonoBehaviour
{
    public GameObject menus;
    public GameObject results;
    public GameObject timer;
    private bool _isActive;
    private int _pressed;
    void Start()
    {
        menus.SetActive(false);
        results.SetActive(false);
    }
    void Update()
    {
        
        if (Input.GetKey("q") && !_isActive && _pressed == 0)
        {
            results.GetComponent<Transform>().localScale = Vector3.zero;
            menus.SetActive(true);
            timer.GetComponent<timer>().SendTime();
            _isActive = true;
            _pressed++;
        } else if (Input.GetKey("q") && _isActive && _pressed == 0)
        {
            results.GetComponent<Transform>().localScale = new Vector3(1,1,1);
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

    public void PanelToogle()
    {
        GameObject.Find("game").GetComponent<AudioSource>().Play();
        if (!_isActive)
        {
            results.GetComponent<Transform>().localScale = Vector3.zero;
            menus.SetActive(true);
            timer.GetComponent<timer>().SendTime();
            _isActive = true;
        } else if (_isActive)
        {
            results.GetComponent<Transform>().localScale = new Vector3(1,1,1);
            menus.SetActive(false);
            _isActive = false;
        }
    }
    
    public void ResultsToogle(bool isOver)
    {
        if (isOver)
        {
            results.SetActive(true);
        } else
        {
            results.SetActive(false);
        }
    }
    
}
