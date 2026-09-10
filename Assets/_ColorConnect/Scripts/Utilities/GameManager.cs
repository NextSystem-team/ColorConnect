using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int money;

    public bool usingSkin;
    public Skin currentSkin;
    public List<Skin> collectedSkins = new List<Skin>();

    public bool canWatchDailyAd = true;
    public int resetCountToAd = 0;

    [SerializeField] private SkinsListSO skinsListSO;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void EquipSkin(Skin skin)
    {
        if (skin == null) 
        {
            usingSkin = false;
        }
        else
        {
            currentSkin = skin;
            usingSkin = true;
        }
    }

    public void AddSkin(Skin skin)
    {
        if (!collectedSkins.Contains(skin))
        {
            collectedSkins.Add(skin);
            EquipSkin(skin);
        }
        else
        {
            return;
        }
    }

    public bool HasSkinCheck(Skin skin)
    {
        return collectedSkins.Contains(skin);
    }
}
