// this script is called when at the Home Screen.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.IO;


public class MainMenu : MonoBehaviour
{

    AudioSource source; // passing audio Source variable
    public AudioClip hover; // passing public variable onhover sound.
    public AudioClip click; // passing public variable for onclick sound


    private void Start()
    {
        source = GetComponent<AudioSource>(); // assigning audio source component to variable source.
    }

    public void PlayGame () // redirects to board game 
    {       
        BoardManager.Count = 0; // count from Boardmanager scipt is set to 0 as default score.
        StartCoroutine(LoadNextScene()); // calling coroutine.
        PlayerPrefs.SetInt("GoathighestScore", 70); //resetting score to 70 for no player generation at first
        PlayerPrefs.SetInt("TigerhighestScore", 70);
    }

    IEnumerator LoadNextScene() //coroutine to pause the execution of function.
    {
        yield return new WaitForSeconds(0.3f); // waits for 1/3 of second before execution of function.
        SceneManager.LoadScene("game1scene"); // loads scene that has board game. 
    }

    public void QuitGame () // for quit option of Home Screen
    {
       // Debug.Log("QUIT!!"); // quits Application
        Application.Quit(); // quits Application
    }

    public void OnHover()
    {
        source.PlayOneShot(hover); // playing audioclip on mouse hover
    }

    public void OnClick()
    {
        source.PlayOneShot(click); // playing audioclip on mouse click
    }

    private void Update()
    { 
        // DataController and DataControllerGoat are two gameObjects that passes between the scene and needs to be deleted at Restart for fresh fetching of game data.
        if (PauseMenu.GameIsPaused == false && SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Mainmenu")) // if game is not paused and current active scene is Home scene.
        {
            Destroy(GameObject.Find("DataController")); // then destroy gameObject.
            Destroy(GameObject.Find("DataControllerGoat"));
        }
    }
}
