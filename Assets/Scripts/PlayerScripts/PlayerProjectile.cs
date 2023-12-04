using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public Rigidbody2D projectileRB;
    public float speed;
    public float projectileLife;
    public float projectileCount;

    // Start is called before the first frame update
    void Start()
    {
        projectileCount = projectileLife;
    }

    // Update is called once per frame
    void Update()
    {
        projectileCount -= Time.deltaTime;

        if (projectileCount <=0)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
            projectileRB.velocity = new Vector2(speed, projectileRB.velocity.y);
    }

    public void SetSpeed(bool facingRight)
    { 
        if (!facingRight && speed > 0)
        {
            speed = -speed;
        }
        if (facingRight && speed <0)
        {
            speed = -speed;
        }
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Animator>().SetBool("isDead", true);
        }
        Destroy(gameObject);
    }
}
