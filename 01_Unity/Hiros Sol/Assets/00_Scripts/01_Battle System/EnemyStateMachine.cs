using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour {

    //Referance for the BattleManager script.
    private BattleManager BSM;

    //Referance the the script for 'BaseHero'.
    public BaseEnemy enemy;

    //Enums for all the Enemy's states for the battle system.
    public enum TurnState
    {
        PROCESSING,     //The time in which one waits for the progress bar to fill and the Player can use an action.
        CHOOSEACTION,   //Lets the enemy choose it's own action.
        WAITING,        //Idle state.
        ACTION,         //Character does it's action(s).
        DEAD            //Party has been defeated. 

    }

    //Referance for the state system that is created above.
    public TurnState currentState;

    //Referance for the 'Progress Bar' or cool down, in terms of seconds of world time.
    private float currentCooldown = 0f;
    private float maxCooldown = 5f; //Adjust this if the Enemy is hitting too fast. 

    //Referance to THIS GameObject
    private Vector2 startPosition;

    //Variables for 'TimeForAction' function.
    private bool actionStarted = false;
    //Variable for storing the information of the 'Heros' postion.
    public GameObject heroToAttack;
    //The speed of the animation that is being preformed.
    private float animSpeed = 5f;

    void Start()
    {
        //Tells the engine what the starting state is to begin with.
        currentState = TurnState.PROCESSING;
        //Builds the bridge from the 'Battle Manager' GameObject to the script 'BattleManager'.
        BSM = GameObject.Find("Battle Manager").GetComponent<BattleManager>();

        //Helps to orientate any future animations you may add to THIS. 
        startPosition = transform.position;
    }


    void Update()
    {
        //Show me what the current state is.
        Debug.Log(currentState);

        //The state machine for the game and the commands that are going to be preformed. 
        switch (currentState)
        {

            case (TurnState.PROCESSING):
                //Starts the function for the battle timer.
                ProgressBarAdvance();

                break;
            case (TurnState.CHOOSEACTION):
                //Calls the function 'ChooseAction'.
                ChooseAction();
                //Progress to the next state once the function has completed. 
                currentState = TurnState.WAITING;

                break;
            case (TurnState.WAITING):
                //Idle State.

                break;
            case (TurnState.ACTION):
                //Once this state is called it will run the function 'TimeForAction'.
                StartCoroutine(TimeForAction());

                break;
            case (TurnState.DEAD):


                break;
        }

    }

    //Function that takes the progress bar to the full width, thus giving the Player the notification that they can take an action. 
    void ProgressBarAdvance()
    {
        //The 'currentCooldown' is added to the real world time.
        currentCooldown = currentCooldown + Time.deltaTime;

        //if the 'currentCooldown' becomes greater than the 'maxCooldown' value then switch the state to 'ADDTOLIST'.
        if (currentCooldown >= maxCooldown)
        {
            //Changes the state to 'ADDTOLIST'.
            currentState = TurnState.CHOOSEACTION;
        }

    }

    //This will allow for the enemy to choose actions automaticly.
    void ChooseAction()
    {
        //Creates a new instance of the process of 'TurnSystem' that allows for the Enemy to make an attack from a list to follow.
        TurnSystem myAttack = new TurnSystem();

        //The name of the Enemy attacking.
        myAttack.Attacker = enemy.name;
        //Gives THIS object the vaule of an Enemy characer.
        myAttack.Type = "Enemy";
        //The GameObject attached to the Enemy.
        myAttack.Attackers_GameObject = this.gameObject;
        //Randomly target a hero.
            //Finds a random hero in the list based on the maximum number in the built list.
        myAttack.Attackers_Target = BSM.HeroesInBattle[Random.Range(0, BSM.HeroesInBattle.Count)];

        //Pushes the information to the 'BattleManager' script after it is colelcted. 
        BSM.CollectActions(myAttack);

    }

    //This will allow the abality to preform a timed action or function.
        //If "Code does not return..." then that means that you do not have a 'yield' statement in the 'IEnumerator' funtion.
    private IEnumerator TimeForAction()
    {
        //Checks to see if the action has been started. 
            //If the action has started then we stop the action...
        if (actionStarted)
        {
            yield break;

        }

        //...if that is not the case then we set the 'actionStarted' to true.
        actionStarted = true;   //And move forward with the code.

        //Animate the enemy near the Hero to attack.
        //Pulls from the 'TurnSystem' and pushes to the 'BattleManager'.
        Vector2 heroPostion = new Vector2 (heroToAttack.transform.position.x + 1.5f, heroToAttack.transform.position.y);
        //While we are waiting for the hero's postion
            while (MoveTowardsHero(heroPostion))
            {
                //Don't do anything.
                yield return null;

            }

        //Wait for some time.
        yield return new WaitForSeconds(0.5f);

        //Do damage.

        //Animate back to start position.
        Vector2 firstPositon = startPosition;
        while (MoveTowardsStart(firstPositon))
        {
            //Don't do anything.
            yield return null;

        }

        //Remove this action from the list (BSM).
        BSM.PerformList.RemoveAt(0);

        //Reset the BSM, and set to the 'WAIT' state again.
        BSM.BattleState = BattleManager.PerformAction.WAIT; //End Coroutine.

        //Reset the 'actionStarted' back to the top of the process.
        actionStarted = false;

        //Reset THIS enemy state to the begining.
        currentCooldown = 0f;
        currentState = TurnState.PROCESSING;   

    }
    //The direction in which the Enemy will aim its atacking movement towards.
        //Need to be a Vector3... for some reason. I think it is because it calls three values. 
    private bool MoveTowardsHero(Vector3 target)
    {
        //"Return if the variable 'target' (that is not equal to the position of the 'MoveTowards', aim at you target, animate at this speed)."
        return target != (transform.position = Vector3.MoveTowards (transform.position, target, animSpeed * Time.deltaTime));

    }
    //The direction in which this Enemy will return to once the action has been caried out. 
    private bool MoveTowardsStart(Vector3 target)
    {
        //"Return if the variable 'target' (that is not equal to the position of the 'MoveTowards', aim at you target, animate at this speed)."
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));

    }

}
