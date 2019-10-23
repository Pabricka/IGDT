using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speedX;
    public float speedY;
    private bool right;

    private void Start()
    {
        right = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(right) transform.position = new Vector2(transform.position.x + speedX * Time.deltaTime, transform.position.y + speedY * Time.deltaTime);
        else transform.position = new Vector2(transform.position.x - speedX * Time.deltaTime, transform.position.y - speedY * Time.deltaTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlatformEdge"))
        {
            right = !right;
        }
    }
}
