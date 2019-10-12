using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int HP;
    public int damage;

    public float xKnockback, yKnockback;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage, float playerX)
    {
        HP -= damage;
        if (HP <= 0) Die();
        rb.velocity = new Vector2(0f, 0f);
        if(playerX < transform.position.x) rb.AddForce(new Vector2(xKnockback, yKnockback));
        else rb.AddForce(new Vector2(-xKnockback, yKnockback));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(damage, transform.position.x);
        }
    }

    private void Die()
    {
        Destroy(gameObject, 0.25f);
    }
}
