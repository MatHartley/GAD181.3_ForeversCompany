using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    [Header("Game Objects")]
    public TextMeshProUGUI valueText;
    public Slider rightSlider;
    public Slider leftSlider;
    public GameObject playerOne;
    public GameObject playerTwo;

    [Header("Health Variables")]
    [Range(0,100)]
    public float currentHealth;
    public float maxHealth = 100;
    public float playerDistance;
    public bool playerOneImmune;
    public bool playerTwoImmune;

    [Header("Private Health Variables")]
    private float healthDegenRate = 1.0f;
    private float healthRegenRate = 3.0f;
    private bool safeguardActive = false;
    private int closeRange = 10;

    /// <summary>
    /// Start sets the current health to the maximum health
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Update checks the distance between players and applies regel/degen based on distance.
    /// </summary>
    void Update()
    {
        //checks if the players exist
        if (playerOne != null && playerTwo != null)
        {
            //records how far apart the two players are
            playerDistance = Vector2.Distance(playerOne.transform.position, playerTwo.transform.position);

            //if they are too far apart they start to loose health...
            if (playerDistance > closeRange)
            {
                DegenHealth();
                if (!safeguardActive)
                {
                    playerOneImmune = false;
                    playerTwoImmune = false;
                }
            }
            //..they regenerate it when they get very close together.
            else if (playerDistance < closeRange)
            {
                RegenHealth();
                playerOneImmune = true;
                playerTwoImmune = true;
            }

        }
    }

    /// <summary>
    /// ActivateSafeguard sets the immunity status of one player based on the actions of the other
    /// </summary>
    public void ActivateSafeguard(GameObject playerTrigger)
    {
        if (playerTrigger.name == "Player1")
        {
            playerTwoImmune = true;
            safeguardActive = true;
        }
        else if (playerTrigger.name == "Player2")
        {
            playerOneImmune = true;
            safeguardActive = true;
        }
    }

    /// <summary>
    /// ActivateSafeguard sets the immunity status of one player based on the actions of the other
    /// </summary>
    public void RemoveSafeguard(GameObject playerTrigger)
    {
        if (playerDistance > closeRange)
        {
            playerTwoImmune = false;
            playerOneImmune = false;
            safeguardActive = false;
        }
    }

    /// <summary>
    /// TakeDamage applies one-hit damage to the players' health pool, checks if that damage kills them, and updates the health display
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage, GameObject reciever)
    {
        if ((reciever.name == "Player1" && !playerOneImmune) || (reciever.name == "Player2" && !playerTwoImmune))
        {
            currentHealth -= damage;
            CheckDeath();
            DisplayHealth();
        } 
    }

    /// <summary>
    ///DegenHealth deals damage over time at the healthDegenRate 
    /// </summary>
    public void DegenHealth()
    {
        if (currentHealth > 0)
        {
            currentHealth -= (healthDegenRate * Time.deltaTime);
            CheckDeath();
            DisplayHealth();
        }
    }

    /// <summary>
    /// RegenHealth recovers health over time at the healthRegenRate
    /// </summary>
    public void RegenHealth()
    {
        if (currentHealth < 100)
        {
            currentHealth += (healthRegenRate * Time.deltaTime);
            DisplayHealth();
        }
    }

    /// <summary>
    /// CheckDeath checks to see if the health pool has reached zero, destroying the players if so
    /// </summary>
     void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            DisplayHealth();
            Destroy(playerOne);
            Destroy(playerTwo);
        }
    }

    /// <summary>
    /// DisplayHealth updates the sliders and text display to show the current state of the players' health pool
    /// </summary>
    public void DisplayHealth()
    {
        leftSlider.value = currentHealth;
        rightSlider.value = currentHealth;
        valueText.text = currentHealth.ToString("F0");
    }
}
