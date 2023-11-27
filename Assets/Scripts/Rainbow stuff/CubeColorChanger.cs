using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    public Material defaultMaterial;
    private Renderer cubeRenderer;
    private bool isColored = false;

    private void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        if (cubeRenderer == null)
        {
            Debug.LogError("Renderer component not found on this object.");
        }

        // Set the default material/color for each cube
        cubeRenderer.material = defaultMaterial;
    }

    public void ChangeColor(Material newMaterial)
    {
        if (!isColored)
        {
            // Apply the new material to the cube
            cubeRenderer.material = newMaterial;
            isColored = true; // Mark the cube as colored
        }
    }

    // You may want to add a method to reset the color if needed
    // public void ResetColor()
    // {
    //     cubeRenderer.material = defaultMaterial;
    //     isColored = false;
    // }
}
