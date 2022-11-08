using UnityEngine;

public class CubeShrink : MonoBehaviour
{
    private float max_shrink_x = 1.177f;
    private float max_shrink_y = 49.5f;
    private int _maxPoints;
    public Material victoryGlass;
    private Animation _anim;
    void Start()
    {
        Vector3 pos = transform.localScale;
        pos.x = 0.0f;
        gameObject.SetActive(false);
        transform.localScale = pos;
        _anim = gameObject.GetComponent<Animation>();
    }
    
    public void update_frame(int pts)
    {
        _maxPoints = GameObject.FindWithTag("PTS").GetComponent<Points>().ptsMax;
        if (pts == 0) { gameObject.SetActive(false); }  
        else if (pts <= _maxPoints)
        {
            gameObject.SetActive(true);
            Vector3 pos = transform.localScale;
            if(CompareTag("frame_x")) pos.x = (max_shrink_x/_maxPoints)*pts;
            else if (CompareTag("frame_y")) pos.x = (max_shrink_y/_maxPoints)*pts;
            transform.localScale = pos;
            if (pts == _maxPoints) GetComponent<MeshRenderer>().material = victoryGlass;
        }
    }

    public void glow_newlevel() { _anim.Play("frame_newlevel"); }
    public void glow_error() { _anim.Play("frame_error"); }
    public void glow_final() { _anim.Play("frame_final"); }

}
