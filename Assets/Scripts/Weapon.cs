using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioSource enemyDeath;
    public int damage;

    public void Start()
    {
        enemyDeath = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, transform.position.x))
            {
                enemyDeath.Play();
            }
        }
        if (collision.CompareTag("Door"))
        {
            collision.gameObject.GetComponent<Door>().Break(transform.position.x);
        }
    }
}
