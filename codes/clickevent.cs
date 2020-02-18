// This script is attached to buttons.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickevent : MonoBehaviour
{
    AudioSource source; // passing audio Source variable.
    public AudioClip hover;  // passing public variable onhover sound.
    public AudioClip click; // passing public variable for onclick sound.

    // Use this for initialization
    void Start ()
    {
        source = GetComponent<AudioSource>(); // assigning audio source component to variable source.
    }

    public void OnHover() // action onHovering over the button
    {
        source.PlayOneShot(hover); // playing audioclip
    }

    public void OnClick() // action onclick 
    {
        source.PlayOneShot(click); // playing audioclip
    }


}
