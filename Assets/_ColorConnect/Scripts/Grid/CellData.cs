using System.Collections.Generic;
using UnityEngine;

public class CellData   
{
    public Vector2Int gridPosition;
    public bool isPassable;
    public bool isLineLastPosition;
    public List<LineRenderer> lines;

    public CellData()
    {
        gridPosition = Vector2Int.zero;
        isPassable = true;
        isLineLastPosition = false;
        lines = new List<LineRenderer>();
    }

    public CellData(Vector2Int gridPosition, bool isPassable = true, bool isLineLastPosition = false)
    {
        this.gridPosition = gridPosition;
        this.isPassable = isPassable;
        this.isLineLastPosition = isLineLastPosition;
        lines = new List<LineRenderer>();
    }
}
