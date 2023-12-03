using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContextText : MonoBehaviour
{
    public GameObject contextText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            contextText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            contextText.SetActive(false);
        }
    }
}
