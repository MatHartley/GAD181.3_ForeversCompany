using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingBarController : MonoBehaviour
{
    public Image loadingBar;

    public float progress = 0.0f;

    void Update()
    {
        progress = Mathf.Clamp01(progress);

        loadingBar.fillAmount = progress;

        if (progress == 1f)
        {
            SceneManager.LoadScene("World2Building");
        }
    }
}