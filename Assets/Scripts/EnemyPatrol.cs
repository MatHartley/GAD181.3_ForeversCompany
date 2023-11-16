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

    [Header("Private Variables")]
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isRunning", true);
    }

    /// <summary>
    /// Update moves the enemy back and forth on its patrol route, and causes the enemy to chase a player that falls within range
    /// </summary>
    void Update()
    {

        //Check the patrol destination of the enemy...
        if (patrolDestination == 0)
        {
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
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < 0.5f)
            {
                Flip();
                patrolDestination = 0;
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
    }
}
