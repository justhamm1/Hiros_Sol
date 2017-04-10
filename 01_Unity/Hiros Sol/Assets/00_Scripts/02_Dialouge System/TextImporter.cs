using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextImporter : MonoBehaviour {
    //This is designed to import the script (in terms of words to be said) into the text box prefab.

    //Referance to the file that we are imporing into the game.
    public TextAsset textFile;
    //An array for the lines of text that will come into the code as a 'string'. 
    public string[] textLines;

	
	void Start ()
    {
	    //Checks to see if the text file is there.
        if(textFile != null)
        {
            //Getting the textLines array and put in the array the lines (text) from the textFile and split it into seperate pieces via the notation of '\n' (The way the system reconizes that a new line has been inserted).
            textLines = (textFile.text.Split('\n'));

        }
        	
	}
	

}
