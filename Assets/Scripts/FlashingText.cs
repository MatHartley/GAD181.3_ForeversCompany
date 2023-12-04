using UnityEngine;
using TMPro;
using System.Collections;

public class FlashingText : MonoBehaviour
{
    public float flashSpeed = 1.0f;

    private TextMeshProUGUI textMeshProComponent;
    private bool isTextVisible = true;

    public TextMeshProUGUI textBox;

    void Start()
    {
        textMeshProComponent = GetComponent<TextMeshProUGUI>();

        StartCoroutine(FlashText());

        textBox.enabled = false;
    }

    IEnumerator FlashText()
    {
        yield return new WaitForSeconds(4.7f);

        textBox.enabled = true;
        while (true)
        {
            isTextVisible = !isTextVisible;

            textMeshProComponent.enabled = isTextVisible;

            yield return new WaitForSeconds(1.0f / flashSpeed);
        }
    }
}
