using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuySkinButton : MonoBehaviour, IPointerClickHandler
{
    public enum SkinButtonState
    {
        Buy,
        Equipped,
        NotEquipped
    }

    [SerializeField] private Skin skin;
    [SerializeField] private Image linePreviewImage;
    [SerializeField] private Image dotPreview;

    [SerializeField] private GameObject skinInfoContainer;
    [SerializeField] private Text textSkinName;
    [SerializeField] private Text textPrice;

    [SerializeField] private Text isEquippedInfoText;

    public SkinListManager skinListManager;

    [SerializeField] private float doubleClickTime = 0.3f;
    private float lastClickTime = -1f;
    private SkinButtonState currentState;

    private Material linePreviewMaterial;
    private Image imageRenderer;

    void Awake()
    {
        imageRenderer = GetComponent<Image>();

        if (linePreviewImage != null && linePreviewImage.material != null)
        {
            linePreviewMaterial = new Material(linePreviewImage.material);
            linePreviewImage.material = linePreviewMaterial;
        }
    }

    private void OnDestroy()
    {
        if (linePreviewMaterial != null)
        {
            Destroy(linePreviewMaterial);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        float currentTime = Time.unscaledTime;

        if (currentTime - lastClickTime <= doubleClickTime)
        {
            
            HandleDoubleClick();

            lastClickTime = -1f;
        }
        else
        {
            lastClickTime = currentTime;
        }
    }

    public void SetSkin(Skin skin)
    {
        this.skin = skin;
        dotPreview.sprite = skin.skinDotSprite;
        textSkinName.text = skin.skinName;
        textPrice.text = skin.price.ToString();

        SetupLinePreviewShader(skin.skinLineTexture);
    }

    private void SetupLinePreviewShader(Texture2D texture)
    {
        linePreviewMaterial.SetTexture("_SkinTexture", texture);
        linePreviewMaterial.SetColor("_LineColor", Color.white);
        linePreviewMaterial.SetVector("_SkinTextureST", new Vector4(3f, 3f, 0f, 0f));
    }

    public void SetState(SkinButtonState state)
    {
        currentState = state;

        switch (state)
        {
            case SkinButtonState.Buy:
                SetBuyState();
                break;
            case SkinButtonState.Equipped:
                SetEquippedState();
                break;
            case SkinButtonState.NotEquipped:
                SetNotEquippedState();
                break;
        }
    }

    public void UpdateState()
    {
        if (GameManager.Instance.HasSkinCheck(skin))
        {
            if (GameManager.Instance.currentSkin == skin)
            {
                SetState(SkinButtonState.Equipped);
            }
            else
            {
                SetState(SkinButtonState.NotEquipped);
            }
        }
        else
        {
            SetState(SkinButtonState.Buy);
        }
    }

    private void SetBuyState()
    {
        isEquippedInfoText.gameObject.SetActive(false);
        skinInfoContainer.SetActive(true);

        Color color = new Color32(255, 255, 255, 23);
        imageRenderer.color = color;
    }

    private void SetEquippedState()
    {
        isEquippedInfoText.gameObject.SetActive(true);
        isEquippedInfoText.text = "Equipped";
        skinInfoContainer.SetActive(false);

        Color color = new Color32(255, 210, 0, 140);
        imageRenderer.color = color;
    }

    private void SetNotEquippedState()
    {
        isEquippedInfoText.gameObject.SetActive(true);
        isEquippedInfoText.text = "Not Equipped";
        skinInfoContainer.SetActive(false);

        Color color = new Color32(255, 255, 255, 23);
        imageRenderer.color = color;
    }

    private void HandleDoubleClick()
    {
        switch (currentState)
        {
            case SkinButtonState.Buy:
                if (GameManager.Instance.money >= skin.price)
                {
                    GameManager.Instance.money -= skin.price;
                    GameManager.Instance.AddSkin(skin);
                    skinListManager.UpdateMoney();
                    skinListManager.UpdateSkinButtons();
                    SetState(SkinButtonState.Equipped);
                }
                break;
            case SkinButtonState.Equipped:
                GameManager.Instance.EquipSkin(null);
                SetState(SkinButtonState.NotEquipped);
                break;
            case SkinButtonState.NotEquipped:
                GameManager.Instance.EquipSkin(skin);
                SetState(SkinButtonState.Equipped);
                skinListManager.UpdateSkinButtons();
                break;
        }
    }
}
