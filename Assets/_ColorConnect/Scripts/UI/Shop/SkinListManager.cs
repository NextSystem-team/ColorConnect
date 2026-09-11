using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinListManager : MonoBehaviour
{
    [SerializeField] private GameObject skinButtonPrefab;
    [SerializeField] private SkinsListSO skinsListSO;

    [SerializeField] private Text moneyText;

    private List<BuySkinButton> skinButtons = new List<BuySkinButton>();

    void Start()
    {
        UpdateMoney();

        foreach (var skin in skinsListSO.skins)
        {
            GameObject skinButtonObj = Instantiate(skinButtonPrefab, transform);
            BuySkinButton skinButton = skinButtonObj.GetComponent<BuySkinButton>();
            skinButton.skinListManager = this;
            skinButton.SetSkin(skin);
            skinButton.UpdateState();
            skinButtons.Add(skinButton);
        }
    }

    public void UpdateSkinButtons()
    {
        foreach (var skinButton in skinButtons)
        {
            skinButton.UpdateState();
        }
    }

    public void UpdateMoney()
    {
        moneyText.text = GameManager.Instance.money.ToString();
    }
}
