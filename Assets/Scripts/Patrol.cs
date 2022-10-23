using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [HideInInspector] public bool mustPatrol;
    public Rigidbody2D enemyBody;
    public Animator anim;

    public float walkSpeed;

    void Start()
    {
        mustPatrol = true;

    }
    void Update()
    {
        if (mustPatrol)
        {
            AIPatrol();
        }
    }
    void AIPatrol()
    {
        enemyBody.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, enemyBody.velocity.y);
        float horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontalInput < -0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        enemyBody.velocity = new Vector2((horizontalInput) * walkSpeed, enemyBody.velocity.y);
        // set animation parameter, Run = has horizontalInput or not
        // If has horizontal input, Run = true else false
        anim.SetBool("Run", horizontalInput != 0);
    }
}
