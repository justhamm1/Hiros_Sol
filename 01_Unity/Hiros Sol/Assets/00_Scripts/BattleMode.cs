using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Allows the use of calling scenes in the game.
using UnityEngine.SceneManagement;

//I made dis!
    //You're a fucking child...
public class BattleMode : MonoBehaviour {
    //This is build to create a simple battle system to launch into associated scenes. 

    //Referance to the name of the battle scene you wish to load. 
    public string battle;


    //This is the system that will call and launch the battle scenes. 
        //'OnTriggerEnter2D' only works for something that is labeld a 'Trigger'
            //As opposed to an 'OnCollisionEnter2D' which only works if both objects are set to non-triggers.
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Hero")
        {
            Debug.Log("Let the fighting begins!");
            SceneManager.LoadScene(battle);

        }

    }
}
