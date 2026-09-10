using System.Collections.Generic;
using UnityEngine;

public class SkinListManager : MonoBehaviour
{
    [SerializeField] private GameObject skinButtonPrefab;
    [SerializeField] private SkinsListSO skinsListSO;

    private List<BuySkinButton> skinButtons = new List<BuySkinButton>();

    void Start()
    {
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
}
