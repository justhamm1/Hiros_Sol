using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {
    //This will be the states that the battles will go thorough. 
    //Also known as 'BattleStatemachine'
    //Battle States
    public enum PerformAction
    {
        WAIT,           //Idleing while we gather infomation or the player chooses their actions.
        TAKEACTION,     //Player chooses an action to preform. 
        PERFORMACTION   //Pushs the actions from 'TAKEACTION' 

    }
    //Referance for the enum state 'PREFORMACTION'.
    public PerformAction BattleState;

    //This stores the object of 'HandleTurn' class into the object called 'PreformList'.
    public List<TurnSystem> PerformList = new List<TurnSystem>();
    //List for the game objects for the characters in the battle.
    public List<GameObject> HeroesInBattle = new List<GameObject>();
    public List<GameObject> EnemysInBattle = new List<GameObject>();


    
    void Start () {
        BattleState = PerformAction.WAIT;
        //Builds an array with the objects associated with the tags listed and holds that as a referance in the scene. 
        EnemysInBattle.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        HeroesInBattle.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
    }
	
	
	void Update () {
        //The stats that the battle system will run to determine the flow of battle. 
        switch (BattleState)
        {
            case (PerformAction.WAIT):
                //If there is a value with the list created by the 'PreformList' variable, take an action ('TAKEACTION').
                if (PerformList.Count >0)
                {
                    //Will switch the state to 'TAKEACTION'.
                    BattleState = PerformAction.TAKEACTION;

                }
                break;
            case (PerformAction.TAKEACTION):
                //Take the information from the preform list (from the first of the list) and prepare thouse actions to be preformed.
                    //Take the information stored in the variable 'preformer' (which is now a gameObject) and set it equal to the vaule found at the top of the list from our 'PreformList' with referance to the name of the 'Attacker'.
                GameObject performer = GameObject.Find(PerformList[0].Attacker);
                //Anything with the type of Enemy will call the script and methods found in the 'EnemyStateMachine'.
                if (PerformList[0].Type =="Enemy")
                {
                    //Creates the referance for the 'EnemyStateMachine".
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();
                    //Pushes the information back to the 'EnemyStateMachine'.
                    ESM.heroToAttack = PerformList[0].Attackers_Target;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;

                }

                if (PerformList[0].Type == "Hero")
                {


                }

                //Once we are done with getting the info about the actions we preform it.
                BattleState = PerformAction.PERFORMACTION;
                        break;
            case (PerformAction.PERFORMACTION):

                break;


        }
	}

    //Pulls actions from the 'ChooseActions' functions and builds a function for exicuting said 'input's.
    public void CollectActions(TurnSystem input)
    {
        //
        PerformList.Add(input);

    }
}

