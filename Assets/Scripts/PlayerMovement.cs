using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHight;
    [SerializeField] private LayerMask groundLayer;
    private Rigidbody2D playerBody;
    private Animator anim;
    private BoxCollider2D boxCollider;
    public float horizontalInput;
    public int dir;

    private void Awake()
    {
        // Grab references
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // run
        run();

        // Jump
        Jump();
    }

    private void run()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            dir = 1;
        }
        else if (horizontalInput < -0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            dir = -1;
        }

        playerBody.velocity = new Vector2((horizontalInput) * runSpeed, playerBody.velocity.y);
        // set animation parameter, Run = has horizontalInput or not
        // If has horizontal input, Run = true else false
        anim.SetBool("Run", horizontalInput != 0);
    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded())
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpHight);
            anim.SetTrigger("Jump");
        }

        // set animation parameter, Grounded = grounded
        // If collision with other 2D object, set Grounded = true, else false
        anim.SetBool("Grounded", isGrounded());
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            0.1f,
            groundLayer
        );
        // Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        //Character can only attack when not moving or jumping
        return horizontalInput == 0 && isGrounded();
    }
}
