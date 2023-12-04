using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadToWorld2 : MonoBehaviour
{
    public GameMaster gameMaster;

    public GameObject everthing;
    public GameObject Loading;

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
        yield return new WaitForSeconds(2f);
        Loading.SetActive(true);
        everthing.SetActive(false);
    }
}
