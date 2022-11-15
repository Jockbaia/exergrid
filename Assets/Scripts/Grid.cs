using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject ps;
    public GameObject[] g = {null, null, null, null, null, null, null, null, null};
    public int hoverTiles;
    private bool _yellowTiles;
    public int[] numSpikes =  {0, 0, 0, 0, 0, 0, 0};
    public int[] yellowPercentage = {0, 0, 0, 0, 0, 0, 0};
    

void Start()
    {

    }

    public void SetActive(int val) { g[val - 1].GetComponent<Tile>().SetActive(); }
    public void SetShaky(int val) { g[val - 1].GetComponent<Tile>().SetShaky(); }
    public void SetSpiky(int val) { g[val - 1].GetComponent<Tile>().SetSpiky(); }

    public void PickNewGreen(bool positive)
    {
        int newGreenNumber;

        if (positive) ps.GetComponent<Points>().add_points();
        else ps.GetComponent<Points>().Mistake();

        do newGreenNumber = Random.Range(0, 9);
        while (isFull(g[newGreenNumber]));


        int isGreen = Random.Range(0, 4);
        if (isGreen == 2 && _yellowTiles) SetShaky(newGreenNumber + 1);
        else SetActive(newGreenNumber + 1);
    }

    public void PickNewSpike(bool positive)
    {
        if (positive) ps.GetComponent<Points>().Mistake();
        int newRedNumber;

        do newRedNumber = Random.Range(0, 9);
        while (isFull(g[newRedNumber]));

        SetSpiky(newRedNumber + 1);
    }

    public void ChangeSpikes()
    {
        for (int i = 0; i < 9; i++)
        {
            if (g[i].GetComponent<Tile>().isSpiky)
            {
                PickNewSpike(false);
                g[i].GetComponent<Tile>().EmptyTile();
            }
        }
    }

    public void ResetBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            g[i].GetComponent<Tile>().EmptyTile();
        }
    }
    
    public void SoftResetBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            if (!g[i].GetComponent<Tile>().isActive) g[i].GetComponent<Tile>().EmptyTile();
        }
    }
    
    public void LevelSwitch(int level)
    {
        SoftResetBoard();
        
        if (level == 0)
        {
            _yellowTiles = false; SetActive(4);
        }

        for (int i = 0; i < numSpikes[level]; i++)
        {
            PickNewSpike(false);
        }
        
        // else if (level==2) _yellowTiles = true;
        // if (level != 0) PickNewSpike(false);
    }

    public void end_level() { for (int i = 0; i < 9; i++) g[i].GetComponent<Tile>().end_level(); }

    bool isFull(GameObject tile)
    {
        return tile.GetComponent<Tile>().isActive ||
               tile.GetComponent<Tile>().isShaky ||
               tile.GetComponent<Tile>().isSpiky;
    }


}
