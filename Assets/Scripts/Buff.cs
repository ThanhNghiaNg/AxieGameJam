using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Buff 
{
    public enum buffType { AttackBuff, DefBuff, Invincible };
    public buffType buff;

    // So turn buff danh cho buff giam dan theo thoi gian hoac so stack neu la buff vinh vien
    public int buffValue;
}
