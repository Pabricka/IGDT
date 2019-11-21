using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage, transform.position.x);
        }
        if (collision.CompareTag("Door"))
        {
            collision.gameObject.GetComponent<Door>().Break(transform.position.x);
        }
    }
}
