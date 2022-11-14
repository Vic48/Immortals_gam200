using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private HeXiangu hXG;
    public GameObject arrow;
    public float launchForce;
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;
    public bool isFlip;

    public Vector2 position;

    private void Start()
    {
        points = new GameObject[numberOfPoints];
        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }
    }
    private void Update()
    {
        //bow pos as pt A
        Vector2 bowPosition = transform.position;

        //mouse pos as pt B
        //returns the pos of mouse cursor in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //direction bet A and B
        Vector2 Direction = mousePosition - bowPosition;

        isFlip = hXG.GetComponent<SpriteRenderer>().flipX;

        //Make right pos as the direction
        if ((Direction.x >= 0 && isFlip == false) || (Direction.x <= 0 && isFlip == true))
        {
            Debug.Log(Direction.x);
            transform.right = Direction;
        }

        if (Input.GetMouseButtonDown(0))
        {
            shoot();
        }

        for(int i = 0; i < numberOfPoints; i++)
        {
            points[i].transform.position = PointPosition(i * spaceBetweenPoints);
        }
    }
    void shoot()
    {
        //store new created arrow in an object
        GameObject newArrow =Instantiate(arrow, shotPoint.position, shotPoint.rotation);

        //fetch rb components 
        newArrow.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)shotPoint.position + (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        return position;
    }

}
