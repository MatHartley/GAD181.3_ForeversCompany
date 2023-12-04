using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
{
    [Header("Damage Variables")]
    public int damage;

    [Header("Target Variables")]
    public GameObject targetPlayer;
    private Transform targetTransform;
    private bool isFacingRight = false;
    public float distanceToTarget;
    public float viewRange;
    private Vector2 spawnPosition;
    public float chaseRange;
    public float speed;
    public bool isChasing = false;

    [Header("Scripts")]
    public HealthManager healthManager;
    private PlayerController playerController;

    [Header("Animation")]
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spawnPosition = GetComponent<Transform>().position;
        targetTransform = targetPlayer.GetComponent<Transform>();
    }

    private void Update()
    {
        if (anim.GetBool("isDead"))
        {
            StartCoroutine(WaitForTime(0.9f));
        }
        else
        {
            distanceToTarget = Vector3.Distance(transform.position, targetTransform.position);

            //Turn toward the target
            if (distanceToTarget < viewRange)
            {
                //Flip the sprite to look in the direction of the target
                if (transform.position.x <= targetTransform.position.x && !isFacingRight)
                {
                    Flip();
                    isFacingRight = true;
                }
                else if (transform.position.x >= targetTransform.position.x && isFacingRight)
                {
                    Flip();
                    isFacingRight = false;
                }
            }

            //Move toward the target
            if (distanceToTarget < chaseRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
            }
            //Move back to the spawn point
            else if (distanceToTarget >= chaseRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, spawnPosition, speed * Time.deltaTime);
                //Flip the sprite to face where it is moving
                if (transform.position.x < spawnPosition.x && !isFacingRight)
                {
                    Flip();
                    isFacingRight = true;
                }
                else if (transform.position.x > spawnPosition.x && isFacingRight)
                {
                    Flip();
                    isFacingRight = false;
                }
                else
                {
                    LookLeft();
                    isFacingRight = false;
                }
            }
        }
    }

    /// <summary>
    /// OnCollisionEnter2D causes the bomb to explode and damage the player if it comes into contact with them
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isDead", true);
            gameObject.GetComponent<Collider2D>().enabled = false;
            playerController = collision.gameObject.GetComponentInChildren<PlayerController>();
            playerController.knockbackCount = playerController.knockbackTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.knockFromRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                playerController.knockFromRight = false;
            }
            if (collision.gameObject == targetPlayer)
            {
                healthManager.TakeDamage(damage, collision.gameObject);
            }
        }
    }

    void Flip()
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1f;
        gameObject.transform.localScale = localScale;
    }

    void LookLeft()
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x = 1f;
        gameObject.transform.localScale = localScale;
    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
