using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWarp : MonoBehaviour {
    //This allows for the player to 'Warp' between places on your map.
    /*
    ---This will Include---
    -Target - for the script to send the Player to.
    -In Unity: The 'Exit' gameobjects will need a colider and will need to be set to 'Is Trigger'.
    --Also will include an animationt that calls the 'ScreenFade' script.
    */

    public Transform warpTarget;


    //Notes if the player has entered the door coliders, etc..
    //Had to change from 'void' to 'IEnumerable', allowing you to use 'yield' statements within your function.
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        //Referance to the 'ScreenFader' script. 
        ScreenFader sf = GameObject.FindGameObjectWithTag("Fader").GetComponent<ScreenFader>();

        Debug.Log("Pre Fade out.");

        //Yield stops the system from running code until this statement that is being called is completed. 
        yield return StartCoroutine (sf.FadeToBlack());

        Debug.Log("Update Player pos.");

        //This will note in the 'Console' if something has happned.
        Debug.Log("An object has collided");

        //Simple Warp system
        //When you collide with the trigger it will adjust your pos to the 'Target' destination and moves the camera to the 'warpTarget'.
        other.gameObject.transform.position = warpTarget.position;//'other' represents the gameObject that collided with the trigger. 
        Camera.main.transform.position = warpTarget.position;

        yield return StartCoroutine(sf.FadeToClear());

        Debug.Log("Fade in Complete.");

       

    }

}
