// clean
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject pawn;
    public GameObject report;
    public bool isActive;
    public bool isShaky; 
    public bool isHover;
    public bool isSpiky;
    public bool isOver;
    public int numTile;
    public bool isDangerous;
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
        if ((isActive || (isShaky && !isDangerous)) && isHover && !isOver && grid.GetComponent<Grid>().hoverTiles == 1)
        {
            if(isShaky) report.GetComponent<Report>().TrackTile(numTile, "Purple (safe)", false);
            else report.GetComponent<Report>().TrackTile(numTile, "Green", false);
            
            if(!GameObject.FindWithTag("PTS").GetComponent<Points>().SessionFinished() && 
               !GameObject.FindWithTag("PTS").GetComponent<Points>().LevelFinished())
            {
                // MUTEX Green
                grid.GetComponent<Grid>().PickNewGreen(true);
                while (!grid.GetComponent<Grid>().activeSet) {}
                grid.GetComponent<Grid>().activeSet = false;
                // MUTEX Reds
                grid.GetComponent<Grid>().ChangeSpikes();
                while (!grid.GetComponent<Grid>().refreshSet) {}
                grid.GetComponent<Grid>().refreshSet = false;
                // !MUTEX
                
                EmptyTile();
            }
            else
            {
                GameObject.FindWithTag("PTS").GetComponent<Points>().add_points();
            }
            
            CancelInvoke();
        }

        // TILE IS YELLOW
        else if (isShaky && isDangerous && isHover && !isOver && grid.GetComponent<Grid>().hoverTiles == 1)
        {
            report.GetComponent<Report>().TrackTile(numTile, "Purple (shaky)", true);
            grid.GetComponent<Grid>().PickNewGreen(false); 
            grid.GetComponent<Grid>().ChangeSpikes();
            EmptyTile();
            shakySfx.Stop();
            CancelInvoke();
        }

        // TILE IS RED
        else if (isSpiky && isHover && !isOver && grid.GetComponent<Grid>().hoverTiles == 1)
        {
            report.GetComponent<Report>().TrackTile(numTile, "Red", true);
            grid.GetComponent<Grid>().PickNewSpike(true);
            EmptyTile();
        }
    }
    

    private void Update()
    {
        GetComponent<Animator>().SetBool(IsActive, isActive);
        GetComponent<Animator>().SetBool(IsShaky, isShaky);
        GetComponent<Animator>().SetBool(IsDangerous, isDangerous);
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
        isDangerous = false;
        gameObject.GetComponent<Renderer>().material.color = default(Color);
    }

    public void SetActive()
    {
        isShaky = false;
        isSpiky = false;
        isActive = true;
        isDangerous = false;
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
        isDangerous = false;
        GetComponent<Renderer>().material.color = Color.red;
    }

    void SwitchShaky()
    {
        if (isShaky && isDangerous)
        {
            shakySfx.Stop();
            GetComponent<Renderer>().material.color = Color.green;
            isDangerous = false;

        } else if (isShaky && !isDangerous)
        {
            shakySfx.Play();
            GetComponent<Renderer>().material.color = Color.magenta;
            isDangerous = true;
        }
    }

    public void end_level()
    {
        shakySfx.Stop();
        isSpiky = false;
        isActive = false;
        isShaky = false;
        isDangerous = false;
    }
}
