// for flashing play button on Home Screen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashingText : MonoBehaviour
{
    public float timer; // initiating variable to store timer value.

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime; // stores time between the current and previous frame.

        if (timer >= 0.5) 
        {
            GetComponent<TextMeshProUGUI> ().enabled = true; // enabling text component if stored timer value is greater than 0.5 second and less than 1 second.
        }

        if (timer >= 1)
        {
            GetComponent<TextMeshProUGUI> ().enabled = false; // enabling text component if stored timer value is greater than 1 second.
            timer = 0; // set the timer value to 0 for next operation in loop.
        }		
	}
}
