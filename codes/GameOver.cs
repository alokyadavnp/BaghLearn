// this script is called when gameover condition is meet by any player side.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class GameOver : MonoBehaviour
{
    GameObject[] alok;

    void Start() // initiating parameters at the call of the script.
    {
        Time.timeScale = 1f; // game screen is unfreezed.
        BoardManager.gameIsOver = false; // unfreezing the game after gameover condition.
    }

    public void Quit() // Application exit
    {
       // Debug.Log("APPLICATION QUIT!"); 
        Application.Quit(); // simply quit the game
    }

    public void Retry() // restarting the game
    {
        PlayerPrefs.SetInt("GoathighestScore", 70); // score set to 70 for no player generation at first
        PlayerPrefs.SetInt("TigerhighestScore", 70);
        BoardManager.Count = 0; // count from Boardmanager scipt is set to 0 as default score.
        Time.timeScale = 1f; // unfreezing the game scene if it was in any case.
        SceneManager.LoadScene("game1scene"); // simply changing the state of game to restart.
    }
          
    public void Home() // reloding the home screen.
    {
        Time.timeScale = 1f; // // unfreezing the game scene if it was in any case.
        PauseMenu.GameIsPaused = false; // game is unpaused if it was in any case.
        BoardManager.Count = 0; // count from Boardmanager scipt is set to 0 as default score.
        SceneManager.LoadScene("Mainmenu"); // changing the state of the game to Home Screen.
    }
}
