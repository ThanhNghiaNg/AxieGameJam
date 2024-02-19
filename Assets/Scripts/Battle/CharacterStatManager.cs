using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatManager : MonoBehaviour
{
    
    [Header("CharacterStats")]
    public CharacterStat characterStat;



    [Header("Buffs")]
    public Buff vunrable;
    public Buff invincible;


    public HealthBarUI characterHealthBar;
    GameManager gameManager;
    BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        gameManager = FindObjectOfType<GameManager>();
        characterHealthBar.healthBar.maxValue = characterStat.maxHealth;
    }
    void Update()
    {
        
    }
    public void TakeDamage(int amount)
    {
        if(invincible.buffValue != 0)
        {
            amount = 0;
        }    
        characterStat.currentHealth -= amount;

        Debug.Log(characterStat.currentHealth.ToString());

        //UpdateHealthBar
        UpdateHealUI();
    }
    public void UpdateHealUI()
    {
        characterHealthBar.DisplayHealth(characterStat.currentHealth);
    }

    public void AddBuff(Buff.buffType type, int amount)
    {
        if( type == Buff.buffType.AttackBuff)
        {

        }
        else if(type == Buff.buffType.Invincible)
        {
            if(invincible.buffValue <=0)
            {
                
            }
            invincible.buffValue += amount;
            //DisplayBuff
        }
    }
    public void EvaluateBuff()
    {

    }

    // Xoa all buff
    public void ResetBuff()
    {

    }

    public void LevelUp()
    {
        if(characterStat.experience >= characterStat.experienceToLevelUp)
        {
            int spareExperience = characterStat.experience-characterStat.experienceToLevelUp;
            characterStat.level += 1;
            characterStat.UpdateStat();
            characterStat.experience += spareExperience; 
        }
        return;
    }
}
