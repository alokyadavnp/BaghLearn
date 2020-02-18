using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// we need to abstract data from an external file called JSon file and for that we need to serialize an object.
// here we are taking the game data into a string of JSON, its all public variables and writing them into a text into a JSON file called serialization.
// and later when we are going to use that data we are deserializing and use them to populate the field of our game data object.




[System.Serializable] // for serialization.
// this is just gonna be a data class with no variables.
public class DataForGame // this is not a monobehaviour but a pure c# class.
{
    public LevelData[] LevelData; // array of all level data. 
	
}
