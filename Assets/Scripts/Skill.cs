using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public Sprite sprite;
    public string skillName;
    public SkillDescription skillDescription;
    public SkillEffectAmount effectAmount;
    public int lv;
    public int coolDown;
    public bool isUpgrade;
    public List<Buff> buffs = new List<Buff>();
    public enum Type {Buff, Debuff, Attack, Shield, Heal, Dot, Support, Passive};
    public enum Range { Single, AOE };
    public Type type;
    public Range range;
    public Position posToUse;
    public Position posTarget;

    public string GetSkilDescription()
    {
        if(!isUpgrade)
        {
            return this.skillDescription.nonUpgrade;
        }
        else { return this.skillDescription.upgrade; }
    }
    public float GetSkillEffectAmount()
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
    public float nonUpgrade;
    public float upgrade;
}
[System.Serializable]

// true false cho nhung vi tri cast dc chieu
public struct Position
{
    public List<bool> pos ;
}