using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{



    [Header("Battle")]
    public int skill;
    public List<CharacterUI> posChar = new List<CharacterUI>();
    public List<Skill> skills = new List<Skill>();

    [Header("Drop")]
    public int gold;
    public int experience;
    public bool isBear;
    public bool isWwolf;
    BattleManager battleManager;
    CharacterStat enemystat;
    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        posChar = battleManager.axieInGameObjects;
    }

    public void TakeTurn()
    {
        if (isWwolf)
        {
            // xu ly sp_Turn Wwolf
            return;
        }
        if (isBear)
        {
            // xu ly sp_Turn Bear
            return;
        }

        /*currentSkill = enemyPatterns.patterns[patternTurn];*/
        /*switch(currentSkill.)*/


    }
    private void AttackPlayer(List<int> pos, int value)
    {
        posChar[RandomInList(pos)].stat.TakeDamage(value);
    }
    private int RandomInList(List<int> list)
    {
        int num = 0;
        num = Random.Range(0, list.Count - 1);
        return list[num];
    }
}
