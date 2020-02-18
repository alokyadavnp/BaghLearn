using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]  // with this we can add the values with unity inspector.

public class LevelData // this is not a monobehaviour but a pure c# class.
{
    public int pointsForEachAnswer;  // for each correct answer
    public string Levelname; // name of the level   
    public DataForQuestion[] questions; // for number of question in each level
    public int timeRemaining; // for time in each level


}
