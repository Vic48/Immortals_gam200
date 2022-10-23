using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [HideInInspector] public bool mustPatrol;
    [HideInInspector] public bool mustTurn;

    public Rigidbody2D enemyBody;
    public Transform groundCheck;
    public LayerMask groundPosLayer;
    public Transform player, shootPos;
    public GameObject bullet;

    public float shootRate = 10f;
    float nextAttackTime = 0f;
    private float timePass = 0f;

    public float walkSpeed, range, timeBTWShots, shootSpeed;
    private float distToPlayer;
    void Shoot(int dir)
    {
        //yield return new WaitForSeconds(timeBTWShots);
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newBullet.GetComponent<Bullet>().isenemy = true;
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(newBullet.GetComponent<Bullet>().speed * dir * Time.fixedDeltaTime, 0);

    }

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
            // Face the player
            if (player.position.x > transform.position.x && transform.localScale.x < 0
                || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }
            //No shooting when patrolling 
            mustPatrol = false;
            enemyBody.velocity = Vector2.zero;
            if (timePass <= 0)
            {
                if ((player.position.x - transform.position.x) > 0)
                {
                    Shoot(1);
                }
                else
                {
                    Shoot(-1);
                }
                timePass = shootRate;
            }
            else
            {
                timePass -= Time.fixedDeltaTime;
            }


        }
        else
        {
            mustPatrol = true;
        }

    }

    private void FixedUpdate()
    {
        if (mustTurn)
        {
            Flip();
        }
        //if must Patrol is true
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundPosLayer);
        }
    }

    void AIPatrol()
    {
        enemyBody.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, enemyBody.velocity.y);
    }
    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
