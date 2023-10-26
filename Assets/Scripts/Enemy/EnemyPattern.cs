using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPattern : ScriptableObject
{
    public List<Pattern> patterns = new List<Pattern>();
    public List<Pattern> sp_Patterns = new List<Pattern>();
}
[System.Serializable]
public class Pattern
{
    public enum Type { }
    public Type type;
    public int amount;
    public List<Buff> buffs = new List<Buff>();
    public Position posTarget;
    public int chance;
    public Position posToUse;
}