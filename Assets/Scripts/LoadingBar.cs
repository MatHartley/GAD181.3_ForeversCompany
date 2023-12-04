using System.Collections;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    public LoadingBarController loadingBarController;

    void Start()
    {
        StartCoroutine(LoadToHalf());
    }

    IEnumerator LoadToHalf()
    {
        yield return new WaitForSeconds(0.1f);
        loadingBarController.progress = 0.10f;

        yield return new WaitForSeconds(0.1f);
        loadingBarController.progress = 0.20f;

        yield return new WaitForSeconds(0.1f);
        loadingBarController.progress = 0.30f;

        yield return new WaitForSeconds(0.1f);
        loadingBarController.progress = 0.40f;

        yield return new WaitForSeconds(2f);
        loadingBarController.progress = 0.50f;

        yield return new WaitForSeconds(0.1f);
        loadingBarController.progress = 0.60f;

        yield return new WaitForSeconds(2f);
        loadingBarController.progress = 0.70f;

        yield return new WaitForSeconds(0.1f);
        loadingBarController.progress = 0.90f;

        yield return new WaitForSeconds(0.1f);
        loadingBarController.progress = 1f;
    }
}

