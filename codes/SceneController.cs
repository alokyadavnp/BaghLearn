using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    AudioSource source;
    public AudioClip hover; // initializing for onhover sound.
    public AudioClip click; // initializing for onclick sound.
    public static int ClickCount; // passing global variable for click event.

    private void Start()
    {
        source = GetComponent<AudioSource>(); // assigning audio source component to variable source.
    }

    private void Update()
    {
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");// this will take us to quiz game when start button pressed.
    }

    public void Learning()
    {
        StartCoroutine(LoadLearningScene()); // calling coroutine.
    }

    public void MenuScene()
    {
        StartCoroutine(LoadMenuScene()); // calling coroutine.
    }

    public void OnHover() // function for hover sound event
    {
        source.PlayOneShot(hover); // playing audioclip on mouse hover
    }

    public void OnClick() // function for onclick sound event
    {
        source.PlayOneShot(click); // playing audioclip on mouse click
    }

    public void DynamicProgramming()
    {
        ClickCount = 1; // set global int value of click count to 1 when Selected.
    }

    public void BranchandBound()
    {
        ClickCount = 2; // set global int value of click count to 2 when Selected.
    }

    public void Randomized()
    {
        ClickCount = 3; // set global int value of click count to 3 when Selected.
    }

    public void GreedyAlgo()
    {
        ClickCount = 4; // set global int value of click count to 4 when Selected.
    }

    public void Approximation()
    {
        ClickCount = 5; // set global int value of click count to 5 when Selected.
    }

    public void DivideandConquer()
    {
        ClickCount = 6; // set global int value of click count to 6 when Selected.
    }

    IEnumerator LoadLearningScene() //coroutine to pause the execution of function.
    {
        yield return new WaitForSeconds(0.2f); // with the pause of 0.2 second next line of code executes.
        SceneManager.LoadScene("Learning"); // takes to scene dedicated to learning on topics.
    }

    IEnumerator LoadMenuScene() //coroutine to pause the execution of function.
    {
        yield return new WaitForSeconds(0.2f); // with the pause of 0.2 second next line of code executes.
        SceneManager.LoadScene("Mainmenu"); // takes to scene that loads quiz.
    }
}
