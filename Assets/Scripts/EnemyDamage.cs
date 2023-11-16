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

    /// <summary>
    /// Deals damage if either of the players collide with the enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerController = collision.gameObject.GetComponentInChildren<PlayerController>();
            playerController.knockbackCount = playerController.knockbackTime;
            if(collision.transform.position.x <= transform.position.x)
            {
                playerController.knockFromRight = true;
            }
            if (collision.transform.position.x <= transform.position.x)
            {
                playerController.knockFromRight = false;
            }
            healthManager.TakeDamage(damage, collision.gameObject);
        }
    }
}
