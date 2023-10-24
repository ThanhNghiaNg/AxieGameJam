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
    public TMP_Text skillText;
    public TMP_Text skillType;
    public TMP_Text skillDescription;
    public Image skillImg;
    public GameObject description;


    CharacterUI positionInBattle;




    private void Awake()
    {
        positionInBattle = FindObjectOfType<CharacterUI>();
    }
    public void LoadSkill(Skill _skill)
    {
        skill = _skill;
        skillText.text = skill.GetSkilDescription();
        skillImg.sprite = skill.sprite;
        
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
        if (skillType != null)
        {
            Debug.Log("Null");
        }
        else
        Debug.Log(this.skill.GetSkilDescription()); 
    }
    //Click vao skill khac
    public void HandleDeSelected()
    {
        Debug.Log("Se can trong tuong lai");
    }


}
