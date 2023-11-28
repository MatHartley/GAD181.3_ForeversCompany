using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayer2 : MonoBehaviour
{
    public PlayerController controller;
    public Rigidbody2D playerBeingBlocked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            Debug.Log("Player1 can pass through the wall.");
        }
        else if (other.CompareTag("Player2"))
        {
            Debug.Log("Player2 cannot pass through the wall.");
            controller.speed = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player2"))
        {
            controller.speed = 5;
        }
    }
}