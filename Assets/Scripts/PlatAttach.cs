using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatAttach : MonoBehaviour
{
    public GameObject LvDongbin;
    public GameObject HeXiangu;

    //Speed of the Platform
    public float speed;

    public GameObject platform;
    public Collider2D triggerBox;
    //starting position of the Platform
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float lvDongbinHeight;
    public float heXianguHeight;

    //index of the array
    private int i;
    private void Start()
    {
        // set start point and end point
        startPoint = new Vector3(0, 0, 0);
        endPoint = new Vector3(0, 23, 0);
        // Set the pos of platform to start/end pos
        platform.transform.localPosition = startPoint;
;
    }

    private void Update()
    {
        if (
            (LvDongbin != null && triggerBox.IsTouching(LvDongbin.GetComponent<Collider2D>())) ||
            (HeXiangu != null && triggerBox.IsTouching(HeXiangu.GetComponent<Collider2D>()))
        ) 
        {
            Vector3 currentPosition = platform.transform.localPosition;
            // move parent
            platform.transform.localPosition = Vector3.MoveTowards(
                currentPosition,
                endPoint,
                speed * Time.deltaTime
            );
            LvDongbin.transform.position = new Vector3(
                LvDongbin.transform.position.x,
                platform.transform.position.y + lvDongbinHeight,
                0
            );
            HeXiangu.transform.position = new Vector3(
                HeXiangu.transform.position.x,
                platform.transform.position.y + heXianguHeight,
                0
            );
        }
        
    }
}
