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

    // import play attch script
    private PlatAttach platAttach;
    private int whichPlayerOn = 0; // 0 no one, 1 lv is one, 2 he is on platform
    private Collider2D playerCollider;

    //index of the array
    private int i;

    private void Start()
    {
        //Set the pos of platform to start/end pos
        transform.position = points[startingPoint].position;

        // find plat attach
        platAttach = GameObject.Find("Plat_Move").GetComponentInChildren<PlatAttach>();
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

        if (gameControl.Instance.isPlayerOnPlat && whichPlayerOn != 0 && !!playerCollider)
        {
            if (whichPlayerOn == 1)
            {
                Transform colliderTransform = playerCollider.transform;
                Vector3 originalPosition = colliderTransform.position;
                colliderTransform.position = new Vector3(originalPosition.x, gameObject.transform.position.y + platAttach.lvDongbinHeight, originalPosition.z);
            }
            if (whichPlayerOn == 2)
            {
                Transform colliderTransform = playerCollider.transform;
                Vector3 originalPosition = colliderTransform.position;
                colliderTransform.position = new Vector3(originalPosition.x, gameObject.transform.position.y + platAttach.heXianguHeight, originalPosition.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "LvDongbin")
        {
            this.whichPlayerOn = 1;
            this.playerCollider = collision.collider;
        }
        if (collision.collider.name == "HeXiangu")
        {
            this.whichPlayerOn = 2;
            this.playerCollider = collision.collider;
        }
        gameControl.Instance.isPlayerOnPlat = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        this.whichPlayerOn = 0;
        this.playerCollider = null;
    }
}
