using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PositionInBattle : MonoBehaviour
{
    [Range(1, 4)]
    public int position;

    public Character character;

    public GameObject axieParent;
    public GameObject enemyParent;
    


    public List<GameObject> axiePosition = new List<GameObject>();
    public List<GameObject> enemyPosition = new List<GameObject>();


    public List<GameObject> inUseAxiePosition = new List<GameObject>();
    public List<GameObject> inUseEnemyPosition = new List<GameObject>();


    BattleManager battleManager;


    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }
    public void LoadCharaters(Character _character, int _pos)
    {
        
        if (_pos < 0 || _pos > 3) return;


       /* character = _character;*/

            if (!_character.isAxie)
            {
                GameObject loadC = Instantiate(_character.axiePrefab, axieParent.transform.position, Quaternion.identity);
                loadC.transform.parent = axieParent.transform;
                loadC.transform.position = axiePosition[_pos].transform.position;
                loadC.transform.rotation = new Quaternion(0f,-180f,0f, 0);
                inUseAxiePosition.Add(axiePosition[_pos]);
                /*loadC.GetComponent<Character>().curPos = _pos;*/


            }
            else
            {
                GameObject loadC = Instantiate(_character.axiePrefab, enemyParent.transform.position, Quaternion.identity);
                loadC.transform.parent = enemyParent.transform;
                loadC.transform.position = enemyPosition[_pos].transform.position;
                inUseEnemyPosition.Add(enemyPosition[_pos]);
                /*loadC.GetComponent<Character>().curPos = _pos;*/

            }

        
    }
    public bool AvailableToAdd(Character _character)
    {
        if(_character.isAxie)
        {
            if (axiePosition != null) return true;
        }
        else
        {
            if(enemyPosition != null) return true;
        }
        Debug.Log("Chac la full slot");
        return false;
    }

    public void ChangePosition( List<Character> characters,int curentPos, int desPos )
    {
       if(Mathf.Abs(curentPos - desPos) > 1)
       {
            characters[curentPos].curPos = desPos;
            characters[desPos].curPos = curentPos;
       }
       else
       {
           if(curentPos - desPos > 0)
            {
                for (int i = desPos; i < curentPos; i ++)
                {
                    characters[i].curPos = characters[i+1].curPos;
                }
                characters[curentPos].curPos = desPos;
            }
           else
            {
                for (int i = desPos ; i < curentPos; i++)
                {
                    characters[i].curPos = characters[i -1].curPos;
                }
                characters[curentPos].curPos = desPos;
            }
       }
       characters =  characters.OrderBy(x => x.curPos).ToList();
    }
   
}
