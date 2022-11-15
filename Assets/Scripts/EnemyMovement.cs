using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public bool mustPatrol;
    [HideInInspector] public bool mustTurn = false;

    public Collider2D bodyCollider;
    public Animator anim;
    public Rigidbody2D enemyBody;
    public Transform groundCheck;
    public LayerMask groundPosLayer;
    public Player player;
    public float walkSpeed, range;
    private float distToPlayer;

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
        //cal dist between enemy and player
        distToPlayer = Vector2.Distance(transform.position, player.position);
        bool isDead = gameControl.Instance.getIsPlayerDead();

        // in the range and player not dead
        if (distToPlayer <= range && !isDead)
        {
            mustPatrol = false;
            // Face the player
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
        }
        else
        {
            mustPatrol = true;
        }
    }

    private void FixedUpdate()
    {
        // should turn or hit the wall, turn around
        if (mustPatrol && (mustTurn || bodyCollider.IsTouchingLayers(groundPosLayer)))
        {
            // before flip, disable patrol
            mustPatrol = false;
            Flip();
            mustPatrol = true;
        }
        //if must Patrol is true
        if (mustPatrol)
        {
            // if on the ground do not turn
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundPosLayer);
        }
    }

    void AIPatrol()
    {
        enemyBody.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, enemyBody.velocity.y);
    }
    void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
    }
}

