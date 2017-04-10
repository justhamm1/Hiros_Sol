using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Give access to the Unity UI elements.
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour {
    //Notes all the states that the Heros can be in and act.

    //Referance the the script for 'BaseHero'.
    public BaseHero hero;

    //Enums for all the Heroes states for the battle system.
    public enum TurnState
    {
        PROCESSING, //The time in which one waits for the progress bar to fill and the Player can use an action.
        ADDTOLIST,  //Adds the Character to a list that denotes turn order. 
        WAITING,    //Idle state
        SELECTION,  //Players chooses an action to take place.
        ACTION,     //Character does it's action(s).
        DEAD        //Party has been defeated. 

    }

    //Referance for the state system that is created above.
    public TurnState currentState;

    //Referance for the 'Progress Bar' or cool down, in terms of seconds of world time.
    private float currentCooldown = 0f;//secs
    private float maxCooldown = 5f;//secs

    //Referance for the 'Progress Bar' image in the UI.
    public Image ProgressBar;

	void Start () {
        //Tells the engine what the starting state is to begin with.
        currentState = TurnState.PROCESSING;

	}
	
	
	void Update () {
        //Show me what the current state is.
        Debug.Log(currentState);

        //The state machine for the game and the commands that are going to be preformed. 
        switch (currentState)
        {
            
            case (TurnState.PROCESSING):
                //Starts the function for the battle timer.
                ProgressBarAdvance();

                break;
            case (TurnState.ADDTOLIST):


                break;
            case (TurnState.WAITING):


                break;
            case (TurnState.SELECTION):


                break;
            case (TurnState.ACTION):


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

        //Calculating the ammount of the current cooldown and the maxium cooldown.
        float calcCooldown = currentCooldown / maxCooldown;

        //The technique that advances the image of the 'Progress Bar'.
            //Clamp - locks a range of values between two values; a Min and a Max value.
            //The 'Y' value below makes sure that you don't effect the 'Y value' for the scale. 
        ProgressBar.transform.localScale = new Vector2(Mathf.Clamp(calcCooldown, 0, 1), ProgressBar.transform.localScale.y);

        //if the 'currentCooldown' becomes greater than the 'maxCooldown' value then switch the state to 'ADDTOLIST'.
        if(currentCooldown >= maxCooldown)
        {
            //Changes the state to 'ADDTOLIST'.
            currentState = TurnState.ADDTOLIST;
        }

    }
}
