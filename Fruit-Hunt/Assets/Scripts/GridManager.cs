using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows = 8;
    public int columns = 8;
    public float tileSize = 1.0f;
    public GameObject[] tilePrefabs; // Array of tile sprites

    private GameObject[,] grid;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new GameObject[rows, columns];
        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(col * tileSize, row * tileSize);
                GameObject randomTile = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
                grid[row, col] = Instantiate(randomTile, position, Quaternion.identity, transform);
            }
        }
    }
    private Tile firstSelectedTile;

public void TileSelected(Tile selectedTile)
{
    if (firstSelectedTile == null)
    {
        firstSelectedTile = selectedTile;
    }
    else
    {
        SwapTiles(firstSelectedTile, selectedTile);
        firstSelectedTile = null;
    }
}

void SwapTiles(Tile tileA, Tile tileB)
{
    Vector2 tempPosition = tileA.transform.position;
    tileA.transform.position = tileB.transform.position;
    tileB.transform.position = tempPosition;

    // Swap row and column values
    int tempRow = tileA.row;
    int tempCol = tileA.column;
    tileA.row = tileB.row;
    tileA.column = tileB.column;
    tileB.row = tempRow;
    tileB.column = tempCol;
  //  CheckMatches();
}
void CheckMatches()
{
    for (int row = 0; row < rows; row++)
    {
        for (int col = 0; col < columns - 2; col++)
        {
            if (grid[row, col] != null && grid[row, col + 1] != null && grid[row, col + 2] != null)
            {
                if (grid[row, col].tag == grid[row, col + 1].tag &&
                    grid[row, col].tag == grid[row, col + 2].tag)
                {
                    Destroy(grid[row, col]);
                    Destroy(grid[row, col + 1]);
                    Destroy(grid[row, col + 2]);
                }
            }
        }
    }

    // Repeat the same for columns
}
void RefillGrid()
{
    for (int col = 0; col < columns; col++)
    {
        for (int row = rows - 1; row >= 0; row--)
        {
            if (grid[row, col] == null)
            {
                for (int newRow = row - 1; newRow >= 0; newRow--)
                {
                    if (grid[newRow, col] != null)
                    {
                        grid[row, col] = grid[newRow, col];
                        grid[newRow, col] = null;
                        grid[row, col].transform.position = new Vector2(col * tileSize, row * tileSize);
                        break;
                    }
                }

                if (grid[row, col] == null)
                {
                    GameObject randomTile = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
                    grid[row, col] = Instantiate(randomTile, new Vector2(col * tileSize, row * tileSize), Quaternion.identity, transform);
                }
            }
        }
    }
}


}
