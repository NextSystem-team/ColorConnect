using UnityEngine;

public class CellData   
{
    public Vector2Int gridPosition;
    public bool isPassable;

    public CellData()
    {
        gridPosition = Vector2Int.zero;
        isPassable = true;
    }

    public CellData(Vector2Int gridPosition, bool isPassable = true)
    {
        this.gridPosition = gridPosition;
        this.isPassable = isPassable;
    }
}
