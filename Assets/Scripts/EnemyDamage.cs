using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Variables")]
    public int damage;

    [Header("Scripts")]
    public HealthManager healthManager;
    private PlayerController playerController;
    public EnemyPatrol enemyPatrol;

    [Header("Animation")]
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    /// <summary>
    /// Deals damage if either of the players collide with the enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isAttacking", true);
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
            healthManager.TakeDamage(damage, collision.gameObject);
            StartCoroutine(WaitForTime(1));
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("isAttacking", false);
    }
}