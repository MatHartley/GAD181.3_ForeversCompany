using UnityEngine;
using UnityEngine.UI;

public class ButtonClickHandler : MonoBehaviour
{
    public SpriteRandomizer spriteRandomizer; // Reference to the SpriteRandomizer script
    private Image buttonImage; // Reference to the Image component of the button

    void Start()
    {
        // Get the Image component of the button
        buttonImage = GetComponent<Image>();
    }

    public void OnButtonClick()
    {
        // Check if spriteRandomizer is not null and buttonImage is not null
        if (spriteRandomizer != null && buttonImage != null)
        {
            // Check if the clicked button's sprite matches the current random sprite
            if (spriteRandomizer.IsCorrectSprite(buttonImage.sprite))
            {
                Debug.Log("Correct button clicked!");
                // Do something when the correct button is clicked
            }
            else
            {
                Debug.Log("Incorrect button clicked!");
                // Do something when an incorrect button is clicked
            }
        }
        else
        {
            Debug.LogError("SpriteRandomizer or buttonImage is null!");
        }
    }
}
