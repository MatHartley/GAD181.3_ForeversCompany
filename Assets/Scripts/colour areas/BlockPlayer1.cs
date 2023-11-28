using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayer1 : MonoBehaviour
{
    public GameObject player2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Player2"))
        {
            Debug.Log("Player2 can pass through the wall.");

            Collider2D player2Collider = player2.GetComponent<Collider2D>();
            if (player2Collider != null)
            {
                player2Collider.enabled = true;
            }
        }
        else if (CompareTag("Player1"))
        {
            Debug.Log("Player1 cannot pass through the wall.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        {
            if (CompareTag("Player2"))
            {
                Collider2D player2Collider = player2.GetComponent<Collider2D>();
                if (player2Collider != null)
                {
                    player2Collider.enabled = false;
                }
            }
        }
    }
}