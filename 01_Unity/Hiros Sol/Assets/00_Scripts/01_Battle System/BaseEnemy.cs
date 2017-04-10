using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//This allows you to generate properties and have them show up in the 'Inspector'. 
public class BaseEnemy {
    //Enemy Stats
    public string name;

    public float baseHP;
    public float currentHP;

    public float baseMP;
    public float currentMP;

    public float baseATK;
    public float currentATK;

    public float baseDEF;
    public float currentDEF;

}
