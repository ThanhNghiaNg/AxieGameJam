using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public Sprite sprite;
    public string skillName;
    public SkillDescription skillDescription;
    public SkillEffectAmount effectAmount; // C�i n�y tr? ra dame || Heal || Shield n?u d�ng t?i 
    public int lv; // Kh�ng c?n thi?t 
    public int coolDown;
    public bool isUpgrade;
    public enum Type {Buff, Debuff, Attack, Shield, Heal, Dot, Support, Passive};
    public enum Range { Single, AOE };
    public Type type;
    public Range range;
    public Position posToUse;
    public Position posTarget;

    public List<Buff> buff; // C�i n�y l� ch?a List nh?ng Buff m� m�nh c?n Buff cho nh�n v?t 

    public string GetSkilDescription()
    {
        if(!isUpgrade)
        {
            return this.skillDescription.nonUpgrade;
        }
        else { return this.skillDescription.upgrade; }
    }
    public int GetSkillEffectAmount()
    {
        if (!isUpgrade) { return this.effectAmount.nonUpgrade; }
        else { return this.effectAmount.upgrade;}
    }

    
}


[System.Serializable]
public struct SkillDescription
{
    public string nonUpgrade;
    public string upgrade;
}
[System.Serializable]

//Luong dmg gay ra || Heal || Shield neu can
public struct SkillEffectAmount
{
    public int nonUpgrade;
    public int upgrade;
}
[System.Serializable]

// true false cho nhung vi tri cast dc chieu
public struct Position
{
    public List<bool> pos ;
}