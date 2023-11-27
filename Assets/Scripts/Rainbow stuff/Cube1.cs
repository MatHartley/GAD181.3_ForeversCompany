using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube1 : MonoBehaviour
{
    public List<Material> materials; // List of materials to search through
    public GameObject cubeToColor; // Reference to the cube to be colored
    public float colorChangeDelay = 3f; // Time delay before changing color

    private bool hasBeenColored = false;
    private float lastColorChangeTime;

    private void Start()
    {
        lastColorChangeTime = Time.time;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenColored && other.CompareTag("Player1") || !hasBeenColored && other.CompareTag("Player2"))
        {
            StartCoroutine(WaitForColorChange());
        }
    }

    private IEnumerator WaitForColorChange()
    {
        float elapsedTime = 0f;

        while (elapsedTime < colorChangeDelay)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ChangeColor();
    }

    private void ChangeColor()
    {
        // Find the material that matches the name of the object
        Material matchingMaterial = materials.Find(material => material.name == gameObject.name);

        if (matchingMaterial != null)
        {
            // Apply the material to the other cube
            Renderer cubeRenderer = cubeToColor.GetComponent<Renderer>();
            if (cubeRenderer != null)
            {
                cubeRenderer.material = matchingMaterial;
                Debug.Log("Changed cube color to match: " + matchingMaterial.name);

                hasBeenColored = true; // Mark the cube as colored
                lastColorChangeTime = Time.time; // Update the last color change time
            }
            else
            {
                Debug.LogWarning("CubeToColor does not have a Renderer component.");
            }
        }
        else
        {
            Debug.LogWarning("No matching material found for object: " + gameObject.name);
        }
    }
}