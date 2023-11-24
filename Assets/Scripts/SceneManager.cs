using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOverMenu()
    {
        SceneManager.LoadScene("GameOverMenu");
    }

    public void LoadPlaytest()
    {
        SceneManager.LoadScene("PlaytestScene");
    }
}
