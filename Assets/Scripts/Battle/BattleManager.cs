using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{ 

    [Header("Turn")]
    public List<CharacterUI> turnList = new List<CharacterUI>(); //turn 
    public List<CharacterUI> pendingList = new List<CharacterUI>();
    public CharacterUI currentCharacter;
    public Skill currentSkill;
    public CharacterUI selectedCharacter;


    [Header("InBattle")]    
    public List<Character> axieInGame = new List<Character>(); 
    public List<Character> enemyInGame = new List<Character>();

    public List<CharacterUI> axieInGameObjects = new List<CharacterUI>();
    public List<CharacterUI> enemyInGameObjects = new List<CharacterUI>();


    [Header("Skill")]
    public List<SkillUI> skillUI_InGameObjects = new List<SkillUI>();
    SkillAction skillAction;


    GameManager gameManager;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        skillAction = GetComponent<SkillAction>();
    }
    private void Start()
    {
        BeginBattle();
    }
    public void UseSkill()
    {

    }

    bool CheckSkill(int skillOrder)
    {
        if (currentCharacter == null) return false;
        List<bool> pos = currentCharacter.skills[skillOrder].posToUse.pos;
        return pos[currentCharacter.position] == true;
    }
    public void BeginBattle()
    {
        for (int i = 0; i < axieInGame.Count; i++)
        {
            CharacterUI characterUI = axieInGameObjects[i];
            turnList.Add(characterUI);
            characterUI.LoadCharaters(axieInGame[i], i);
        }
        for (int i = 0; i < enemyInGame.Count; i++)
        {
            CharacterUI characterUI = enemyInGameObjects[i];
            turnList.Add(characterUI);
            characterUI.LoadCharaters(enemyInGame[i], i);
        }
        turnList = turnList.OrderByDescending(x => x.character.speed).ToList();
        ChangeTurn();
    } 
    public void ChangeTurn()
    {
        Debug.Log(turnList.Count);
        if (turnList.Count == 0)
        {
            turnList = pendingList.ToList();
            pendingList.Clear();
        }
        selectedCharacter = null;
        currentSkill = null;
        currentCharacter = turnList[0];
        pendingList.Add(currentCharacter);
        turnList.Remove(turnList[0]);
        if (currentCharacter.character.isAxie)
        {
            SkillUpdate();
        }
        else
        {
            // Xu ly enemy
            HandleEnemyTurn();
        }
    }
    public void HandleEnemyTurn()
    {

    }
    public void SkillUpdate()
    {
        for(int i = 0; i < skillUI_InGameObjects.Count; i++)
        {
            Debug.Log("Skill " + i);
            skillUI_InGameObjects[i].LoadSkill(currentCharacter.skills[i]);
        }
    }
    
 

    public void UsingSkill()
    {
        /*skillAction.PerformSkill(currentSkill, selectedCharacter.stat);*/
        ChangeTurn();


    }
    public void ChangePosition(ref List<CharacterUI> charactersUI, int curentPos, int desPos)
    {
      
        if (Mathf.Abs(curentPos - desPos) > 1)
        {
            charactersUI[curentPos].position = desPos;
            charactersUI[desPos].position = curentPos;
        }
        else
        {
            if (curentPos - desPos > 0)
            {
                for (int i = desPos; i < curentPos; i++)
                {
                    charactersUI[i].position = charactersUI[i + 1].character.curPos;
                }
                charactersUI[curentPos].position = desPos;
            }
            else
            {
                for (int i = desPos; i < curentPos; i++)
                {
                    charactersUI[i].position = charactersUI[i - 1].character.curPos;
                }
                charactersUI[curentPos].position = desPos;
            }
        }
        charactersUI = charactersUI.OrderBy(x => x.character.curPos).ToList();
    }


}
