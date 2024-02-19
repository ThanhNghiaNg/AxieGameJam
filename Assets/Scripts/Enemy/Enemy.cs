using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    
    
    [Header("Battle")]
    public int numberskill;
    public List<CharacterUI> posChar = new List<CharacterUI>();
    public List<CharacterUI> posEnemy = new List<CharacterUI>();
    public List<Skill> skills = new List<Skill>();

    [Header("Drop")]
    public int gold;
    public int experience;
    

    [Header("Wwolf")]
    public bool isWwolf;
    public int countWolf = 0;
    BattleManager battleManager;
    CharacterStat enemystat;

    [Header("Bear")]
    public bool isBear;
    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        posChar = battleManager.axieInGameObjects;
    }

    public void TakeTurn()
    {
        if(isWwolf && countWolf == 0)
        {
            // xu ly sp_Turn Wwolf

            return;
        }
        if(isBear)
        {
            if(enemystat.currentHealth<= enemystat.maxHealth/2 || true)
            {
                // Neu BearMom die or 1/2 mau Rage buff buff atk, spd, Add skill heavy hit aoe;
            }
            return;
        }
        UsingSkill(skills[numberskill]);
        
        
        /*switch(currentSkill.)*/
        

    }
    private void UsingSkill(Skill skill)
    {
        switch(skill.name) {
            case "a":return;

            default: return;
        }
    }
    private void SpawnWolfs()
    {
        // move ve vi tri 4 roi spawn 3 con ra dang trc
    }
    private void AttackSinglePlayer(List<int> pos, int value)
    {
        posChar[RandomInList(pos)].stat.TakeDamage(value);
    }
    private void AttackMutiPlayer(List<int> pos, int value)
    {
        foreach(int i in pos)
        {
            posChar[i].stat.TakeDamage(value);
        }
    }
    private void ApplyBuffSinglePlayer(List<int> pos, Buff.buffType type,int value)
    {
        posChar[RandomInList(pos)].stat.TakeDamage(value);
    }
    private void ApplyBuffMutiPlayer(List<int> pos, Buff.buffType type,int value)
    {
        foreach (int i in pos)
        {
            posChar[i].stat.AddBuff(type, value);
        }
    }
    private void ApplyBuffSelf(Buff.buffType type, int value)
    {
        /*enemystat.AddBuff(type, value);*/
    }
    private void ApplyBuffEnemyTeam(List<int> pos, Buff.buffType type, int value)
    {
       posEnemy[RandomInList(pos)].stat.AddBuff(type, value);
    }
    bool CheckInRangeSkill(List<int> pos)
    {
        foreach (int i in pos)
        {
            if (posChar[i] == null)
                return false;
        }
        return true;
    }
    private int RandomInList(List<int> list)
    {
        int num = 0;
        num = Random.Range(0, list.Count-1);
        return list[num];
    }
}
