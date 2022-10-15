using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Experimental.Video;

public class MovingPlatform : MonoBehaviour
{
    //Speed of the Platform
    public float speed;
    //starting position of the Platform
    public int startingPoint;
    //An array of transform points 
    public Transform[] points;

    //index of the array
    private int i;

    private void Start()
    {
        //Set the pos of platform to start/end pos
        transform.position = points[startingPoint].position;
    }

    private void Update()
    {
        //check dist between plat and points
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++;
            // Check if the platform was on the last point after index increase 
            if (i == points.Length)
            {
                //reset index
                i = 0;
            }
        }

        //moving plat to index i position
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed*Time.deltaTime);
    }
}
