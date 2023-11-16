using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public Rigidbody2D rigidBody;
    public float speed = 5f;

    [Header("Knockback")]
    public float knockbackForce;
    public float knockbackCount;
    public float knockbackTime;
    public bool knockFromRight;

    [Header("Scripts")]
    public HealthManager healthManager;

    [Header("Private Variables")]
    private float horizontal;
    private float vertical;
    private bool isFacingRight = true;

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
        else if (isFacingRight && horizontal <0f)
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
    /// Move sets the horizontal and vertical movement directions based on user input
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    /// <summary>
    /// Safeguard allows one player to lock their position to make the other immune to damage
    /// </summary>

    public void Safeguard(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            healthManager.ActivateSafeguard(gameObject);
            speed = 0f;
            Debug.Log(gameObject.name + " activated SAFEGUARD.");
        }

        if (context.canceled)
        {
            healthManager.RemoveSafeguard(gameObject);
            speed = 5f;
            Debug.Log(gameObject.name + " deactivated SAFEGUARD.");
        }
    }
}
