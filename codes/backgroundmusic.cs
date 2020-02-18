// This script is attached to the board for uninterrupted music transation between the scenes.
// This won't let sound gameObject to distroy as we move to the next scene.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backgroundmusic : MonoBehaviour
{
    private static backgroundmusic instance = null; // assigned a static variable to play Global of type same as that of the class.
    public static backgroundmusic Instance
    {
        get { return instance; }  // it also returns static instance of same type as of the class for the static function 

    }

    // unity method called when the script instance is being loaded.
    // used to initialize game variable before start of the game. 
    void Awake() 
    {
        if (instance != null && instance != this) // checking if instance(backgroundmusic) is not null and current instance is not the first scene.
        {
            Destroy(this.gameObject); // if the condition is matched destroys that gameObject and return.
            return;
        }
        else
        {
            instance = this; // if it is first scence then instance will be assigned to the current scene.
        }

        DontDestroyOnLoad(this.gameObject); // if instance of the game in first scene then this method makes sure the object target is not destroyed ...
    }                                       // .. automatically when loading a new scene.
}


