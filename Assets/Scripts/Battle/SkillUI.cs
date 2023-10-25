using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    public Skill skill;

    [Header("SkillUI")]
    public TMP_Text skillName;
    public TMP_Text skillType;
    public TMP_Text skillDescription;
    public TMP_Text skillTarget;
    public Image skillImg;
    public GameObject description;


    CharacterUI positionInBattle;
    BattleManager battleManager;




    private void Awake()
    {
        positionInBattle = FindObjectOfType<CharacterUI>();
        battleManager = FindObjectOfType<BattleManager>();
    }
    public void LoadSkill(Skill _skill)
    {
        skill = _skill;
        skillName.text = skill.skillName;
        skillImg.sprite = skill.sprite;
        skillDescription.text = skill.GetSkilDescription();
        
        Debug.Log("Skill Load");

    }
    public void HandleHover()
    {
        description.SetActive(true);
        Debug.Log("=]]");
    } 
    public void HandleHoverEnd()
    {
        description.SetActive(false);
        Debug.Log("=]]");
    }    
    //Click vao skill 
    public void HandleClick()
    {
        if (skillType == null)
        {
            Debug.Log("Null");
        }
        else
        Debug.Log(this.skill.GetSkilDescription());
        battleManager.currentSkill = this.skill;
        //Xu ly target o day
    }
    //Click vao skill khac
    public void HandleDeSelected()
    {
        Debug.Log("Se can trong tuong lai");
    }


}
