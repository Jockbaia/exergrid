using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject ps;
    public GameObject[] g = {null, null, null, null, null, null, null, null, null};
    public int hoverTiles;
    public int currentLevel;
    public int[] numSpikes =  {0, 0, 0, 0, 0, 0, 0};
    public int[] yellowPercentage = {0, 0, 0, 0, 0, 0, 0};
    public int[,] channels = {{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0},{0,0,0,0,0,0,0,0}};
    
    public bool activeSet = false;
    public bool spikeSet = false;
    public bool refreshSet = false;
    public void SetActive(int val) { g[val - 1].GetComponent<Tile>().SetActive(); }
    public void SetShaky(int val) { g[val - 1].GetComponent<Tile>().SetShaky(); }
    public void SetSpiky(int val) { g[val - 1].GetComponent<Tile>().SetSpiky(); }

    public void PickNewGreen(bool positive)
    {
        
        if (positive) ps.GetComponent<Points>().add_points();
        else ps.GetComponent<Points>().Mistake();

        ActivePicker();

    }

    public void ActivePicker()
    {
        int newGreenNumber;
        do newGreenNumber = Random.Range(0, 9);
        while (isFull(g[newGreenNumber]));
        int isYellow = Random.Range(1, 5);
        if (isYellow <= yellowPercentage[currentLevel] && yellowPercentage[currentLevel]!= 0) SetShaky(newGreenNumber + 1);
        else SetActive(newGreenNumber + 1);
        Debug.Log("GREEN_"+ (newGreenNumber + 1) +"_SET");
        activeSet = true;
    }

    public void PickNewSpike(bool positive)
    {
        if (positive) ps.GetComponent<Points>().Mistake();
        int newRedNumber;
        do newRedNumber = Random.Range(0, 9);
        while (isFull(g[newRedNumber]));
        SetSpiky(newRedNumber + 1);
        spikeSet = true;
    }

    public void ResetMutex()
    { 
        activeSet = false; 
        spikeSet = false; 
        refreshSet = false; 
    }
    
    public void RefreshSpikes()
    {
        int newRedNumber;
        do newRedNumber = Random.Range(0, 9);
        while (isFull(g[newRedNumber]));
        SetSpiky(newRedNumber + 1);
        Debug.Log("R_" + (newRedNumber + 1));
        spikeSet = true;
    }

    public void ChangeSpikes()
    {
        for (int i = 0; i < 9; i++)
        {
            if (g[i].GetComponent<Tile>().isSpiky)
            {
                g[i].GetComponent<Tile>().EmptyTile();
                PickNewSpike(false);
            }
        }

        refreshSet = true;
    }

    public void ResetBoard()
    {
        for (int i = 0; i < 9; i++)
        {
            g[i].GetComponent<Tile>().EmptyTile();
        }
    }
    
    public void LevelSwitch(int level, bool isStart)
    {
        Debug.Log("START SETTINGS LEVEL" + level);
        currentLevel = level;
        ResetBoard();

        if (isStart) {
            SetActive(4);
            activeSet = true;
        } else {ActivePicker();}

        while (!activeSet) { }
        Debug.Log("GREEN DONE!");
        
        for (int i = 0; i < numSpikes[level]; i++)
        {
            // MUTEX - Spikes
            RefreshSpikes();
            while (!spikeSet) { }
            spikeSet = false;
            // !MUTEX
        }
        
        activeSet = false;
    }

    public void end_level() { for (int i = 0; i < 9; i++) g[i].GetComponent<Tile>().end_level(); }

    bool isFull(GameObject tile)
    {
        return tile.GetComponent<Tile>().isActive ||
               tile.GetComponent<Tile>().isShaky ||
               tile.GetComponent<Tile>().isSpiky ||
               tile.GetComponent<Tile>().isHover;
    }

    public int[] GetChannel(int id)
    {
        int[] myChannel = {0, 0, 0, 0, 0, 0, 0, 0};
        for(int i=0; i<8; i++) myChannel[i] = channels[id,i];
        return myChannel;
    }


}
