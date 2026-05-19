using UnityEngine;

public class Dot : MonoBehaviour
{
    [SerializeField] private Color color;
    public LineRenderer Line {  get; set; }

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

    public Color GetColor()
    {
        return color;
    }
}
