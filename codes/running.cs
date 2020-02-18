// this script is for tiger game object for its different animations like walk, roar etc
// game object must have a box collider and rigid body component attached to it for animation to come to effect
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class running : MonoBehaviour {

    private Animator anim; // initializing animator component ot anim.
    Vector3 destPosition; // initializing for storing hit position from raycast, vector3 since it has x, y and z coordinate value.
    Vector3 lookingTarget; // vector positioning to direction of move.
    Quaternion objectRotation; // structure that holds the rotation.
    float rotationSpeed = 5; // for rotation speed.

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>(); // assigning animator component to variable anim.
    }

    // Update is called once per frame
    void Update ()
    { 
        // animation will only play if game is not paused from PauseMenu script
        if (!PauseMenu.GameIsPaused)
        {
                if (Input.GetMouseButtonDown(0) && BoardManager.isRedTurn) // left click of the mouse and is a tiger turn.
                {
                    RaycastHit hit; // from camera center point of view for input mouse position.
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    //If raycast out hits an object and that object has a box collider, animation sets to execute.
                    if (Physics.Raycast(ray, out hit)) // raycast that we get from camera itself.
                    {
                        if (hit.collider == gameObject.GetComponent<BoxCollider>()) // if raycast hits the box collider that is added to the tiger game object.
                        {

                           destPosition = hit.point; // stores the hitting coordinate to a variable.
                           // calculates the angle towards the new move direction.
                           lookingTarget = new Vector3(destPosition.x - (int)transform.position.x, 0, destPosition.z - (int)transform.position.z); // y value is set to zero to stop rotation in upward direction.
                           anim.SetBool("isSound", true); // when moving, tiger first starts with a roar sound that last for fraction of second.
                           StartCoroutine(WaitTimerTiger()); //calling coroutine
                           objectRotation = Quaternion.LookRotation(lookingTarget);   // rotation that has to occur with the angle from lookAtRotation.                      
                        }
                    }
                }

                else if (Input.GetMouseButtonUp(0)) // if mouse click is released
                {
                    anim.SetBool("isRunning", false); // stop move animation.
                }
                move();   // if its a game is not paused. 
        }
    }

    // this will actually move the tiger gameobject
    void move()
    {
        // smooth transition of tiger from one rotation to another with current rotation angle (transfrom.rotation) to new rotation angle objectRotation() within a fraction of time.
        transform.rotation = Quaternion.Slerp(transform.rotation, objectRotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator WaitTimerTiger() //coroutine to pause execution of next function.
    {
        yield return new WaitForSeconds(0.5f); // let roar to complete its action before move animation plays
        anim.SetBool("isSound", false); // roar animation is set to stop after 0.5 seconds.
        anim.SetBool("isRunning", true);   // move animation starts at the end of roar.
    }
}
