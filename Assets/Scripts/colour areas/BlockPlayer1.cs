using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayer1 : MonoBehaviour
{
    private Collider2D myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            myCollider.isTrigger = !myCollider.isTrigger;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player1"))
        {
            myCollider.isTrigger = !myCollider.isTrigger;
        }
    }
}