using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public GameObject characterHealthBar;



    [Header("Buffs")]
    public Buff vunrable;
    public Buff invincible;



    GameManager gameManager;
    BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        gameManager = FindObjectOfType<GameManager>();
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
        currentHealth -= amount;

        Debug.Log(currentHealth.ToString());

        //UpdateHealthBar
    }
    public void UpdateHealUI()
    {

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
}
