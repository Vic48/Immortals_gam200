using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeXiangu : MonoBehaviour
{
    public Vector2 direction;
    public float LaunchForce;
    //public GameObject Arrow;
    public PlayerMovement playerMovement;

    private void Update()
    {
        //Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ////bow pos
        //Vector2 bowPos = transform.position;
        ////Cal the direction
        //direction = bowPos - MousePos;

        //if (Input.GetMouseButtonDown(0))
        //{
        //    //When press mouse launch an arrow
        //    Shoot();
        //}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }
    }

    void Shoot()
    {
        // instantiate the arrow at the exact simple disposition and rotation
        // change the transform to 
        //GameObject newArrow = Instantiate(Arrow, transform.position, transform.rotation);
        //Apply force on arrow just created 
        //force to arrow is on x-axis
        //newArrow.GetComponent<Rigidbody2D>().AddForce(transform.right * playerMovement.dir * LaunchForce);

    }

    //rotation
    void FaceMouse()
    {
        //x-axis of the bow
        transform.right = direction;
    }
}