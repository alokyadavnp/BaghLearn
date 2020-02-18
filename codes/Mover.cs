
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Mover : MonoBehaviour
{
    public Rail rail; // instantiating a rail
    public PlayMode mode; // to choose from different mode.

    public float speed = 2.5f;
    public bool isGoingBackward; // to reverse the movement.
    public bool isLooping; // if wanted to loop between.
    public bool Bounceback; // 


    private int PresentPor; // existing segment.
    private float Conversion; // this is the ratio.
    private bool isEnded;


	// Use this for initialization
	void Start ()
    {
         isLooping = true;
          Bounceback = true;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!rail) 
            return; // if no rail simply return.

        if(!isEnded) // if not completed then play.
            Play(!isGoingBackward);		// since it is moving in forward direction by default.
	}

    private void Play(bool forward = true) // moving in forward direction
    {
        float m = (rail.nodes[PresentPor + 1].position - rail.nodes[PresentPor].position).magnitude; //(length) getting magnitude of the node
        float s = (Time.deltaTime * 1 / m) * speed; // spend equal amount of time on each segment despite segment length.
        Conversion += (forward) ? s : -s; // if + then forward, if - then reverse direction.
        if(Conversion > 1) // if its value is 1 means moved to next segment.
        {
            Conversion = 0; // resetting value for each segment.
            PresentPor++; // inceasing present segment to next
            if( PresentPor == rail.nodes.Length - 1) // if on the last segment.
            {
                if(isLooping) 
                {
                    if(Bounceback) // if bouncing back at the last segment on both end.
                    {
                        Conversion = 1; // reversing the value from 0 to 1 
                        PresentPor = rail.nodes.Length - 2; // next movable segment will be one backward
                        isGoingBackward = !isGoingBackward;
                    }
                    else // if no bouncing back
                    {
                        PresentPor = 0; // resetting back next segment to zero.
                    }
                }
                else // if not returning back from last segment
                {
                    isEnded = true; // end the rail
                    return;
                }
            }

        }
        else if(Conversion < 0) // means we are going in the other direction.
        {
            Conversion = 1; // we are starting fresh on the new segment
            PresentPor--; // going backward to next segment
            if (PresentPor == - 1) // if the first segment.
            {
                if (isLooping)
                {
                    if (Bounceback) // if bouncing back at the segments on both end.
                    {
                        Conversion = 0; // reversing the value from 1 to 0 
                        PresentPor = 0; // next movable segment will be first one
                        isGoingBackward = !isGoingBackward;
                    }
                    else
                    {
                        PresentPor = rail.nodes.Length - 2; // if not bouncing back from first segment.
                    }
                }
                else
                {
                    isEnded = true; // if not looping then end rail.
                    return; 
                }
            }

        }

        transform.position = rail.RailLocation(PresentPor, Conversion, mode); // moving object from current position from rail script.
        transform.rotation = rail.Orientation(PresentPor, Conversion); // rotating object for current position.
    }
}
