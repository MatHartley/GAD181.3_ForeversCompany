using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClickHandler : MonoBehaviour
{
    public HealthManager HealthManager;
    private float damage = 10f;

    public SpriteRandomizer spriteRandomizer;
    private Image triggerImage;

    public GameObject patternDoor;

    private bool canPick;

    private bool playerInside;
    private float timeInside;

    private bool hasInteracted;

    public GameObject incorrect;
    public GameObject correct;

    void Start()
    {
        triggerImage = GetComponent<Image>();

        if (triggerImage == null)
        {
            Debug.LogError("Image component not found on the button!");
        }

        canPick = true;

        hasInteracted = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playerInside = true;
            timeInside = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            playerInside = false;
        }
    }

    private void Update()
    {
        if (playerInside)
        {
            timeInside += Time.deltaTime;

            if (timeInside >= 3f && !hasInteracted)
            {
                if (spriteRandomizer != null && triggerImage != null)
                {
                    if (spriteRandomizer.IsCorrectSprite(triggerImage.sprite) && canPick)
                    {
                        StartCoroutine(DelayedCorrect());
                        hasInteracted = true;
                    }
                    else
                    {
                        StartCoroutine(DelayedWrong());
                        hasInteracted = true;
                    }
                }
                else
                {
                    Debug.LogError("SpriteRandomizer or buttonImage is null!");
                }
            }
        }
    }
    private IEnumerator DelayedCorrect()
    {
        correct.SetActive(true);
        canPick = false;
        Debug.Log("Correct button clicked!");

        patternDoor.SetActive(false);

        yield return new WaitForSeconds(2f);
        correct.SetActive(false);
        canPick = true;
        hasInteracted = false;
    }

    private IEnumerator DelayedWrong()
    {
        incorrect.SetActive(true);
        canPick = false;
        Debug.Log("Wrong button clicked!");
        HealthManager.currentHealth -= damage;

        yield return new WaitForSeconds(2f);
        incorrect.SetActive(false);
        canPick = true;
        hasInteracted = false;
    }
}
