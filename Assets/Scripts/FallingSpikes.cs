using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpikes : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider;

    public int spikeDamage = 5;
    public float distance;
    public bool isFalling;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(isFalling == false)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);

            if (hit.transform != null)
            {
                if(hit.transform.tag == "Player")
                {
                    rb.gravityScale = 5;
                    isFalling = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.transform.parent.GetComponent<Player>().TakeDamage(spikeDamage);
            // GetComponent<Player>().TakeDamage(spikeDamage);

            // Destroy(gameObject);
            gameObject.SetActive(false);

        }
        else
        {
            rb.gravityScale = 0;
            boxCollider.enabled = false;
        }
    }
}
