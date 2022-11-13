using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;

    private void Start()
    {
        //get the rb that is attached to the arrow now
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (hasHit == false)
        {
            //cal the angle in degree
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            //modify the rotation
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the arrow hits something
        hasHit = true;

        // arrow stop moving completely 
        rb.velocity = Vector2.zero;

        //arrow dont be affected by gravity
        rb.isKinematic = true;

        // need to add due damage code here 
    }
}
