using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //camera speed to the target
    public float FollowSpeed = 2f;
    public float yoffset = 1f;
    public Transform target;

    private CameraZoom camZoom;

    public void Start()
    {
        // find the camera zoom script
        camZoom = GameObject.Find("Main Camera/Zoom").GetComponent<CameraZoom>();
    }

    private void Update()
    {
        // if zooming camera, do not control camera here la. Only one can control at one time!
        if (!camZoom.isZoomActive)
        {
            //Player position; 2d camera z pos stays -10
            Vector3 newPos = new Vector3(target.position.x, target.position.y + yoffset, -10f);
            //change current pos to target pos
            transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
        }
    }
}
