using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private GameObject line;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null) 
        { 
            Debug.LogError($"Erro: SpriteRenderer não encontrado no objeto: {transform.gameObject.name}.");
        }
        else
        {
           spriteRenderer.color = color;
        }
    }

    public void SetLine(GameObject newLine)
    {
        line = newLine;
    }

    public Color GetColor()
    {
        return color;
    }
}
