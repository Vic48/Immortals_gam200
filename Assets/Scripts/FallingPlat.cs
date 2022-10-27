using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlat : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    private float fallDelay = 1f;
    private float destoryDelay = 2f;

    //check if anything collied with the plat
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        // Destory Plat after sometime
        Destroy(gameObject, destoryDelay);
    }
}
