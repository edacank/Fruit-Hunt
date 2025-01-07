using UnityEngine;

public class Tile : MonoBehaviour
{
    public int row;
    public int column;
    public GridManager gridManager;

    private void OnMouseDown()
    {
        gridManager.TileSelected(this);
    }
}
