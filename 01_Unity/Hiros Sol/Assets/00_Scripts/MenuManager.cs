using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Allows the use of calling scenes in the game.
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour{

    public void NewGameButton(string StartGame)
    {
        //Calls the 'SceneManager' to load a particular scene. 
        SceneManager.LoadScene("01_RootIsland_Beach");

    }

    public void EndGame()
    {
        //Quits the application. 
        Application.Quit();

    }

}