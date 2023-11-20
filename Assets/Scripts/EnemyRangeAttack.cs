using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [Header("Targetting Variables")]
    public bool inRange = false;
    public bool inView = false;
    private bool isFacingRight = true;
    public float distanceToTarget;
    public float viewRange;
    public float attackRange;
    public float targetTurnRate;
    public Transform target;
    public Transform playerOne;
    public Transform playerTwo;

    [Header("Projectile Variables")]
    public GameObject projectilePrefab;
    public Transform launchPoint;
    public float shootTime;
    public float shootCount;

    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("isDead") && !anim.GetBool("isAttacking"))
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

            if (distanceToTarget < viewRange)
            {
                //Turn toward the target
                if (transform.position.x <= target.position.x && !isFacingRight)
                {
                    Flip();
                    isFacingRight = true;
                }
                else if (transform.position.x >= target.position.x && isFacingRight)
                {
                    Flip();
                    isFacingRight = false;
                }
                //rotating the launch point to apply rotation to the instantiated shot
                Vector3 targetDir = target.position - transform.position;
                float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                launchPoint.rotation = Quaternion.RotateTowards(launchPoint.rotation, q, targetTurnRate * Time.deltaTime);
            }

            shootCount -= Time.deltaTime;

            if (distanceToTarget < attackRange)
            {
                if (shootCount <= 0)
                {
                    //Check Line of Sight
                    RaycastHit2D hit = Physics2D.Raycast(launchPoint.position, launchPoint.right, attackRange);
                    if (hit.transform == target)
                    {
                        anim.SetBool("isCasting", true);
                        GameObject newShot = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
                        shootCount = shootTime;
                        StartCoroutine(WaitForTime(0.5f));
                    }
                }
            }
        }
    }
    void Flip()
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x *= -1f;
        gameObject.transform.localScale = localScale;
    }
    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("isCasting", false);

    }
}