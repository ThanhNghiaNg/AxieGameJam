using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour
{
    Skill skill;
    public CharacterStat currentCharacter;
    public CharacterStat target;
    BattleManager battleManager;


    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }
    

    public void PerformSkill(Skill _skill, CharacterStat _target)
    {
        skill = _skill;
        target = _target;
        //SwitchCase till ded
        switch (skill.name)
        {

            case "shield_single":
                Debug.Log("Dungskill");
                return;
            default: break;
        }
    }

    private void AttackTarget()
    {
        //Dame base tinh toan
        int totalDamage = skill.GetSkillEffectAmount();
        if(true)
        { }
        target.TakeDamage(totalDamage);
    }
    private void ApplyBuffToSelf(Buff.buffType type)
    {
        currentCharacter.AddBuff(type, skill.GetSkillEffectAmount());
    }
    private void AppleyBuffToTarget(Buff.buffType type)
    {
        target.AddBuff(type, skill.GetSkillEffectAmount());
    }

}
