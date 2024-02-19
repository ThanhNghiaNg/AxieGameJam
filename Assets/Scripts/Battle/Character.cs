using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public GameObject axiePrefab;
    public List<Skill> startingSkills;
    public List<Skill> allSkillsCanLearn;
    public int baseHealth;
    public int scaleHealth;
    public int baseAttack;
    public int scaleAttack;
    public int baseDefend;
    public int scaleDefend;
    public int scaleExperience;
    public int baseExperience;
    public int speed;
    public bool isAxie;
    //0->3 4->7
    public int curPos;
    public int ReturnHealthStat(int level)
    {
        if(level == 0)
        {
            return baseHealth;
        }
        return baseHealth + scaleHealth*level ;
    }
    public int ReturnAttackStat(int level)
    {
        if (level == 0)
        {
            return baseAttack;
        }
        return baseAttack + scaleAttack * level;
    }
    public int ReturnDefendStat(int level)
    {
        if (level == 0)
        {
            return baseDefend;
        }
        return baseDefend + scaleDefend * level;
    }
    public int ReturnExperience(int level)
    {

        return baseExperience + scaleExperience * scaleExperience * level / 4;


       // 100 300 600 1000 
    }
}
