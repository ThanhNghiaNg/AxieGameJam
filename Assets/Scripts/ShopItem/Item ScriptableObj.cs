using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    public string itemDescription;
    public float effectAmount;
    public int price;
    public enum Type { Buff, Heal, Shield };
    public Type type;
    public Position posToUse;
    public Position posTarget;

    public string GetItemDescription()
    {
        return this.itemDescription;
    }
    public float GetItemEffectAmount()
    {
        return this.effectAmount;
    }

    // true false cho nhung vi tri cast dc chieu
    public struct Position
    {
        public List<bool> pos;
    }
}