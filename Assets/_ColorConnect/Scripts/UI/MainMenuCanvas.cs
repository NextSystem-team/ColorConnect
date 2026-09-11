using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform shopRect;
    [SerializeField] private Text moneyText;

    public void CloseShop()
    {
        shopRect.anchoredPosition = new Vector2(-1080, 0);
    }

    public void OpenShop()
    {
        shopRect.anchoredPosition = new Vector2(0, 0);
    }

    public void ShowAd()
    {
        AdsInitializer.Instance.GetRewardedAd(moneyText);
    }
}
