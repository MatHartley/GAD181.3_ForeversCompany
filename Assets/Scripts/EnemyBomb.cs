using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : MonoBehaviour
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
        if (anim.GetBool("isDead"))
        {
            StartCoroutine(WaitForTime(0.9f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isDead", true);
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
        }
    }

    IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
