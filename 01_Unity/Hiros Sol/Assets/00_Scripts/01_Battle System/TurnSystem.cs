using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//You don't want to inheriate from Monodevelop for this script.
    //Instead you what to be able to referance and populate a list of attacks thus you want it to be 'Serializable".
[System.Serializable]
public class TurnSystem  {
    //Also known as 'HandleTurn'
    //This is a vessel in which the system can push and pull information into other systems.
        //This of this like a notepad of turns and their information within this system.
    //The steps in which the turn system is going to take. 
        //The following is a list of referances that will be used to progress through the system. 
    public string Attacker;                     //Name of attacker.
    public string Type;                         //Denotes weather or not the attacker is a Hero of an Enemy. 
    public GameObject Attackers_GameObject;     //The object attached to the attacker. "Who was doing that attack?"
    public GameObject Attackers_Target;         //Who is the attacker targeting?

    //Which attack is being preformed?

	
    
}
