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

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        EnemyHB.SetHealth(currentHealth);

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
}
