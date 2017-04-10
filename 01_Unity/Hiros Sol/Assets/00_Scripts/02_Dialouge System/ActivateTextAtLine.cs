using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {
    //This is used to activate text at certain points in the dialouge script.


    //Referance to the text script that you will need to be refering to for dialouge.
    public TextAsset theText;

    //Referance to the starting line you are going to be reading from.
    public int startLine;

    //Referance to the ending line that you will be ending on. 
    public int endLine;

    //Referance to the text manager that you are pulling from.
    public TextBoxManager theTextBox;

    //Referance to the bool that is going to trigger the Text Manager to destroy the text box once completed. 
    public bool destroyWhenActivated;

    //Referances used to ativate text from an NPC.
    public bool requireButtonPress;
    private bool waitForPress;

	void Start () {
        //Links the textBoxManader in the scene.
        theTextBox = FindObjectOfType<TextBoxManager>();
        	
	}
	
	// Update is called once per frame
	void Update () {
        //If waitForPress is true and we press a button...
        if (waitForPress && Input.GetKeyDown(KeyCode.RightShift))
        {
            //...then run the following...
                //...take the Text Box Manager and pass in the Text document that you want it to be read from...
            theTextBox.ReloadScript(theText);
                //...then start from the assigned line...
            theTextBox.currentLine = startLine;
                //...and assign an ending line here...
            theTextBox.endAtLine = endLine;
                //Turns the Text Box on once the if statement is called.
            theTextBox.EnableTextBox();

                //If dertroyWhenActivated is true...
            if (destroyWhenActivated)
            {
                //...then destroy the gameObject.
                Destroy(gameObject);

            }

    }
	}

    //Function that calls the dialouge box once the player walks into a scene. 
    void OnTriggerEnter2D (Collider2D other)
    {
        //If the object with the Collider2D is linked to a GameObject that is tagged with the name "Player" then...
            //Make sure that you are calling the string element below what you are calling your main player. 
        if(other.name == "Player")
        {
            //If requireButtonPress is true...
            if (requireButtonPress)
            {
                //...then don't run the rest of the script.
                waitForPress = true;
                return;
            }


            //...take the Text Box Manager and pass in the Text document that you want it to be read from...
            theTextBox.ReloadScript(theText);
            //...then start from the assigned line...
            theTextBox.currentLine = startLine;
            //...and assign an ending line here...
            theTextBox.endAtLine = endLine;
            //Turns the Text Box on once the if statement is called.
            theTextBox.EnableTextBox();

        }

        //If dertroyWhenActivated is true...
        if (destroyWhenActivated)
        {
            //...then destroy the gameObject.
            Destroy(gameObject);


        }

    }

    //Function taht is called when the player it leaving the trigger.
    void OnTriggerExit2D (Collider2D other)
    {
        //If the player leave the area...
        if(other.name == "Player")
        {
            //... they can't activate the text box once they leave.
            waitForPress = false;
        }

    }

}
