// this script presists through out the scenes.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// we are going to create a series of small classes which is not monobehaviour and going to use system.
//serializable reason for adding this is so that we can edit them and display their values using the unity inspector.
[System.Serializable] // with this we can add the values with unity inspector.
public class DataForAnswer
{
    public bool isrightOpton; // to hold if the answer is correct.
    public string TextForAnswer; // to hold the answer
    


	
}
