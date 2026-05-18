using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    [SerializeField] private int rows = 7;
    [SerializeField] private int columns = 7;
    [SerializeField] private float margin = 1f;

    [SerializeField] private GameObject dotOutPrefab;
    [SerializeField] private GameObject dotInPrefab;

    private Grid grid;
    private CellData[,] gridData; //posições do grid

    private void Start()
    {
        grid = GetComponent<Grid>();
        gridData = new CellData[columns, rows];

        for (int y = 0; y < rows; y++) //coordenada Y
        {
            for (int x = 0; x < columns; x++) //coordenada X
            {
                Vector2Int currentTilePosition = new(x, y);
                gridData[x, y] = new CellData(currentTilePosition);
                //Cria uma célula na posição atual do grid
            }
        }

        CenterGrid();
        FitGridInCamera();
    }

    private void CenterGrid()
    {
        float gridWidth =
            (columns * grid.cellSize.x) + //Calcula a largura total da célula
            ((columns - 1) * grid.cellGap.x); //Calcula a largura total dos espaços entre as células

        float gridHeight = 
            (rows * grid.cellSize.y) +
            ((rows - 1) * grid.cellGap.y);

        Vector3 gridCenterOffset = new Vector3(
            gridWidth / 2f - grid.cellSize.x / 2f, //Calcula o deslocamento horizontal para centralizar a grade (2f - grid.cellSize.x: serve para poder centralizar de verdade o "pivot" da célula)
            gridHeight / 2f - grid.cellSize.y / 2f,
            0f
        );

        transform.position = -gridCenterOffset; //Move o grid toda para trás considerando o tamanho das células
    }

    public void FitGridInCamera()
    {
        float gridWidth =
            (columns * grid.cellSize.x) + //Calcula a largura total da célula
            ((columns - 1) * grid.cellGap.x); //Calcula a largura total dos espaços entre as células

        float gridHeight =
            (rows * grid.cellSize.y) +
            ((rows - 1) * grid.cellGap.y);

        float screenRatio =
            (float)Screen.width / Screen.height; //Pega o centro da tela

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

    //Debug pré assets
    private void OnDrawGizmos()
    {
        if (grid == null)
            grid = GetComponent<Grid>();

        if (gridData == null) return;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                Vector3 worldPosition =
                    grid.CellToWorld(
                        new Vector3Int(x, y, 0)
                    ) + (grid.cellSize / 2f);

                Gizmos.color =
                    gridData[x, y].isPassable
                    ? Color.green
                    : Color.red;

                Gizmos.DrawCube(
                    worldPosition,
                    grid.cellSize * 0.9f
                );
            }
        }
    }
}
