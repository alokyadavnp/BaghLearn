// attached to blood particles.
// active when a goat is killed.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyblood : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Destroy(gameObject, 2f); // blood particle is washed out from the game scene after 2 seconds of its apperance.
		
	}
}
