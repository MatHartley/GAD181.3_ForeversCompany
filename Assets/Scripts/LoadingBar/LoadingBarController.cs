using UnityEngine;
using UnityEngine.UI;

public class LoadingBarController : MonoBehaviour
{
    public Image loadingBar;

    // Adjust this value to set the progress (0.0 to 1.0).
    public float progress = 0.0f;

    void Update()
    {
        // Clamp progress between 0 and 1.
        progress = Mathf.Clamp01(progress);

        // Update the fill amount of the loading bar.
        loadingBar.fillAmount = progress;
    }
}
