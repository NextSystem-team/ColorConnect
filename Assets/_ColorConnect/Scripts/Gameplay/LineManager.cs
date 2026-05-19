using UnityEngine;
public class LineManager : MonoBehaviour 
{ 
    [SerializeField] private Camera mainCamera; 
    [SerializeField] private GridManager grid; 
    
    private LineRenderer currentLine; 
    private Color currentColor; 
    private CellData currentCell; 
    
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
                    currentColor = dotOut.GetColor(); 
                } 
            } 
        } 
        else if (touch.phase == TouchPhase.Moved && currentColor != default) 
        { 
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(touch.position);

            Vector3Int gridPosition = grid.Grid.WorldToCell(worldPoint); 
            CellData cell = grid.GetCellData(new Vector2Int(gridPosition.x, gridPosition.y)); 
                
            if (cell != currentCell) {
                currentCell = cell;
                print($"Nova célula checada: {cell.gridPosition}"); 
            } 

        } 
        else if (touch.phase == TouchPhase.Ended) 
        { 
            currentColor = default; 
            currentLine = null; 
            currentCell = null; 
            print("Toque finalizado"); 
        } 
    } 
}