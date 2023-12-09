using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEndScript : MonoBehaviour
{
    [Header("Logic")]
    public bool endTriggered = false;

    [Header("Game Objects")]
    public GameObject demoPanel;
    public TextMeshProUGUI congratsText;
    public TextMeshProUGUI demoText;

    [Header("Scripts")]
    public SceneLoader sceneLoader;


    private void Update()
    {
        if (endTriggered)
        {
            Color panelColor = demoPanel.GetComponent<Image>().color;
            Color congratsColor = congratsText.color;
            Color demoColor = demoText.color;

            panelColor.a += 0.5f * Time.deltaTime;
            congratsColor.a += 0.5f * Time.deltaTime;
            demoColor.a += 0.5f * Time.deltaTime;

            demoPanel.GetComponent<Image>().color = panelColor;
            congratsText.color = congratsColor;
            demoText.color = demoColor;

            Debug.Log("Calling EndGame");
            StartCoroutine(EndGame());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        endTriggered = true;
    }

    IEnumerator EndGame()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(5f);
        Debug.Log("Loading Game Over Menu");
        sceneLoader.LoadEndGameMenu();
    }
}