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
    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

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

        if (distToPlayer <= range)
        {
            mustPatrol = false;
            // Face the player
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip(false);
            }

            if (Time.time > nextAttackTime)
            {
                normalAttack();
                //0.5 second cool down time
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        else
        {
            mustPatrol = true;
        }
    }
    void normalAttack()
    {
        //play an attack animation
        anim.SetTrigger("EnemyAttck");
        //Detect enemy in the range attack 
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange
        );
        //Due Damage
        foreach (Collider2D player in hitPlayer)
        {
            if (player.name == gameControl.playerName.LvDongbin.ToString() || player.name == gameControl.playerName.LvDongbin.ToString()) 
            {
                player.transform.parent.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }
    }

    private void FixedUpdate()
    {
        // should turn or hit the wall, turn around
        if (mustPatrol && (mustTurn || bodyCollider.IsTouchingLayers(groundPosLayer)))
        {
            Flip(true);
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
    void Flip(bool isPatrol)
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = isPatrol;
    }

    //draw stuff in editor to check attack range
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
