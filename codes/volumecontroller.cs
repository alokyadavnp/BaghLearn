//this script is called when volume controller bar is active
//present at Option button of Home Screen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class volumecontroller : MonoBehaviour
{

    public AudioMixer audioMixer; // referencing audiomixer component to the slider.

    [Space(10)]
    public Slider musicSlider; // Adding slider button to Game with a size of 10, means value will decrease to 1/10 with each slide.

    public void SetMusicVolume(float volume) // setting volume control slider on Home screen.
    {
        //audioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
        audioMixer.SetFloat("musicVolume", volume); // setting volume of the mixer with slider that is referenced with audiomixer.
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0); // Game starts with same sound value at the level it was saved.       
    }

    private void OnDisable() // saving volume slider value on exit.
    {
        float musicVolume = 0;

        audioMixer.GetFloat("musicVolume", out musicVolume); // gets the value of slider at the time of exit.
        PlayerPrefs.SetFloat("musicVolume", musicVolume); // saves the get value.
        PlayerPrefs.Save(); 


    }
}
