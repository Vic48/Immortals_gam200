using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private int damage = 40;
    public Rigidbody2D rb;
    public bool isenemy = false;
    void Start()
    {
        //RB move right * speed
        //rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        //if have enemy
        if (enemy != null && !isenemy)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}
