using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // animation related
    public Animator anim;

    // health related
    public HealthBar EnemyHB;
    public int maxHealth = 100;
    public int currentHealth;

    // dead related
    public bool isDead = false;
    Vector3 deadPosition;
    [SerializeField]
    public float deadBodyDestroyTime;

    //attack related
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public Transform attackPoint;

    // movement related
    public EnemyMovement enemyMovement;

    void Start()
    {
        currentHealth = maxHealth;
        EnemyHB.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (isDead)
        {
            // Do not change position after die
            this.gameObject.transform.position = deadPosition;
        }
        else if (Time.time > nextAttackTime && !enemyMovement.mustPatrol)
        {
            normalAttack();
            // 0.5 second cool down time
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EnemyHB.SetHealth(currentHealth);

        Debug.Log("HERE");
        //play hurt animation
        anim.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        this.isDead = true;

        //die animation
        anim.SetBool("IsDead", true);

        //Disable the enemy collision
        GetComponent<Collider2D>().enabled = false;

        // record dead position
        this.deadPosition = this.gameObject.transform.position;


        // Destry dead body
        StartCoroutine(enemyDieDestory());
    }

    IEnumerator enemyDieDestory()
    {
        yield return new WaitForSeconds(deadBodyDestroyTime);
        // Destroy enemy dead body after 3s
        this.gameObject.SetActive(false);
        this.enabled = false;
        Destroy(this);
    }
    void normalAttack()
    {
        //play an attack animation
        anim.SetTrigger("Attack");
        //Detect enemy in the range attack 
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange
        );
        //Due Damage
        foreach (Collider2D player in hitPlayer)
        {
            if (player.name == gameControl.playerName.LvDongbin.ToString() || player.name == gameControl.playerName.HeXiangu.ToString())
            {
                player.transform.parent.GetComponent<Player>().TakeDamage(attackDamage);
            }
        }
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
