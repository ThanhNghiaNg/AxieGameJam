using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{ 

    [Header("Turn")]
    public List<GameObject> turnList = new List<GameObject>();
    public Character currentCharacter;
    public Skill currentSkill;


    [Header("InBattle")]    
    public List<Character> axieInGame = new List<Character>(); 
    public List<Character> enemyInGame = new List<Character>();
    PositionInBattle positionInBattle;




    private void Awake()
    {
        positionInBattle = FindObjectOfType<PositionInBattle>();
    }
    private void Start()
    {
        LoadOnStart();
    }
    public void UseSkill()
    {

    }

    bool CheckSkill()
    {
        if (currentCharacter == null) return false;
       
        return true;
    }
    public void LoadOnStart()
    {
        for (int i = 0; i < axieInGame.Count; i++)
        {
            positionInBattle.LoadCharaters(axieInGame[i], i);
        }
        for (int i = 0; i < enemyInGame.Count; i++)
        {
            positionInBattle.LoadCharaters(enemyInGame[i], i);
        }

    }    


}
