using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public Rigidbody2D projectileRB;
    public float speed;
    public float projectileLife;
    public float projectileCount;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        projectileCount = projectileLife;
    }

    // Update is called once per frame
    void Update()
    {
        projectileCount -= Time.deltaTime;

        if (projectileCount <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        projectileRB.AddRelativeForce (new Vector2(speed, projectileRB.velocity.y));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject healthManager = GameObject.Find("HealthManager");
            healthManager.GetComponent<HealthManager>().TakeDamage(damage, collision.gameObject);
        }
        Destroy(gameObject);
    }
}
