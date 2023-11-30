using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
            gameObject.GetComponent<Collider2D>().enabled = false;

            if (specialCount <= 0)
            {
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

    private void GoEthereal()
    {
        specialCount = specialTime;
        isEthereal = true;
    }

    private void GoFast()
    {
        specialCount = specialTime;
        isFast = true;
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
            anim.SetBool("isGuarding", true);
            healthManager.ActivateSafeguard(gameObject);
            speed = 0f;
            Debug.Log(gameObject.name + " activated SAFEGUARD.");
        }

        if (context.canceled)
        {
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
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }
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