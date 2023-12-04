using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseScreen;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene("PlaytestScene");
        Time.timeScale = 1.0f;
    }
}
