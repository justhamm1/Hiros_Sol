using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Gives access to the UI elements within Unity.
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    //Referance to the Text Box GameObject.
    public GameObject textBox;
    //Referance to the text with in the Text Box.
    public Text theText;

    //Referance to the line that we are on with in the dialouge script.
    public int currentLine;
    //Referance to the line where the script will stop reading text.
    public int endAtLine;

    //**Pulled from "TextImporter.cs" Script.**//
    //Referance to the file that we are imporing into the game.
    public TextAsset textFile;
    //An array for the lines of text that will come into the code as a 'string'. 
    public string[] textLines;
    //**_____**//


    //Referance to the player that will be used to disable the player movement. (The Referance it pulled from the script of the same name you yourself created.)
    public PlayerMovement player;

    //Referance to the bool variable that will turn on or off the functionality of parts of the code if input is not needed. 
    public bool isActive;
    //Referance to the bool variable that will stop the player from moving while text is on the screen.
    public bool stopPlayerMovement;

    //Referance to the bool that will ask if the text is typing across the screen.
    private bool isTyping = false;
    //Referance to the bool that will allow for the intreupting of the text typing across the screen.
    private bool cancelTyping = false;
    //Referance to the speed at which the text will be typed across the screen.
    public float typeSpeed;


    void Start()
    {
        //Links the player variable to the referance to the PlayerMovement script and the objects attached to it.
        player = FindObjectOfType<PlayerMovement>();

        //Checks to see if the text file is there.
        if (textFile != null)
        {
            //Getting the textLines array and put in the array the lines (text) from the textFile and split it into seperate pieces via the notation of '\n' (The way the system reconizes that a new line has been inserted).
            textLines = (textFile.text.Split('\n'));

        }

        //If the endAtLine is equal to 0 then...
        if(endAtLine == 0)
        {
            //...then the endAtLine is equal to the textLines length within the array (total line minus 1 as it is an index line).
            endAtLine = textLines.Length - 1;

        }

        //If isActive is true...
        if (isActive)
        {
            //call the EnableTextBox function.
            EnableTextBox();

        }
        else
        {
            //Call the DisableTextBox function.
            DisableTextBox();

        }

    }

    void Update()
    {
        //If the code(bool) is not active then return to the top of the update (here).
        if (!isActive)
        {
            return;

        }

        //Access the dialouge_script object (via textLines) and input the value of the array as the current line and move on.
        //theText.text = textLines[currentLine]; //REMOVED TO ALLOW FOR TEXT SCROLLING AS THEPLAYER IS TALKING.

        //Allows the Player to advance the text line.
            //Possibly change this to match the input commant.
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //If the text isn't scrolling across already...
            if (!isTyping)
            {
                //...then add one to the current line....
                currentLine += 1; //If the button above is pressed change the value and thus the text box line to the next one.

                //...and disable the text box if it goes too far...
                if (currentLine > endAtLine) //Turns off the text box once we reach the desired line.
                {
                    DisableTextBox();

                }
                //...otherwise...
                else
                {
                    //...start the Corouting (TextScroll)...
                        //...Call the the referance to the text lines...
                        //...build an array based on the need from the text...
                        //...fill the array with the lines that we have assigned to this section.
                    StartCoroutine(TextScroll(textLines[currentLine]));

                }
            }
            //...else if the text is typing and you already haven't cancled typeing...
            else if (isTyping && !cancelTyping)
            {
                //...Cancel the text typing.
                cancelTyping = true;
            }
        }
    }

    //The following is a Corouting that will allow for the text to be typed out one letter at a time.
        //Will take a line of text that is pulled from the Dialouge script.
    private IEnumerator TextScroll (string lineOfText) //This will work in it's own timeline of the script and keep the rest of the code working in tandiem.
    {
        //Start with nothing and add one letter at a time to the string of text, until the line of text is complete.

        //Referance to the letter we are on in the string.
        int letter = 0;
        //The text to be displayed.
        theText.text = ""; //By default display nothing.
        //Set the bool istyping to true so the sequence can begin...
        isTyping = true;
        //...and set the cancelTyping to false to allow for the typing to begin.
        cancelTyping = false;

        //White isTyping is true and cancelTypeing is false and the letter value is less than the number assigned to the line of text value minus 1...
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) //While the statement is true run this loop.
        {
            //...look at the number assigned to the letter in the full text line...
            theText.text += lineOfText[letter];
            //...add another letter..
            letter += 1;
            //...hold for X seconds, assigned by the type speed variable.
            yield return new WaitForSeconds(typeSpeed);

        }
        //If the Player cancels the typing text then just print the whole line of text.
        theText.text = lineOfText;
        //Once that has been called, reset the bools to default.
        isTyping = false;
        cancelTyping = false;
    }

    //Function that turns on the text box if called to do so.
    public void EnableTextBox()
    {
        //Line that turns on the text box.
        textBox.SetActive(true);
        //Turns the bool to true when the trigger is fired
        isActive = true;

        //This stops the player while the text box is active.
        if (stopPlayerMovement)
        {
            player.canMove = false;
            
        }

        //Start the Corouting (TextScroll)...
        //...Call the the referance to the text lines...
        //...build an array based on the need from the text...
        //...fill the array with the lines that we have assigned to this section.
        StartCoroutine(TextScroll(textLines[currentLine]));

    }

    //Function that turns off the text box if called to do so.
    public void DisableTextBox()
    {
        //Line that turns off the text box.
        textBox.SetActive(false);
        //Turns the bool to false when the trigger is fired
        isActive = false;

        //This makes sure that the player can move as long as the text box is not active.
        player.canMove = true;

    }

    //This function give the abality to reload a new line of text with a new text file.
        //Must pass in a new text file from somewhere else.
    public void ReloadScript(TextAsset theText)
    {
        //If the Text is not equal to nothing...
        if (theText != null)
        {
            //...textlines (the dialouge lines or the text array we auto created) is equal to a new string array and remove the old text files with a new script.
            textLines = new string[1];

            //Copy of text from above...
                //Getting the textLines array and put in the array the lines (text) from the new theText file and spliting it into seperate pieces via the notation of '\n' (The way the system reconizes that a new line has been inserted).
            textLines = (theText.text.Split('\n'));

        }
    }
}

