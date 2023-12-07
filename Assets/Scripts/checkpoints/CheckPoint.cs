using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [Header("VFX")]
    public GameObject skullLight;

    [Header("SFX")]
    public AudioSource checkpointSFX;

    [Header("Private")]
    private GameMaster gm;
    private bool isActive = false;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            if (!isActive)
            {
                gm.lastCheckPointPos = transform.position;
                skullLight.SetActive(true);
                checkpointSFX.Play();
                gm.SaveData();
                isActive = true;
            }
            else
            {
                gm.lastCheckPointPos = transform.position;
                gm.SaveData();
            }
        }
    }
}
