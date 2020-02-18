using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public enum PlayMode // enumerator of choices for type of movement.
{
    Straight,
    Catmull,
}

[ExecuteInEditMode] // executing the array of node in editorial mode.
public class Rail : MonoBehaviour
{
    public Transform[] nodes; // referencing to nodes.

	// Use this for initialization
	void Start ()
    {
        nodes = GetComponentsInChildren<Transform>(); //assigning component of type Transform  to variable nodes.

    }

    public Vector3 RailLocation(int Por, float ratio, PlayMode mode) // choice of movement.
    {
        switch (mode)
        {
            default: // no if mode is defined then it will choose to be linear 
            case PlayMode.Straight: // if linear movement is what you want
                return StraightPosition(Por, ratio);

            case PlayMode.Catmull: // if smooth transition is what you want.
                return CatmullPosition(Por, ratio);
        }
    }

    public Vector3 StraightPosition(int Por, float ratio) // creating position on rail in Straight manner.
    {
        Vector3 p1 = nodes[Por].position; // position of first node
        Vector3 p2 = nodes[Por+ 1].position; // position of next node.

        return Vector3.Lerp(p1, p2, ratio); // smooth movement between first and second position.
    }

    public Vector3 CatmullPosition(int Por, float ratio) // for smooth curvy movement between nodes.
    {
        Vector3 p1, p2, p3, p4; // four points need for catmull equation.

        if (Por== 0) // if it is a first node
        {
            p1 = nodes[Por].position; // first position is a position behind.
            p2 = p1;                // start position. // P1 and P2 are same for first position since there is no point backward
            p3 = nodes[Por+ 1].position; // third position is one position from start position.
            p4 = nodes[Por+ 2].position; // fourth position is two position away from start position.
        }
        else if (Por== nodes.Length - 2) // if on the last segment
        {
            p1 = nodes[Por- 1].position; // first position is a position behind.
            p2 = nodes[Por].position;   // start position.
            p3 = nodes[Por+ 1].position;  // third position is one position from start position.
            p4 = p3; // same as position P3 since for last there is no forward position.
        }
        else // other than first and last segment
        {
            p1 = nodes[Por- 1].position; // position behind.
            p2 = nodes[Por].position; // start position
            p3 = nodes[Por+ 1].position; // one position forward
            p4 = nodes[Por+ 2].position; // two position forward.
        }

        float t2 = ratio * ratio; // square of ratio.
        float t3 = t2 * ratio;  // cube of ratio.

        float x =                          // curve x position from catmull equation.
            0.5f * ((2.0f * p2.x)
            + (-p1.x + p3.x) * ratio
            + (2.0f * p1.x - 5.0f * p2.x + 4 * p3.x - p4.x) 
            * t2 + (-p1.x + 3.0f * p2.x - 3.0f * p3.x + p4.x)
            * t3);
        float y =                   // curve y position from catmull equation.
            0.5f * ((2.0f * p2.y)
            + (-p1.y + p3.y)
            * ratio + (2.0f * p1.y - 5.0f * p2.y + 4 * p3.y - p4.y)
            * t2 + (-p1.y + 3.0f * p2.y - 3.0f * p3.y + p4.y)
            * t3);

        float z =                   // curve y position from catmull equation.
            0.5f * ((2.0f * p2.z)
            + (-p1.z + p3.z)
            * ratio + (2.0f * p1.z - 5.0f * p2.z + 4 * p3.z - p4.z)
            * t2 + (-p1.z + 3.0f * p2.z - 3.0f * p3.z + p4.z)
            * t3);

        return new Vector3(x, y, z); // value with x, y, and z.

    }

    public Quaternion Orientation(int Por, float ratio) // rotation of rail object
    {
        Quaternion q1 = nodes[Por].rotation; // rotation for first node position
        Quaternion q2 = nodes[Por+ 1].rotation; // rotation for second node position

        return Quaternion.Lerp(q1, q2, ratio); // for smooth rotation between nodes.
    }

    private void OnDrawGizmos() // Drawing invisible lines following the rail path. 
    {
        #if UNITY_EDITOR 
        for (int i = 0; i < nodes.Length - 1; i++) // to every nodes present in the rail path.
        {
           
            Handles.DrawDottedLine(nodes[i].position, nodes[i + 1].position, 3.0f); // DrawDottedLine is an editorial function which can only be called for Unity engine but can't be used for build functionality
        }                                                                           // for this function to be able to be convertible to build, every editorial functionality is started and end with if and end if.
        #endif
    }
	
	
}
