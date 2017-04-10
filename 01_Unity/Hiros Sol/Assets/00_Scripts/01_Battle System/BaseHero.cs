using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//This allows you to generate properties and have them show up in the 'Inspector'. 
public class BaseHero {
    //Hero Stats
    public string name;

    public float baseHP;
    public float currentHP;

    public float baseMP;
    public float currentMP;

    public int stamina;
    public int intellect;
    public int dexterity;
    public int agility;
}
