using UnityEngine;

[System.Serializable]
public class Skin
{
    public string skinID;
    public string skinName;
    public Texture2D skinLineTexture;
    public Sprite skinDotSprite;
    public int price;
}

[CreateAssetMenu(fileName = "SkinsListSO", menuName = "Scriptable Objects/Skin List")]
public class SkinsListSO : ScriptableObject
{
    public Skin[] skins;
}
