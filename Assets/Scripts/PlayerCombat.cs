using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public float attackRate = 2f;
    private float nextAttackTime = 0f;

    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            //normal attck
            if (Input.GetKeyDown(KeyCode.Q))
            {
                normalAttack();
                //0.5 second cool down time
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void normalAttack()
    {
        //play an attack animation
        anim.SetTrigger("NormalAttack");
        //Detect enemy in the range attack 
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange,
            enemyLayers
        );
        //Due Damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
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