using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(hasHit == false)
        {
            arrowMovement();
        }
    }
    void arrowMovement()
    {
        // direction
        Vector2 direction = rb.velocity;
        // angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // get angle and axis we wish rotation to be performed
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the arrow hits something
        hasHit = true;

        // need to add due damage code here 
    }
}
