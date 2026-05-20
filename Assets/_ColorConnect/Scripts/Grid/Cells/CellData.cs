using System.Collections.Generic;
using UnityEngine;

public class CellData   
{
    public Vector2Int gridPosition;
    public bool isPassable;
    public List<LineRenderer> lines;
    public _CellObject containedObject;

    public CellData()
    {
        gridPosition = Vector2Int.zero;
        isPassable = true;
        lines = new List<LineRenderer>();
    }

    public CellData(Vector2Int gridPosition, bool isPassable = true)
    {
        this.gridPosition = gridPosition;
        this.isPassable = isPassable;
        lines = new List<LineRenderer>();
    }
}
