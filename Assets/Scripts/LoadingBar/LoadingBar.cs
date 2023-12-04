using System.Collections;
using UnityEngine;

public class YourLoadingLogic : MonoBehaviour
{
    public LoadingBarController loadingBarController;

    void Start()
    {
        StartCoroutine(LoadToHalf());
    }

    IEnumerator LoadToHalf()
    {
        yield return new WaitForSeconds(2f);
        loadingBarController.progress = 0.5f;

        yield return new WaitForSeconds(2f);
        loadingBarController.progress = 1f;
    }
}
