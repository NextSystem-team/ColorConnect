using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class LineManager : MonoBehaviour 
{ 
    [SerializeField] private Camera mainCamera; 
    [SerializeField] private GridManager grid;
    [SerializeField] private LineRenderer linePrefab;
    
    private LineRenderer currentLine; 
    private Color currentColor;
    private CellData currentCell;
    private CellData[] currentAdjacentCells;
    private List<CellData> cellsPassed;

    [SerializeField] private StageManager stageManager;
    
    private void Update() 
    { 
        if (Input.touchCount == 0) return; 
        
        LineCreator(); 
    } 
    
    private void LineCreator() { 
        Touch touch = Input.GetTouch(0); 
        
        if (touch.phase == TouchPhase.Began) { 
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(touch.position); 
            Collider2D hit = Physics2D.OverlapPoint(worldPoint); 
            
            if (hit) { 
                if (hit.transform.CompareTag("DotOut")) {

                    print("Toque em DotOut detectado"); 
                    
                    Dot dotOut = hit.GetComponent<Dot>();
                    if (dotOut.Line == null)
                    {
                        currentColor = dotOut.GetColor();

                        Vector2Int gridPosition = new(grid.Grid.WorldToCell(worldPoint).x, grid.Grid.WorldToCell(worldPoint).y);
                        CellData starterCell = grid.GetCellData(gridPosition);
                     
                        currentAdjacentCells = grid.GetAdjacentCellData(gridPosition);

                        currentLine = StartLine(currentColor, gridPosition, transform);
                        dotOut.Line = currentLine;

                        starterCell.lines.Add(currentLine);
                        cellsPassed = new List<CellData>();
                        cellsPassed.Add(starterCell);

                        currentCell = starterCell;
                    }
                } 
            } 
        } 
        else if (touch.phase == TouchPhase.Moved && currentLine != null) 
        { 
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(touch.position);
            Vector3Int gridPosition = grid.Grid.WorldToCell(worldPoint);

            int currentCellIndex = cellsPassed.IndexOf(currentCell);
            CellData cell = grid.GetCellData(new Vector2Int(gridPosition.x, gridPosition.y));

            if (cell == null) return;

            if (cell != currentCell && 
                !cell.lines.Contains(currentLine) && 
                currentAdjacentCells.Contains(cell)) 
            {
                currentLine.positionCount++;
                currentLine.SetPosition(currentLine.positionCount - 1, grid.Grid.GetCellCenterWorld(gridPosition));

                cell.lines.Add(currentLine);
                GameObject cellObject = cell.containedObject;

                if (cellObject && cellObject.CompareTag("DotIn"))
                {
                    print("DotIn encontrado");

                    Dot dotIn = cell.containedObject.GetComponent<Dot>();

                    if (dotIn.GetColor() == currentColor)
                    {
                        dotIn.Line = currentLine;

                        currentLine = null;
                        currentColor = default;
                        cellsPassed = new List<CellData>();
                        currentCell = null;
                        currentAdjacentCells = null;

                        stageManager.dotsConnected++;
                    }
                }
                else
                {
                    cellsPassed.Add(cell);
                    currentCell = cell;
                    currentAdjacentCells = grid.GetAdjacentCellData(cell.gridPosition);
                }
                
            }
            else if (cell != currentCell && 
                     currentAdjacentCells.Contains(cell) && 
                     cell.lines.Contains(currentLine) && 
                     cell == cellsPassed[currentCellIndex-1])
            {
                currentLine.positionCount--;
                currentCell.lines.Remove(currentLine);
                cellsPassed.Remove(currentCell);

                currentCell = cell;
                currentAdjacentCells = grid.GetAdjacentCellData(cell.gridPosition);
            }

        } 
        else if (touch.phase == TouchPhase.Ended) 
        {
            if (currentLine != null)
            {
                foreach (var cell in cellsPassed)
                {
                    cell.lines.Remove(currentLine);
                }

                Destroy(currentLine.gameObject);

                currentColor = default;
                currentLine = null;
                currentCell = null;
            }

            print("Toque finalizado"); 
        } 
    } 

    private LineRenderer StartLine(Color color, Vector2Int startPosition, Transform parent)
    {
        Vector3Int starterCellCenter = new(startPosition.x, startPosition.y, 0);
        Vector3 lineStartPosition = grid.Grid.GetCellCenterWorld(starterCellCenter);

        LineRenderer newLine = Instantiate(linePrefab);
        newLine.transform.parent = parent;
        newLine.transform.position = lineStartPosition;
        newLine.startColor = color;
        newLine.endColor = color;

        newLine.SetPosition(0, lineStartPosition);

        return newLine;
    }
}