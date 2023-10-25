using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public GameObject axiePrefab;
    public List<Skill> startingSkills;
    public List<Skill> allSkillsCanLearn;
    public int maxHealth;
    public int speed;
    public bool isAxie;
    //0->3 4->7
    public int curPos;
}
