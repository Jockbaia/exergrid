// clean
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject pawn;
    public bool isActive;
    public bool isShaky; 
    public bool isHover;
    public bool isSpiky;
    public bool isOver; 
    private bool _isDangerous;
    public GameObject grid;
    public AudioSource shakySfx;
    private static readonly int IsActive = Animator.StringToHash("isActive");
    private static readonly int IsShaky = Animator.StringToHash("isShaky");
    private static readonly int IsDangerous = Animator.StringToHash("isDangerous");
    private static readonly int IsSpiky = Animator.StringToHash("isSpiky");
    private static readonly int IsOver = Animator.StringToHash("isOver");

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == pawn)
        {
            grid.GetComponent<Grid>().hoverTiles++;
            isHover = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        // TILE IS GREEN
        if ((isActive || (isShaky && !_isDangerous)) && isHover && !isOver && grid.GetComponent<Grid>().hoverTiles == 1)
        {
            grid.GetComponent<Grid>().PickNewGreen(true);
            grid.GetComponent<Grid>().ChangeSpikes();
            EmptyTile();
            CancelInvoke();
        }

        // TILE IS YELLOW
        else if ((isShaky && _isDangerous) && isHover && !isOver && grid.GetComponent<Grid>().hoverTiles == 1)
        {
            grid.GetComponent<Grid>().PickNewGreen(false);
            grid.GetComponent<Grid>().ChangeSpikes();
            EmptyTile();
            shakySfx.Stop();
            CancelInvoke();
        }

        // TILE IS RED
        else if (isSpiky && isHover && !isOver && grid.GetComponent<Grid>().hoverTiles == 1)
        {
            grid.GetComponent<Grid>().PickNewSpike(true);
            EmptyTile();
        }

    }

    private void Update()
    {
        GetComponent<Animator>().SetBool(IsActive, isActive);
        GetComponent<Animator>().SetBool(IsShaky, isShaky);
        GetComponent<Animator>().SetBool(IsDangerous, _isDangerous);
        GetComponent<Animator>().SetBool(IsSpiky, isSpiky);
        GetComponent<Animator>().SetBool(IsOver, isOver);
    }

    void OnTriggerExit(Collider collision)
    {
        grid.GetComponent<Grid>().hoverTiles--;
        isHover = false;
    }

    public void EmptyTile()
    {
        isActive = false;
        isShaky = false;
        isSpiky = false;
        gameObject.GetComponent<Renderer>().material.color = default(Color);
    }

    public void SetActive()
    {
        isShaky = false;
        isSpiky = false;
        isActive = true;
        GetComponent<Renderer>().material.color = Color.green;
    }
    
    public void SetShaky()
    {
        isActive = false;
        isSpiky = false;
        isShaky = true;
        InvokeRepeating("SwitchShaky", 0, 2.0f);
    }

    public void SetSpiky()
    {
        isActive = false;
        isSpiky = true;
        isShaky = false;
        GetComponent<Renderer>().material.color = Color.red;
    }

    void SwitchShaky()
    {
        if (isShaky && _isDangerous)
        {
            shakySfx.Stop();
            GetComponent<Renderer>().material.color = Color.green;
            _isDangerous = false;

        } else if (isShaky && !_isDangerous)
        {
            shakySfx.Play();
            GetComponent<Renderer>().material.color = Color.yellow;
            _isDangerous = true;
        }
    }

    public void end_level()
    {
        GetComponent<Animation>().Play("frame_final");
        isActive = false;
        isSpiky = false;
        isShaky = false;
        _isDangerous = false;
        isOver = true;
        GetComponent<Renderer>().material.color = Color.green;
        shakySfx.Stop();
    }
}
