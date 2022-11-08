using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject ps;
    public GameObject g1, g2, g3, g4, g5, g6, g7, g8, g9;
    private GameObject[] g = {null, null, null, null, null, null, null, null, null};
    public int hoverTiles;
    private bool _yellowTiles;

    void Start()
    {
        g[0] = g1;
        g[1] = g2;
        g[2] = g3;
        g[3] = g4;
        g[4] = g5;
        g[5] = g6;
        g[6] = g7;
        g[7] = g8;
        g[8] = g9;
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
    
    public void Rules(int level)
    {
        if (level == 0) {_yellowTiles = false; SetActive(4);}
        else if (level==2) _yellowTiles = true;
        if (level != 0) PickNewSpike(false);
    }

    public void end_level() { for (int i = 0; i < 9; i++) g[i].GetComponent<Tile>().end_level(); }

    bool isFull(GameObject tile)
    {
        return tile.GetComponent<Tile>().isActive ||
               tile.GetComponent<Tile>().isShaky ||
               tile.GetComponent<Tile>().isSpiky;
    }


}
