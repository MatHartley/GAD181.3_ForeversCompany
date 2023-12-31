using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameMaster gameMaster;


    void Start()
    {
        //gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGameOverMenu()
    {
        SceneManager.LoadScene("GameOverMenu");
    }

    public void LoadEndGameMenu()
    {
        SceneManager.LoadScene("EndGameMenu");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLast()
    {
        if (gameMaster != null)
        {
            gameMaster.LoadSavedData();
            SceneManager.LoadScene("PlaytestScene");
        }
    }
}
