using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowRand3 : MonoBehaviour
{
    public List<GameObject> colorObjects; // List of game objects representing different colors

    void Start()
    {
        // Ensure the list is not null and contains at least one object
        if (colorObjects == null || colorObjects.Count == 0)
        {
            Debug.LogError("Color objects list is not assigned or is empty!");
            return;
        }

        // Activate a random color object from the list
        ActivateRandomColor();
    }

    void ActivateRandomColor()
    {
        // Choose a random index within the range of the list
        int randomIndex = Random.Range(0, colorObjects.Count);

        // Activate the chosen color object
        GameObject chosenColor = colorObjects[randomIndex];

        if (chosenColor != null)
        {
            chosenColor.SetActive(true);
            Debug.Log("RaindbowRand3 colour is " + chosenColor);
        }
        else
        {
            Debug.LogWarning("Chosen color object is null.");
        }
    }
}