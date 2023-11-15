using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerOne;
    public Transform playerTwo;
    public float playerDistance;
    public HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector2.Distance(playerOne.transform.position, playerTwo.transform.position);

        if (playerDistance > 10)
        {
            DegenHealth();
        }
        else if (playerDistance <2)
        {
            RegenHealth();
        }
    }

    private void DegenHealth()
    {
        healthManager.DecreaseHealth();
        StartCoroutine(WaitForTime(5));
    }

    private void RegenHealth()
    {
        healthManager.IncreaseHealth();
        StartCoroutine(WaitForTime(5));
    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}