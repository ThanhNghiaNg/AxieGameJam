using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterUI : MonoBehaviour
{
    [Range(0, 3)]
    public int position;

    public Character character;

    public GameObject characterParent;


    public List<GameObject> axiePosition = new List<GameObject>();
    public List<GameObject> enemyPosition = new List<GameObject>();

    public List<Skill> skills = new List<Skill>();

    public CharacterStat stat = new CharacterStat();


    BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        

    }
    private void Update()
    {
        MoveToNewPos();
    }

    public void LoadCharaters(Character _character, int _pos)
    {
        
        if (_pos < 0 || _pos > 3) return;


        character = _character;
        skills = _character.startingSkills;
        position = _pos;
        Debug.Log(character.maxHealth);

        if (_character.isAxie)
        {
            GameObject loadC = Instantiate(_character.axiePrefab, characterParent.transform.position, Quaternion.identity);
            loadC.transform.parent = characterParent.transform;
            /*loadC.transform.position = axiePosition[_pos].transform.position;*/
            loadC.transform.rotation = new Quaternion(0f,-180f,0f, 0);
        }
        else
        {
            GameObject loadC = Instantiate(_character.axiePrefab, characterParent.transform.position, Quaternion.identity);
            loadC.transform.parent = characterParent.transform;
            /*loadC.transform.position = enemyPosition[_pos].transform.position;*/

        }
        //Day stat nhan vat vao trong gameManager r load ra 

        stat.maxHealth = character.maxHealth;
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

    public void MoveToNewPos()
    {
        if(character==null)
            return;
        if(character.isAxie )
        {
            while (this.transform.position != axiePosition[position].transform.position)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, axiePosition[position].transform.position, 20f * Time.deltaTime);
            }

        }    
        else
        {
            while (this.transform.position != enemyPosition[position].transform.position)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, enemyPosition[position].transform.position, 20f * Time.deltaTime);
            }
        }
    }


    public void HandleOnClick()
    {
        if (battleManager.currentSkill!=null)
        {
            battleManager.selectedCharacter = this;
            battleManager.UsingSkill();
        }
    }

}
