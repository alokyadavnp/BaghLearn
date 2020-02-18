using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonForAnswer : MonoBehaviour
{
    private GameMonitor Gamemonitor; // adding gamemonitor from script GameMonitor
    public Text TextforAnswer; // store reference to answer button text
    AudioSource source; // initializing audiosource component
    public AudioClip click; //initializing sound gameobject.

    private DataForAnswer dataforanswer; // initializing the variable.
    

	// Use this for initialization
	void Start ()
    {
        gamemonitor = FindObjectOfType<GameMonitor>(); // finding the reference to the GameMonitor.
        //that the button being diplayed on the screen is in the gamemonitor to safely find it.
        source = GetComponent<AudioSource>();
    }

    public void Arrange(DataForAnswer data) // takes answerdata of type data as argument.
    {
        dataforanswer = data; // data that is passed from function
        TextforAnswer.text = dataforanswer.TextforAnswer; // string from the script DataForAnswer is gonna pass to ButtonForAnswer
    }
	
	// Update is called once per frame
	
    public void HandleClick() 
    {
        //button clicked function to tell gamemonitor that the answer clicked is the correct one.
        gamemonitor.CorrectAnswerSelected(dataforanswer.isTrue);
    }

    public void OnClick()
    {
        source.PlayOneShot(click);
    }
}
