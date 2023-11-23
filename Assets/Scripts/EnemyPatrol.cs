using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Route")]
    public Transform[] patrolPoints;
    public int patrolDestination;

    [Header("Enemy Variables")]
    public float speed;
    public float chaseSpeed;
    private bool isFacingRight = true;

    [Header("Private Variables")]
    private Animator anim;

    [Header("Targetting Variables")]
    public float chaseRange;
    public float distanceToTarget;
    public Transform target;
    public Transform playerOne;
    public Transform playerTwo;
    public bool isChasing;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    /// <summary>
    /// FixedUpdate moves the enemy back and forth on its patrol route, and causes the enemy to chase a player that falls within range
    /// </summary>
    void FixedUpdate()
    {
        if (anim.GetBool("isDead"))
        {
            speed = 0;
        }
        else if (!anim.GetBool("isAttacking"))
        {
            //Check which player is closer and set them as the target
            if (Vector3.Distance(transform.position, playerOne.position) > Vector3.Distance(transform.position, playerTwo.position))
            {
                target = playerTwo;
            }
            else
            {
                target = playerOne;
            }

            distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (isChasing)
            {
                if (transform.position.x > target.position.x)
                {
                    transform.position += Vector3.left * chaseSpeed * Time.deltaTime;
                    LookLeft();
                }
                if (transform.position.x < target.position.x)
                {
                    transform.position += Vector3.right * chaseSpeed * Time.deltaTime;
                    LookRight();
                }

                if (distanceToTarget > chaseRange)
                {
                    isChasing = false;
                }
            }
            else
            {
                if (distanceToTarget <= chaseRange)
                {
                    isChasing = true;
                }

                //Check the patrol destination of the enemy...
                if (patrolDestination == 0)
                {
                    LookRight();
                    //...move towards it...
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[0].position) < 0.5f)
                    {
                        //...and turn around when it gets there
                        Flip();
                        patrolDestination = 1;
                    }
                }
                if (patrolDestination == 1)
                {
                    LookLeft();
                    transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
                    if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.5f)
                    {
                        Flip();
                        patrolDestination = 0;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Flip changes the facing direction of the sprite
    /// </summary>
    void Flip()
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1f;
        gameObject.transform.localScale = localScale;
        isFacingRight = !isFacingRight;
    }
    void LookLeft()
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x = -1f;
        gameObject.transform.localScale = localScale;
        isFacingRight = false;
    }

    void LookRight()
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x = 1f;
        gameObject.transform.localScale = localScale;
        isFacingRight = true;
    }

}
