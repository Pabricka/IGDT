using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{

    public float horizontalForce;
    public float verticalForce;
    public float timer;
    public int pulses;
    private int passedPulses;
    private float passedTime;
    private bool flip;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public Sprite sprite1;
    public Sprite sprite2;

    private void Start()
    {
        flip = false;
        passedTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= passedTime + timer/2)
        {
            sr.sprite = sprite1;
        }

        if (Time.time >= passedTime + timer)
        {
            sr.sprite = sprite2;
            if(passedPulses >= pulses)
            {
                passedPulses = 0;
                flip = !flip;
            }
            rb.velocity = new Vector2(0f, 0f);
            rb.angularVelocity = 0;
            if (!flip)
            {
                rb.AddForce(new Vector2(verticalForce, horizontalForce));
            }
            else
            {
                rb.AddForce(new Vector2(-verticalForce, -horizontalForce));
            }

            passedPulses += 1;
            passedTime = Time.time;
        }
    }
}
