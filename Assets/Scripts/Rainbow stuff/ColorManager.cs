using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public List<Material> colors; // List of materials to apply to the cubes
    public float colorChangeDelay = 3f; // Time delay before changing color
    private int currentColorIndex = 0;
    private float lastColorChangeTime;

    // Reference to the CubeColorChanger script attached to each cube
    public List<CubeColorChanger> cubes;

    private Material selectedColor; // Color selected by the player

    void Start()
    {
        // Initialize the last color change time
        lastColorChangeTime = Time.time;
    }

    void Update()
    {
        // Check if enough time has passed since the last color change
        if (Time.time - lastColorChangeTime >= colorChangeDelay)
        {
            // Change the color of the current cube if a color is selected
            if (selectedColor != null)
            {
                cubes[currentColorIndex].ChangeColor(selectedColor);
                selectedColor = null; // Reset the selected color
                currentColorIndex = (currentColorIndex + 1) % cubes.Count; // Move to the next cube
                lastColorChangeTime = Time.time; // Update the last color change time
            }
        }
    }

    // Player selects a color
    public void SelectColor(Material color)
    {
        selectedColor = color;
    }
}
