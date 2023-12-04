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
    public Transform playerOneTransform;
    public Transform playerTwoTransform;
    public GameObject deathPanel;
    public TextMeshProUGUI deathText;
    public SceneLoader sceneLoader;

    [Header("Materials")]
    public Material playerOneMat;
    public Material playerTwoMat;
    public Material safeguardMat;
    public Material regenMat;
    public Material damageMat;

    [Header("SFX")]
    public AudioSource damageSFX;

    [Header("Health Variables")]
    [Range(0, 100)]
    public float currentHealth;
    public float maxHealth = 100;
    public float playerDistance;
    public bool playerOneImmune;
    public bool playerTwoImmune;

    [Header("Private Health Variables")]
    private float healthDegenRate = 1.0f;
    private float healthRegenRate = 5.0f;
    private bool safeguardActive = false;
    private bool takingDamage = false;
    private int closeRange = 5;

    public LineRenderer lineRenderer;

    /// <summary>
    /// Start sets the current health to the maximum health
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;

        lineRenderer = GetComponent<LineRenderer>();
    }

    /// <summary>
    /// Update checks the distance between players and applies regel/degen based on distance.
    /// </summary>
    void Update()
    {
        lineRenderer.SetPosition(0, playerOneTransform.position);
        lineRenderer.SetPosition(1, playerTwoTransform.position);

        //checks if the players exist
        if (playerOne != null && playerTwo != null)
        {
            //records how far apart the two players are
            playerDistance = Vector2.Distance(playerOne.transform.position, playerTwo.transform.position);

            //if they are too far apart they start to loose health...
            if (playerDistance > closeRange)
            {
                DegenHealth();

                //lineRenderer.enabled = false;

                if (!safeguardActive && !takingDamage)
                {
                    playerOne.GetComponent<SpriteRenderer>().material = playerOneMat;
                    playerTwo.GetComponent<SpriteRenderer>().material = playerTwoMat;
                    playerOneImmune = false;
                    playerTwoImmune = false;
                }
            }
            //..they regenerate it when they get very close together.
            else if (playerDistance < closeRange)
            {
                RegenHealth();
                if (currentHealth < 100)
                    playerOneImmune = true;
                playerTwoImmune = true;

                //lineRenderer.enabled = true;
            }
        }

        if (playerOne == null && playerTwo == null)
        {
            Color panelColor = deathPanel.GetComponent<Image>().color;
            Color textColor = deathText.color;

            panelColor.a += 0.5f * Time.deltaTime;
            textColor.a += 0.5f * Time.deltaTime;

            deathPanel.GetComponent<Image>().color = panelColor;
            deathText.color = textColor;

            Debug.Log("Calling GameOver");
            StartCoroutine(GameOver());
        }
    }

    /// <summary>
    /// ActivateSafeguard sets the immunity status of one player based on the actions of the other
    /// </summary>
    public void ActivateSafeguard(GameObject playerTrigger)
    {
        if (playerDistance > closeRange)
        {
            if (playerTrigger.name == "Player1")
            {
                playerTwo.GetComponent<SpriteRenderer>().material = safeguardMat;
                playerTwoImmune = true;
                safeguardActive = true;
            }
            else if (playerTrigger.name == "Player2")
            {
                playerOne.GetComponent<SpriteRenderer>().material = safeguardMat;
                playerOneImmune = true;
                safeguardActive = true;
            }
        }
    }

    /// <summary>
    /// ActivateSafeguard sets the immunity status of one player based on the actions of the other
    /// </summary>
    public void RemoveSafeguard(GameObject playerTrigger)
    {
        playerOne.GetComponent<SpriteRenderer>().material = playerOneMat;
        playerTwo.GetComponent<SpriteRenderer>().material = playerTwoMat;
        playerOneImmune = false;
        playerTwoImmune = false;
        safeguardActive = false;
    }

    /// <summary>
    /// TakeDamage applies one-hit damage to the players' health pool, checks if that damage kills them, and updates the health display
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage, GameObject reciever)
    {
        if ((reciever.name == "Player1" && !playerOneImmune) || (reciever.name == "Player2" && !playerTwoImmune))
        {
            StartCoroutine(DamageEffect(reciever));
            currentHealth -= damage;
            damageSFX.Play();
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
            playerOne.GetComponent<SpriteRenderer>().material = regenMat;
            playerTwo.GetComponent<SpriteRenderer>().material = regenMat;
            currentHealth += (healthRegenRate * Time.deltaTime);
            DisplayHealth();
        }
        else
        {
            playerOne.GetComponent<SpriteRenderer>().material = safeguardMat;
            playerTwo.GetComponent<SpriteRenderer>().material = safeguardMat;
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

    public void ReelIn(float reelSpeed)
    {
        playerOneTransform.position = Vector2.MoveTowards(playerOneTransform.position, playerTwoTransform.position, reelSpeed * Time.deltaTime);
        playerTwoTransform.position = Vector2.MoveTowards(playerTwoTransform.position, playerOneTransform.position, reelSpeed * Time.deltaTime);
    }

    IEnumerator DamageEffect(GameObject reciever)
    {
        takingDamage = true;
        if (reciever.name == "Player1")
        {
            Debug.Log("Player 1 hit");
            playerOne.GetComponent<SpriteRenderer>().material = damageMat;
        }
        else
        {
            playerTwo.GetComponent<SpriteRenderer>().material = damageMat;
        }

        yield return new WaitForSeconds(0.2f);

        if (reciever.name == "Player1")
        {
            Debug.Log("Player 1 mat return");
            playerOne.GetComponent<SpriteRenderer>().material = playerOneMat;
        }
        else
        {
            playerTwo.GetComponent<SpriteRenderer>().material = playerTwoMat;
        }
        takingDamage = false;
    }

    IEnumerator GameOver()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds(5f);
        Debug.Log("Loading Game Over Menu");
        sceneLoader.LoadGameOverMenu();
    }
}