using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeXiangu : MonoBehaviour
{
    public Vector2 direction;
    //public GameObject Arrow;
    public PlayerMovement playerMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coins"))
        {
            Destroy(other.gameObject);
        }
    }
}