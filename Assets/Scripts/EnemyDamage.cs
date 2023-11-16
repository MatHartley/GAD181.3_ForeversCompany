using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [Header("Damage Variables")]
    public int damage;

    [Header("Scripts")]
    public HealthManager healthManager;

    /// <summary>
    /// Deals damage if either of the players collide with the enemy
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            healthManager.TakeDamage(damage, collision.gameObject);
        }
    }
}
