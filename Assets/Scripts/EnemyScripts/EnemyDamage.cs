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

    [Header("Animation")]
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim != null)
        {
            if (anim.GetBool("isDead"))
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
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
            Debug.Log("Attack");
            playerController = collision.gameObject.GetComponentInChildren<PlayerController>();
            playerController.knockbackCount = playerController.knockbackTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                LookLeft();
                playerController.knockFromRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                LookRight();
                playerController.knockFromRight = false;
            }
            healthManager.TakeDamage(damage, collision.gameObject);
            StartCoroutine(WaitForTime(0.5f));
        }
    }

    void LookLeft()
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x = -1f;
        gameObject.transform.localScale = localScale;
    }

    void LookRight()
    {
        Vector3 localScale = gameObject.transform.localScale;
        localScale.x = 1f;
        gameObject.transform.localScale = localScale;
    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        anim.SetBool("isAttacking", false);
    }
}