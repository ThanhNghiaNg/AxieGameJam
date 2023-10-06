using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : ScriptableObject
{
    public string skillName;
    public SkillDescription skillDescription;
    public SkillEffectAmount effectAmount;
    public bool isUpgrade;
    public List<Buff> buffs = new List<Buff>();

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