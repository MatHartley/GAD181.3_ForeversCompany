using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadToWorld2 : MonoBehaviour
{
    public GameMaster gameMaster;

    private void Start()
    {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            EndWorld();
        }
    }

    void EndWorld()
    {
        StartCoroutine(LoadWorld2());
    }

    IEnumerator LoadWorld2()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("World2Building");
    }
}