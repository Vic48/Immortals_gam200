using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpHight;
    [SerializeField] private LayerMask groundLayer;
    public GameObject target;
    public Transform targetTransform;
    private Rigidbody2D playerBody;
    private Animator anim;
    private BoxCollider2D boxCollider;
    public float horizontalInput;
    public int dir;

    private CameraZoom camZoom;

    private void Start()
    {
        // find the camera zoom script
        camZoom = GameObject.Find("Main Camera/Zoom").GetComponent<CameraZoom>();
    }

    private void Awake()
    {
        // Grab references
        playerBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        bool isPlayerDead = gameControl.Instance.getIsPlayerDead();
        // caution! camera is zooming! charactor do not move!
        if (!camZoom.isZoomActive && !isPlayerDead)
        {
            // run
            run();

            // Jump
            Jump();

            // after run, jump tell parent Player where am I
            UpdatePlayerPosition();
        }
    }

    private void run()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
        {
            flipPlayer(1);
        }
        else if (horizontalInput < -0.01f)
        {
            flipPlayer(-1);
        }

        playerBody.velocity = new Vector2((horizontalInput) * runSpeed, playerBody.velocity.y);
        // set animation parameter, Run = has horizontalInput or not
        // If has horizontal input, Run = true else false
        // If in the air do play play run animation!
        anim.SetBool("Run", horizontalInput != 0 && isGrounded());
    }

    // flip player
    private void flipPlayer(int direction) 
    {
        if (dir == 1) 
        {
            GetComponent<SpriteRenderer>().flipX = false;
            if (gameObject.name == gameControl.playerName.LvDongbin.ToString())
            {
                // flip attack range
                Vector3 attackPosition = gameObject.transform.GetChild(0).localPosition;
                attackPosition.x = 0.9f;
                gameObject.transform.GetChild(0).transform.localPosition = attackPosition;
            }
            if (gameObject.name == gameControl.playerName.HeXiangu.ToString())
            {
                // target pos
                Vector3 originalPosition = target.transform.localPosition;
                Vector3 newPosition = new Vector3(-0.77f, originalPosition.y, originalPosition.z);
                target.transform.localPosition = newPosition;
            }
        }
        if (dir == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            if (gameObject.name == gameControl.playerName.LvDongbin.ToString())
            {
                // flip attack range
                Vector3 attackPosition = gameObject.transform.GetChild(0).localPosition;
                attackPosition.x = -0.9f;
                gameObject.transform.GetChild(0).transform.localPosition = attackPosition;
            }
            if (gameObject.name == gameControl.playerName.HeXiangu.ToString())
            {
                Debug.Log("1");
                // target pos
                Vector3 originalPosition = target.transform.localPosition;
                // flip target pos
                Vector3 newPosition = new Vector3(0.77f, originalPosition.y, originalPosition.z);
                target.transform.localPosition = newPosition;
            }
        }
        dir = direction;
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            playerBody.velocity = new Vector2(playerBody.velocity.x, jumpHight);
            anim.SetBool("Jump", true);
            // jump la, dont run!
            anim.SetBool("Run", false);
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

    private void UpdatePlayerPosition() 
    {
        player.position = gameObject.transform.position;
    }
}
