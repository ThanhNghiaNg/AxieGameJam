using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat 
{
    public Character currentCharacter;
    public int currentHealth;
    public int maxHealth;
    public int attack;
    public int defend;
    public int speed;
    public int pos;
    public int level;
    public int experience;
    public int experienceToLevelUp;

    public void UpdateStat()
    {
        maxHealth = currentCharacter.ReturnHealthStat(level);
        attack = currentCharacter.ReturnAttackStat(level);
        defend = currentCharacter.ReturnDefendStat(level);
        currentHealth = maxHealth;
        experience = 0;
        experienceToLevelUp += currentCharacter.ReturnExperience(level);

    }
}
