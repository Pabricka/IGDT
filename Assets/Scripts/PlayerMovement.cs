using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float runSpeed;
    public bool jump = false;
    public CharacterController2D controller;
    float horizontalMove = 0f;
    public bool shoot = false;

    private float lastDamageTime;

    public float immuneTime;
    public float stunTime;

    void Start()
    {
        
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if(Input.GetButton("Jump"))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }
        if(Input.GetButton("Fire1"))
        {
            shoot = true;
        }
        else
        {
            shoot = false;
        }
    }

    void FixedUpdate()
    {
        if (Time.time > lastDamageTime + stunTime)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, shoot);
        }
    }

    public void TakeDamage(int damage, float enemyX)
    {
        if(Time.time > lastDamageTime + immuneTime)
        {
            lastDamageTime = Time.time;
            controller.TakeDamage(damage, enemyX);
        }
    }

    void GameOver()
    {
        //TODO
    }
}
