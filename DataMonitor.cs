// this script will continue to load through out the scenes since this scene is responsible to load data needed for execution.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking; // to load data from Json File using handler.

public class DataMonitor : MonoBehaviour
{
    private LevelData[] LevelData; // to include multiple level at later stage of the game.
      
    private PlayerDevelopment playerdevelopment; // initializing variable to store player score development.
    private string DataFileName = "data.json";

    void Start ()
    {
        Time.timeScale = 1f;
        DontDestroyOnLoad(gameObject); // so that game scene is not destroyed when loaded new scenes.
        SupplyPlayerDevelopment();
        StartCoroutine(LoadData()); // load game data at the time of start
        SceneManager.LoadScene("MenuScreen"); // its role is to supply the round data to the game controller when we get to that scene
        

	}

    public LevelData PresentLevelData()
    {
        if (SceneController.ClickCount == 1) // if it is the first choice from topic selection
        {
            return LevelData[Random.Range(1, 4)]; // will random return Level data from index 1 to 3.
        }

        if (SceneController.ClickCount == 2) // if it is the second choice from topic selection
        {
            return LevelData[Random.Range(4, 7)]; // will random return Level data from index 4 to 6.
        }

        if (SceneController.ClickCount == 3) // if it is the third choice from topic selection
        {
            return LevelData[Random.Range(7, 10)];  // will random return Level data from index 7 to 9.
        }

        if (SceneController.ClickCount == 4) // if it is the fourth choice from topic selection
        {
            return LevelData[Random.Range(10, 13)]; // will random return Level data from index 10 to 12.
        }

        if (SceneController.ClickCount == 5) // if it is the fifth choice from topic selection
        {
            return LevelData[Random.Range(13, 16)]; // will random return Level data from index 13 to 15.
        }

        if (SceneController.ClickCount == 6) // if it is the sixth choice from topic selection
        {
            return LevelData[Random.Range(16, 19)]; // will random return Level data from index 16 to 18.
        }

        else
        {
            return LevelData[0]; // if not the given choice then return Level data from index 0.
        }

    }

   

    public int GetLatestPlayerDevelopment() // ask the datamonitor for the highest score and returns the score
    {
        return playerdevelopment.TigerhighestScore; 
    }

    public void SubmitNewPlayerDevelopment(int latestScore) // gamemonitor submits a new score at the end of every round.
    {
        // if score from the level is either greater of less than previous level, in either case submit the new score
        if (latestScore > playerdevelopment.TigerhighestScore || latestScore < playerdevelopment.TigerhighestScore)
        {
            playerdevelopment.TigerhighestScore = latestScore; // new score is the current score.
            SavePlayerDevelopment();
        }
    }

    IEnumerator LoadData() //coroutine to load the questions for data from a Json file
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, DataFileName); // for file path of gamedata file path that is in the asset folder                                                                                   // string dataAsJson = "";
        UnityWebRequest www = UnityWebRequest.Get(filePath);
        yield return www.Send();
        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string dataAsJson = www.downloadHandler.text;
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson.Trim()); // populate gamedata object from Json string.
            LevelData = loadedData.LevelData;
        }
    }    

    private void SavePlayerDevelopment() // saving score
    {     
        // it stores a session value with a key TigerhighestScore.
      PlayerPrefs.SetInt("TigerhighestScore", playerdevelopment.TigerhighestScore); 
      PlayerPrefs.Save();
    }
 
    // Update is called once per frame
    void Update ()
    {
        if (BoardManager.gameIsOver == true) // if gameover from Boardmanager then destroy the gameObject for new start
        {
            Destroy(GameObject.Find("DataController"));
        }      
    }

    private void SupplyPlayerDevelopment() // loading saved score value
    {
        playerdevelopment = new playerdevelopment(); //set to new object.

        if (PlayerPrefs.HasKey("TigerhighestScore")) // if stored a session value with key TigerhighestScore
        {
            // it takes a string that we get the value with key TigherhighestScore
            playerdevelopment.TigerhighestScore = PlayerPrefs.GetInt("TigerhighestScore");
        }
    }
}
