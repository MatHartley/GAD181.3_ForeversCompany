using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BlockPlayer2 : MonoBehaviour
{
    public GameObject player1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CompareTag("Player1"))
        {
            Debug.Log("Player1 can pass through the wall.");

            Collider2D player1Collider = player1.GetComponent<BoxCollider2D>();
            if (player1Collider != null)
            {
                player1Collider.isTrigger = true;
            }
        }
        else if (CompareTag("Player2"))
        {
            Debug.Log("Player2 cannot pass through the wall.");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        {
            if (CompareTag("Player1"))
            {
                Collider2D player1Collider = player1.GetComponent<BoxCollider2D>();
                if (player1Collider != null)
                {
                    player1Collider.isTrigger = false;
                }
            }
        }
    }
}