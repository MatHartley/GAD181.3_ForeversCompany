using UnityEngine;
using UnityEngine.UI;

public class SpriteRandomizer : MonoBehaviour
{
    public Sprite[] sprites; // Array of sprites to randomize
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    void Start()
    {
        // Get the SpriteRenderer component attached to this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Call the function to randomize the sprite
        RandomizeSprite();
    }

    void RandomizeSprite()
    {
        // Check if there are sprites in the array
        if (sprites.Length > 0 && spriteRenderer != null)
        {
            // Get a random index within the array
            int randomIndex = Random.Range(0, sprites.Length);

            // Set the sprite of the SpriteRenderer component to the randomly chosen sprite
            spriteRenderer.sprite = sprites[randomIndex];
        }
        else
        {
            // Log an error if no sprites are available or if the SpriteRenderer component is not found
            Debug.LogError("No sprites assigned or SpriteRenderer component not found!");
        }
    }

    // Method to check if the provided sprite is the correct one
    public bool IsCorrectSprite(Sprite selectedSprite)
    {
        // Check if the provided sprite is the same as the currently set sprite
        return selectedSprite == spriteRenderer.sprite;
    }
}
