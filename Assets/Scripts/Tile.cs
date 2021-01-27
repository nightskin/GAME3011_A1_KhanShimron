using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public Color underside;
    public bool flipped;
    public int value;
    public int row;
    public int col;
    public string value_str;
    GameGrid game;

    private void Start()
    {
        game = GameObject.Find("Game Grid").GetComponent<GameGrid>();
    }

    public void Activate()
    {
        if (game.mode == GameGrid.Mode.EXTRACT && !flipped)
        {
            gameObject.GetComponent<Image>().color = underside;
            flipped = true;
            GameObject.Find("Game Grid").GetComponent<GameGrid>().score += value;
            game.extracts--;
        }
        else if (game.mode == GameGrid.Mode.SCAN && game.scans > 0)
        {
            int total = value;
            if(row > 0)
            {
                total += game.tile_grid[row - 1, col].value;
            }
            if(col > 0)
            {
                total += game.tile_grid[row, col - 1].value;
            }
            if (row < 31)
            {
                total += game.tile_grid[row + 1, col].value;
            }
            if(col < 31)
            {
                total += game.tile_grid[row, col + 1].value;
            }
            if(row > 0 && col > 0)
            {
                total += game.tile_grid[row - 1, col - 1].value;
            }
            if(row < 31 && col < 31)
            {
                total += game.tile_grid[row + 1, col + 1].value;
            }
            if(row < 31 && col > 0)
            {
                total += game.tile_grid[row + 1, col - 1].value;
            }
            if(col < 31 && row > 0)
            {
                total += game.tile_grid[row - 1, col + 1].value;
            }
            game.scans--;
            game.lastFind = total;
        }
    }


}
