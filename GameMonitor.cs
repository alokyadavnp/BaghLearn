using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMonitor : MonoBehaviour
{
    public Transform ButtonForAnswerParent; // perenting answerbutton object
    public Text TigerhighScoreDisplay; // high score display for the round.   
    public Text TextForQuestion; // to display the question text
    public Text scoreDisplayText; // for score to display
    public Text timeRemainingforDisplayText; // for displaying time on screen

    private float Remainingtime; // amount of time left in the round.
    private bool isLevelLive; // to check the level status if its over by any condition. like time out
    private int scoreForPlayer; // for score
    private int questionIndex; // index of question
    
    private DataForQuestion[] dataQuestion; // array for question data pool
    public GameObject questionPanel; // Display screen for questions
    public GameObject roundEndPanel; //  Display screen for roundEnd
    private LevelData presentLevelData; // initializing present level.

    public SimpleObjectPool answerButtonObjectPool; // since we are going to interact with the object pool
    private DataMonitor datamonitor; // initializing data monitoring.
       
    private List<GameObject> ButtonForAnswerGameObjects = new List<GameObject>(); // to list all the gameObject of type buttonforanswer



    // Use this for initialization
    void Start()
    {
        // here datamonitor is supplying all the details for present level.
        Time.timeScale = 1f;
        datamonitor = FindObjectOfType<DataMonitor>();  // get and store a reference of monitor data
        presentLevelData = datamonitor.PresentLevelData(); // storing from the datamonitor and store it in our presentleveldata
        dataQuestion = presentLevelData.questions; // leveldata to dataquestion to store questions for asking
        Remainingtime = presentLevelData.timeRemaining; // for remaining time
        UpdateTimeRemaningforDisplay();

        scoreForPlayer = 0; // initializing score to 0 at start
        questionIndex = 0; // since starting from initial

        DisplayQuestion(); // for Level question
        isLevelLive = true; // because level has started.

    }

    void Update()
    {
        if (isLevelLive) // check if active
        {
            Remainingtime -= Time.deltaTime; // to substract time in each subsequent whole second.
            UpdateTimeRemaningDisplay(); // once substracted update time display 

            if (Remainingtime <= 0f) // checking if time ran out in that case we gonna end the round.
            {
                RoundEnd();
            }
        }
    }

    // clear all the answer button before sending the level data to the question data
    private void DeleteButtonsForAnswers() 
    {
        // to remove all the answer button from previous questions and return to the answer data that are there at the start of new question
        while (ButtonForAnswerGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(ButtonForAnswerGameObjects[0]);
            // if the gameobject element is 0 in our list is going to get return to the answer data meaning
            // its going to be deactivated and ready to be recycled an reused.
            ButtonForAnswerGameObjects.RemoveAt(0); //removing from the list of active answer button
        }
    }

    private void DisplayQuestion()
    {
        DeleteButtonsForAnswers(); // first thing to do  before showing next question.
        DataForQuestion questionData = dataQuestion[questionIndex]; // pulling question from that question pool index and storing it in question data
        TextForQuestion.text = questionData.TextForQuestion; // reaching to our pool of questions, get the questions and from there to the string of question.

        for (int i = 0; i < questionData.solutions.Length; i++) // from 0 to number of answer buttons it have
        {
            // buttonforanswer gameobject that we will be working with that we get from answerpool.
            GameObject ButtonForAnswerGameObject = answerButtonObjectPool.GetObject();
            // to spawn answer button and parenting the answer button.
            ButtonForAnswerGameObjects.Add(ButtonForAnswerGameObject);
            ButtonForAnswerGameObject.transform.SetParent(ButtonForAnswerParent);

            ButtonForAnswer answerButton = ButtonForAnswerGameObject.GetComponent<ButtonForAnswer>();
            // to get a reference to the buttonforanswer script attached to it.
            answerButton.Setup(questionData.solutions[i]); // to set the text of the button to display the correct answer
        }
    }

    public void CorrectAnswerSelected(bool isTrue) // for mouse click event on answers
    {
        if (isTrue) // condition to check if player hits the correct answer
        {
            scoreForPlayer += presentLevelData.pointsForEachAnswer; // increase the point if correct answer
            scoreDisplayText.text = "Score: " + scoreForPlayer.ToString(); // converting player score to a string from integer and append to score.
        }

        if (dataQuestion.Length > questionIndex + 1) // check if there are additional question in the pool
        {
            questionIndex++; // increase question index 
            DisplayQuestion();  // then show the next question.
        }
        else // if there are no more questions in the pool then end the round.
        {
            RoundEnd();
        }
    }

    public void RoundEnd()
    {
        // in RoundEnd function we set the active status of game round to say the round has ended
        isLevelLive = false;
        datamonitor.SubmitNewPlayerDevelopment(scoreForPlayer);   // submitting new player score to the round end screen
        TigerhighScoreDisplay.text = "TigerScore:  " + datamonitor.GetLatestPlayerDevelopment().ToString(); // using highscore display text screen data monitor to display the highest score for the round
        questionPanel.SetActive(false); // since no more questions to display
        roundEndPanel.SetActive(true); // there will be end of the round so it is set to true. 
        counter(); 
    }

    public void counter() // calling coroutine
    {
         StartCoroutine(WaitTimer());
    }

    public void UpdateTimeRemaningDisplay()
    {
        // seperate function to display remaining time after converting to a whole number from int to string.
        timeRemainingforDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }

    // Update is called once per frame
    

    IEnumerator WaitTimer() //coroutine to pause the execution of function.
    {
        yield return new WaitForSeconds(1); // waits for 1 second before execution of function.
        SceneManager.LoadScene("game1scene");   // loads next scene    
    }
}

