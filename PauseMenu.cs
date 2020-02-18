// this script is attached to all the scenes available for pause options.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false; // passing global variable for pause menu.

    public GameObject UIMenuPause; // Initializing gameobject for pausemenu canvas.

    void Update () // called every frame per second
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // if escape/back key is pressed.
        {
            if (GameIsPaused) 
            {
                Resume(); // if game is already paused then will be resumed.

            }
            else
            {
                Pause(); // if game is resumed then will be paused.

            }
        }		
	}

    public void Resume() // for resume 
    {
        UIMenuPause.SetActive(false); // pause canvas is set false
        Time.timeScale = 1.0f;
        GameIsPaused = false; // pause bollean is set to false.
       // backgroundmusic.Instance.gameObject.GetComponent<AudioSource>().UnPause();


    }

    void Pause()
    {
        UIMenuPause.SetActive(true); // pause menu is set to active
        Time.timeScale = 0.0f; // freezes the game screen if set to 0.
        GameIsPaused = true; // pause bollean is set to true
        //backgroundmusic.Instance.gameObject.GetComponent<AudioSource>().Pause();

    } // for pause

    public void StartAgain() // for restarting the game 
    {
        Time.timeScale = 1f; // unfreeze the game screen  if set to 1.
        GameIsPaused = false; // game is set off for pause unless escape button is pressed
        SceneManager.LoadScene("game1scene"); // moves to scene of gameplay.

    }

    public void BackGame() // takes to home menu
    {
        BoardManager.Count = 0; // count set to 0 from boardmanager script for Learning to restart.
        Time.timeScale = 1f; // unfreeze the game screen if set to 1.
        GameIsPaused = false; // game is set off for pause unless escape button is pressed
        SceneManager.LoadScene("Mainmenu"); // moves to scene of home page.
    }

    public void Exit() // for game quit
    {
        Debug.Log("QUIT!!");
        Application.Quit(); // game quits
    }
}
