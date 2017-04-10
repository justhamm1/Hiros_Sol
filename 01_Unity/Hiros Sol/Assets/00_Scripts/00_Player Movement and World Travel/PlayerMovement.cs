using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    /* 
    *--TO DO LIST--*
    ----------------
    -Get Animator
    -Get Rigidbody
    -Take Input from the player.
    -Send Inputs from the Animation Controller.
    -Update Position on the map. 
    */

    //--Vaiables--
    //Gives a ref to the Rigidbody (RB) attached to the player. 
    Rigidbody2D rbody;
    //Gives ref to the Animator parameters that we have created. 
    Animator anim;

    //Referance to the bool variable that will lock the player movement while a CS or text is being displayed on the screen.
    public bool canMove;


	// Use this for initialization
	void Start () {
        //Give access to the two components that were attached to our player. 
        rbody = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>(); 

	}
	
	// Update is called once per frame
	void Update () {
        //If canMove is false...
        if (!canMove)
        {
            //then return here in this function.
            return;


        }

        //Creates a Vector 2 that calls the inputs Unity gives tot he terms 'Horizontal' and 'Vertical' and creates a ref for you to call it later in the code. 
            //Using 'GetAxisRaw' as opposed to 'GetAxis' because 'Raw' returns a T/F on input command vs allowing for the variables from 0 to 1 e.g.:0,0.1,0.2,0.3,...
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Are we moving?
            //If the 'movement_vector' is not 0 -> then we must be moving. Thus...
        if(movement_vector != Vector2.zero)
        {
            //Enable the parameter that we created for 'isWalking' to equal true and start the animation.
            anim.SetBool("isWalking", true);

            //Supply the 'movement_vector's to the animator.
            anim.SetFloat("input_x", movement_vector.x);
            anim.SetFloat("input_y", movement_vector.y);

        }
        else
        {
            //Otherwise set the parameter to false and remain 'Idle'.
            anim.SetBool("isWalking", false);

        }

        //Apply the movement to the RB (attached to the Player).
            //From where we are + Where we are going * A smooth transition of the frames.
        rbody.MovePosition (rbody.position + movement_vector /** Time.deltaTime*/); //Removed 'Time.deltaTime' to get the player to move with larger steps. (Not really a fix but it worked.)
	}
}
