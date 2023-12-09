using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public Rigidbody2D rigidBody;
    public Animator anim;
    public float speed;
    public float originSpeed;
    public float reelSpeed;
    public bool isFacingRight = true;
    public bool reelingIn = false;

    [Header("Special")]
    public float boostSpeed;
    public float specialTime;
    public float specialCount;
    public float cooldownTime;
    public float cooldownCount;
    public bool isFast = false;
    public bool isEthereal = false;
    public bool specialReady = true;
    public GameObject specialDisplay;

    [Header("Materials")]
    public Material standardMat;
    public Material specialMat;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockbackCount;
    public float knockbackTime;
    public bool knockFromRight;

    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float shootTime;
    public float shootCount;

    [Header("Scripts")]
    public HealthManager healthManager;

    [Header("SFX")]
    public AudioSource shotSFX;
    public AudioSource specialSFX;
    public AudioSource specialUpSFX;
    public AudioSource safeguardSFX;

    [Header("Private Variables")]
    private float horizontal;
    private float vertical;

    void Start()
    {
        shootCount = shootTime;
        originSpeed = speed;
    }

    void Update()
    {
        shootCount -= Time.deltaTime;
        specialCount -= Time.deltaTime;

        //Reel In ability
        if (reelingIn)
        {
            healthManager.ReelIn(reelSpeed);
        }

        //Speed boost special ability
        if (isFast)
        {
            speed = boostSpeed;
            gameObject.GetComponent<SpriteRenderer>().material = specialMat;

            if (specialCount <= 0)
            {
                isFast = false;
                speed = originSpeed;
            }
        }
        //Ethereal special ability
        if (isEthereal)
        {
            gameObject.GetComponent<SpriteRenderer>().material = specialMat;
            Color etherealColor = gameObject.GetComponent<SpriteRenderer>().color;
            etherealColor.a = 0f;
            gameObject.GetComponent<SpriteRenderer>().color = etherealColor;
            gameObject.GetComponent<Collider2D>().enabled = false;

            if (specialCount <= 0)
            {
                Color normalColor = gameObject.GetComponent<SpriteRenderer>().color;
                normalColor.a = 1f;
                gameObject.GetComponent<SpriteRenderer>().color = normalColor;
                isEthereal = false;
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
        }

        if (!specialReady)
        {
            cooldownCount -= Time.deltaTime;
            if (cooldownCount <= 0)
            {
                specialReady = true;
                specialUpSFX.Play();
                if (gameObject.name == "Player1")
                {
                    Color specialColor = specialDisplay.gameObject.GetComponent<Image>().color;
                    specialColor.r = 0; specialColor.g = 255; specialColor.b = 255;
                    specialDisplay.gameObject.GetComponent<Image>().color = specialColor;
                }
                else if (gameObject.name == "Player2")
                {
                    Color specialColor = specialDisplay.gameObject.GetComponent<Image>().color;
                    specialColor.r = 255; specialColor.g = 255; specialColor.b = 0;
                    specialDisplay.gameObject.GetComponent<Image>().color = specialColor;
                }
            }
        }
    }

    /// <summary>
    /// FixedUpdate sets the velocity of the player's rigid body and checks the facing of the sprite
    /// </summary>
    void FixedUpdate()
    {
        if (knockbackCount <= 0)
        {
            //Sets the player's movement velocity based on the speed
            rigidBody.velocity = new Vector2(horizontal * speed, vertical * speed);
        }
        else
        {
            if (knockFromRight)
            {
                rigidBody.velocity = new Vector2(-knockbackForce, knockbackForce);
            }
            if (!knockFromRight)
            {
                rigidBody.velocity = new Vector2(knockbackForce, knockbackForce);
            }

            knockbackCount -= Time.deltaTime;
        }

        //Calls flip based on the player's movement direction and current facing
        if (!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    /// <summary>
    /// Flip changes the scale of the sprite's x value to -1, flipping it
    /// </summary>
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1f;
        gameObject.transform.localScale = localScale;
    }

    /// <summary>
    /// GoEthereal sets the cooldown for player 1's special, activates it, deactivated the indicator, and plays a sound effect
    /// </summary>
    private void GoEthereal()
    {
        specialCount = specialTime;
        isEthereal = true;
        SpecialColourBlack();
        specialSFX.Play();
    }

    /// <summary>
    /// GoFast sets the cooldown for player 2's special, activates it, deactivated the indicator, and plays a sound effect
    /// </summary>
    private void GoFast()
    {
        specialCount = specialTime;
        isFast = true;
        SpecialColourBlack();
        specialSFX.Play();
    }

    /// <summary>
    /// SpecialColourBlack sets the colour of the used ability's indicator to black, showing it has been used
    /// </summary>
    void SpecialColourBlack()
    {
        Color specialColor = specialDisplay.gameObject.GetComponent<Image>().color;
        specialColor.r = 0; specialColor.g = 0; specialColor.b = 0;
        specialDisplay.gameObject.GetComponent<Image>().color = specialColor;
    }

    /// <summary>
    /// Move sets the horizontal and vertical movement directions based on user input
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {

        anim.SetBool("isMoving", true);
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;

        if (context.canceled)
        {
            anim.SetBool("isMoving", false);
        }
    }

    /// <summary>
    /// Safeguard allows one player to lock their position to make the other immune to damage
    /// </summary>
    public void Safeguard(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            safeguardSFX.Play();
            anim.SetBool("isGuarding", true);
            healthManager.ActivateSafeguard(gameObject);
            speed = 0f;
            Debug.Log(gameObject.name + " activated SAFEGUARD.");
        }

        if (context.canceled)
        {
            safeguardSFX.Stop();
            anim.SetBool("isGuarding", false);
            healthManager.RemoveSafeguard(gameObject);
            speed = originSpeed;
            Debug.Log(gameObject.name + " deactivated SAFEGUARD.");
        }
    }

    /// <summary>
    /// Fire causes the player to shoot a projectile 
    /// </summary>
    /// <param name="context"></param>
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed && shootCount <= 0)
        {
            anim.SetBool("isAttacking", true);
            projectilePrefab.GetComponent<PlayerProjectile>().SetSpeed(isFacingRight);
            Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);
            shootCount = shootTime;
            shotSFX.Play();
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    /// <summary>
    /// Interact allows players to activate buttons and solve puzzles - not used - puzzles were scripted by Matt.G to use the Input Manager
    /// </summary>
    /// <param name="context"></param>
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

    /// <summary>
    /// Special activates a players unique special ability
    /// </summary>
    public void Special(InputAction.CallbackContext context)
    {
        if (context.performed && gameObject.name == "Player1" && specialReady)
        {
            GoEthereal();
            specialReady = false;
            cooldownCount = cooldownTime;
        }
        else if (context.performed && gameObject.name == "Player2" && specialReady)
        {
            GoFast();
            specialReady = false;
            cooldownCount = cooldownTime;
        }
    }

    /// <summary>
    /// ReelIn activates the reelingIn boolean, which is used to call the ReelIn function from the Health Manager
    /// </summary>
    public void ReelIn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            reelingIn = true;
        }
        else
        {
            reelingIn = false;
        }
    }
}