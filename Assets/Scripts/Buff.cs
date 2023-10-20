using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff 
{
    public enum buffType { AttackBuff, };
    public buffType buff;

    // So turn buff danh cho buff giam dan theo thoi gian hoac so stack neu la buff vinh vien
    public int buffValue;
}
