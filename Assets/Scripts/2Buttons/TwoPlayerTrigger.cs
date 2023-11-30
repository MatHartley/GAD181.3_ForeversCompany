using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoPlayerTrigger : MonoBehaviour
{
    private bool player1Inside = false;
    private bool player2Inside = false;

    public GameObject door1;
    public GameObject door2;

    // Update is called once per frame
    private void Update()
    {
        if (player1Inside && player2Inside)
        {
            if (Input.GetKeyDown(KeyCode.E) && Input.GetKeyDown(KeyCode.Keypad7))
            {
                OpenDoors();
            }
        }
    }

    // OnTriggerEnter2D is called when a collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Inside = true;
        }
        else if (other.CompareTag("Player2"))
        {
            player2Inside = true;
        }
    }

    // OnTriggerExit2D is called when a collider exits the trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1"))
        {
            player1Inside = false;
        }
        else if (other.CompareTag("Player2"))
        {
            player2Inside = false;
        }
    }

    // Perform the action when both players press their keys
    private void OpenDoors()
    {
        // Replace this with the action you want to perform
        Debug.Log("Both players pressed their keys inside their respective trigger zones. Performing action!");
        door1.SetActive(false);
        door2.SetActive(false);
    }
}
