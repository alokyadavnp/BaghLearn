// this script is for goat gameobject for triggering death animation along with bool particle system.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killtrigger : MonoBehaviour
{
    Animator _killAnim; // initializing animator component ot anim.
    public GameObject bloodparticlesystem; // holds blood particle for kill animation
    public GameObject splash; // holds blood splash for kill animation

    // Use this for initialization
    void Start()
    {
        _killAnim = this.transform.parent.GetComponent<Animator>(); // assigning animator component from parent gameobject of existing monobehaviour to variable for instantiation.
    }

    private void OnTriggerStay() // unity function that is called almost all the frames for Collider that is touching the trigger.
    {
        StartCoroutine(WaitTrigger()); // calling coroutine
        BoardManager.goatisdead = false; // goatisdead is set to false for next death animation.
    }
    
    IEnumerator WaitTrigger() // execution of death method.
    {
        yield return new WaitForSeconds(1.8f); // holds trigger for 1.8 seconds before start of death animation.
        if (BoardManager.goatisdead) // death animation and particle system is only triggered if bollean goatisdead from BoardManager script is set true.
        {
            Instantiate(bloodparticlesystem, transform.position, Quaternion.identity); // instantiating bood particle right after tiger makes a double jump. 
            Instantiate(splash, transform.position, Quaternion.identity); // instantiating splash particle in parallel to blood particle system.
            _killAnim.SetTrigger("isDead"); // trigger death animation from any running state of Goat once tiger makes a double jump over it. 
            
        }
           
    }
}
