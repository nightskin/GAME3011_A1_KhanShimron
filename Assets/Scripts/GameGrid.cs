using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGrid : MonoBehaviour
{
    [SerializeField]
    private int deposits;

    // maximum and minimum amount of deposits
    public int minD;
    public int maxD;

    //number of turns
    public int extracts;
    public int scans;

    // current score of the game
    public int score = 0;
    // result of last scan
    public int lastFind = 0;
    
    // tile gameobject that gets created on setup
    public GameObject tilePrefab;

    //array of Tiles
    public Tile[,] tile_grid = new Tile[32, 32];
    
    // array of Tile GameObjects
    GameObject[,] obj_grid = new GameObject[32, 32];

    // UI Text elements
    public Text modetxt;
    public Text message;
    public Text resources;

    //The modes that you toggle through
    public enum Mode
    {
        SCAN,
        EXTRACT
    }
    public Mode mode;

    ///----------------- sets up the game grid
    void Setup()
    {
        // creates the grid
        for (int r = 0; r < 32; r++)
        {
            for (int c = 0; c < 32; c++)
            {
                GameObject obj = Instantiate(tilePrefab, transform);
                Tile tile = obj.GetComponent<Tile>();
                tile.underside = Color.black;
                tile.row = r;
                tile.col = c;
                tile.flipped = false;
                tile.value = 0;
                tile.value_str = "B";
                tile_grid[r,c] = tile;
                obj_grid[r,c] = obj;
            }
        }

        CoreTiles();
        SuroundTiles("Y" ,new Color(1, 0.65f, 0), 2, "O");
        SuroundTiles("O" ,Color.red, 1, "R");
    }

    void CoreTiles()
    {
        // sets deposits to a number between minD and maxD
        deposits = Random.Range(minD, maxD);
        // place the yellow tiles
        for (int i = 0; i < deposits; i++)
        {
            //choose a random tile
            int r = Random.Range(0, 32);
            int c = Random.Range(0, 32);
            //put a yellow tile there
            tile_grid[r, c].underside = Color.yellow;
            tile_grid[r, c].value = 4;
            tile_grid[r, c].value_str = "Y";
        }
    }

    void SuroundTiles(string oldSym, Color underside, int value, string symbol)
    {

        for (int r = 0; r < 32; r++)
        {
            for (int c = 0; c < 32; c++)
            {
                if (tile_grid[r, c].underside == Color.black)
                {
                    if (r > 0)
                    {
                        if (tile_grid[r - 1, c].value_str == oldSym)
                        {               
                            tile_grid[r, c].underside = underside;
                            tile_grid[r, c].value = value;
                            tile_grid[r, c].value_str = symbol;
                        }
                    }
                    if (c > 0)
                    {
                        if (tile_grid[r, c - 1].value_str == oldSym)
                        {
                            tile_grid[r, c].underside = underside;
                            tile_grid[r, c].value = value;
                            tile_grid[r, c].value_str = symbol;
                        }
                    }
                    if (r < 31)
                    {
                        if (tile_grid[r + 1, c].value_str == oldSym)
                        {
                            tile_grid[r, c].underside = underside;
                            tile_grid[r, c].value = value;
                            tile_grid[r, c].value_str = symbol;
                        }
                    }
                    if (c < 31)
                    {
                        if (tile_grid[r, c + 1].value_str == oldSym)
                        {
                            tile_grid[r, c].underside = underside;
                            tile_grid[r, c].value = value;
                            tile_grid[r, c].value_str = symbol;
                        }
                    }
                    if (r > 0 && c > 0)
                    {
                        if (tile_grid[r - 1, c - 1].value_str == oldSym)
                        {
                            tile_grid[r, c].underside = underside;
                            tile_grid[r, c].value = value;
                            tile_grid[r, c].value_str = symbol;
                        }
                    }
                    if (r < 31 && c < 31)
                    {
                        if (tile_grid[r + 1, c + 1].value_str == oldSym)
                        {
                            tile_grid[r, c].underside = underside;
                            tile_grid[r, c].value = value;
                            tile_grid[r, c].value_str = symbol;
                        }
                    }
                    if (r < 31 && c > 0)
                    {
                        if (tile_grid[r + 1, c - 1].value_str == oldSym)
                        {
                            tile_grid[r, c].underside = underside;
                            tile_grid[r, c].value = value;
                            tile_grid[r, c].value_str = symbol;
                        }
                    }
                    if (c < 31 && r > 0)
                    {
                        if (tile_grid[r - 1, c + 1].value_str == oldSym)
                        {
                            tile_grid[r, c].underside = underside;
                            tile_grid[r, c].value = value;
                            tile_grid[r, c].value_str = symbol;
                        }
                    }
                }
            }
        }
    }
    
    ///-------------------- Events
    void Start()
    {
        modetxt = GameObject.Find("Current Mode").GetComponent<Text>();
        message = GameObject.Find("Message Bar").GetComponent<Text>();
        mode = Mode.EXTRACT;
        
        Setup();
    }

    void Update()
    {
        GameObject.Find("ScoreTxt").GetComponent<Text>().text = score.ToString();
        if (mode == Mode.EXTRACT)
        {
            message.text = "Turns Left: " + extracts.ToString();
        }
        else if(mode == Mode.SCAN)
        {
            message.text = "Scans Left:" + scans.ToString() + ", Scan Found:" + lastFind.ToString();
        }

        if(extracts < 1)
        {
            PlayerPrefs.SetInt("Gold", score);
            SceneManager.LoadScene(1);
        }
    }
    
    ///-------------------- For button clicks
    public void ToggleMode()
    {
        if (mode == Mode.EXTRACT)
        {
            mode = Mode.SCAN;
        }
        else if (mode == Mode.SCAN)
        {
            mode = Mode.EXTRACT;
        }
        modetxt.text = mode.ToString();
    }

    ///-------------------- Used for Debug Purposes
    // flips over all tiles in game grid
    void RevealAll()
    {
        for (int i = 0; i < 32; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                obj_grid[i,j].GetComponent<Image>().color = tile_grid[i,j].underside;
            }
        }
    }

    // logs the game grid in a neat manner
    void LogGrid()
    {
        string msg = string.Empty;
        for (int r = 0; r < 32; r++)
        {
            for (int c = 0; c < 32; c++)
            {
                msg += tile_grid[r,c].value_str + ",";
                if (c == 31)
                {
                    msg += System.Environment.NewLine;
                }
            }
        }
        Debug.Log(msg);
    }

}
