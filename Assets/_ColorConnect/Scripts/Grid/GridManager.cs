using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int rows = 7;
    [SerializeField] private int columns = 7;
    [SerializeField] private float margin = 1f;

    [SerializeField] private GameObject dotOutPrefab;
    [SerializeField] private GameObject dotInPrefab;
    [SerializeField] private GameObject bridgePrefab;

    public Grid Grid { get; private set; }
    private CellData[,] gridData; //posições do grid

    private void Start()
    {
        Grid = GetComponent<Grid>();
        gridData = new CellData[columns, rows];

        for (int y = 0; y < rows; y++) //coordenada Y
        {
            for (int x = 0; x < columns; x++) //coordenada X
            {
                Vector2Int currentTilePosition = new(x, y);
                gridData[x, y] = new CellData(currentTilePosition);
                //Cria uma célula na posição atual do grid

                if (x == 0 && y == 0)
                {
                    GameObject dotOut = Instantiate(dotOutPrefab);
                    dotOut.transform.parent = transform;
                    dotOut.transform.position = Grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                }else if (x == rows-1 && y == columns-1)
                {
                    GameObject dotIn = Instantiate(dotInPrefab);
                    dotIn.transform.parent = transform;
                    dotIn.transform.position = Grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    dotIn.GetComponent<_CellObject>().cell = gridData[x, y];
                    gridData[x, y].containedObject = dotIn.GetComponent<_CellObject>();
                }
                else if (x == rows - 1 && y == columns - 4)
                {
                    GameObject dotIn = Instantiate(dotInPrefab);
                    dotIn.transform.parent = transform;
                    dotIn.transform.position = Grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    dotIn.GetComponent<Dot>().SetColor(Color.blue);
                    dotIn.GetComponent<_CellObject>().cell = gridData[x, y];
                    gridData[x, y].containedObject = dotIn.GetComponent<_CellObject>();
                } else if (x == 3 && y == 0)
                {
                    GameObject dotOut = Instantiate(dotOutPrefab);
                    dotOut.transform.parent = transform;
                    dotOut.transform.position = Grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    dotOut.GetComponent<Dot>().SetColor(Color.blue);
                } else if (x == 4 && y == 4)
                {
                    GameObject bridge = Instantiate(bridgePrefab);
                    bridge.transform.parent = transform;
                    bridge.transform.position = Grid.GetCellCenterWorld(new Vector3Int(x, y, 0));
                    bridge.GetComponent<_CellObject>().cell = gridData[x, y];
                    gridData[x, y].containedObject = bridge.GetComponent<_CellObject>();
                }
            }
        }

        CenterGrid();
        FitGridInCamera();
    }

    private void CenterGrid()
    {
        float gridWidth =
            (columns * Grid.cellSize.x) + //Calcula a largura total da célula
            ((columns - 1) * Grid.cellGap.x); //Calcula a largura total dos espaços entre as células

        float gridHeight = 
            (rows * Grid.cellSize.y) +
            ((rows - 1) * Grid.cellGap.y);

        Vector3 gridCenterOffset = new Vector3(
            gridWidth / 2f, //Calcula o deslocamento horizontal para centralizar a grade. 
            gridHeight / 2f,
            0f

            //(2f - grid.cellSize.x) serve para poder centralizar de verdade o "pivot" da célula)
        );

        transform.position = -gridCenterOffset; //Move o grid toda para trás considerando o tamanho das células
    }

    public void FitGridInCamera()
    {
        float gridWidth =
            (columns * Grid.cellSize.x) + //Calcula a largura total da célula
            ((columns - 1) * Grid.cellGap.x); //Calcula a largura total dos espaços entre as células

        float gridHeight =
            (rows * Grid.cellSize.y) +
            ((rows - 1) * Grid.cellGap.y);

        float screenRatio =
            (float)UnityEngine.Device.Screen.width / UnityEngine.Device.Screen.height; //Pega o centro da tela

        float targetHeight =
            gridHeight / 2f; //Diz onde é pra centralizar verticalmente

        float targetWidth =
            (gridWidth / screenRatio) / 2f; //Diz onde é pra centralizar horizontalmente

        Camera.main.orthographicSize =
            Mathf.Max(targetHeight, targetWidth) + margin; //Ajusta o tamanho ortográfico da câmera para garantir que o grid caiba na tela,
                                                           //adicionando uma margem para não ficar tão apertado

        //Mathf.Max é usado para garantir que a câmera seja grande o suficiente para mostrar toda a grade,
        //independentemente da proporção da tela.
        //Ele escolhe o maior valor entre targetHeight e targetWidth para garantir que ambos os eixos sejam adequadamente ajustados.
    }

    public CellData GetCellData(Vector2Int gridPosition)
    {
        if (gridPosition.x < 0 || gridPosition.x >= columns ||
            gridPosition.y < 0 || gridPosition.y >= rows)
        {
            return null; // Retorna null se a posição estiver fora dos limites do grid
        }

        return gridData[gridPosition.x, gridPosition.y];
    }

    public CellData[] GetAdjacentCellData(Vector2Int gridPosition)
    {
        CellData[] adjacentCells = new CellData[4];
        adjacentCells[0] = GetCellData(new Vector2Int(gridPosition.x, gridPosition.y + 1)); // Cima
        adjacentCells[1] = GetCellData(new Vector2Int(gridPosition.x, gridPosition.y - 1)); // Baixo
        adjacentCells[2] = GetCellData(new Vector2Int(gridPosition.x - 1, gridPosition.y)); // Esquerda
        adjacentCells[3] = GetCellData(new Vector2Int(gridPosition.x + 1, gridPosition.y)); // Direita

        return adjacentCells;
    }

    //Debug pré assets
    private void OnDrawGizmos()
    {
        if (Grid == null)
            Grid = GetComponent<Grid>();

        if (gridData == null) return;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 worldPosition =
                    Grid.CellToWorld(
                        new Vector3Int(x, y, 0)
                    ) + (Grid.cellSize / 2f);

                Gizmos.color =
                    gridData[x, y].isPassable
                    ? Color.green
                    : Color.red;

                Gizmos.DrawCube(
                    worldPosition,
                    Grid.cellSize * 0.9f
                );
            }
        }
    }
}
