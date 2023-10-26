using Spine.Unity;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public string axieId;
    public GameObject axiePrefab;
    public SkeletonDataAsset skeletonDataAsset;
    public List<Skill> startingSkills;
    public List<Skill> allSkillsCanLearn;
    public int maxHealth;
    public int speed;   
    public bool isAxie;
    //0->3 4->7
    public int curPos;
}
