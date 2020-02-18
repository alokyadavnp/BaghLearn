using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable] // with this we can add the values with unity inspector.

public class DataForQuestion // this is not a monobehaviour but a pure c# class.
{
    public SolutionData[] solutions; // to hold a number of solutions associated with it
    public string TextForQuestion; // for question text
    
	
}
