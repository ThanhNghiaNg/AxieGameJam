using UnityEngine;

public class Asset : ScriptableObject
{
    public enum AssetType { HP, Alixir, Buff_ATK, Buff_DEF }
    public AssetType asset;
    public int quatity;
}
