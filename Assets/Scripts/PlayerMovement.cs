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

    private Animator anim;

    private float lastDamageTime;
    private float lastAttackTime;

    public float immuneTime;
    public float stunTime;

    void Start()
    {
        anim = GetComponent<Animator>();
        
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
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("Attack", true);
            lastAttackTime = Time.time;
        }
        if (Input.GetButton("Fire1"))
        {
            if (Time.time > lastAttackTime + 0.2)
            {
                shoot = true;
            }
        }
        else
        {
            shoot = false;
            anim.SetBool("Attack", false);
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

            anim.SetTrigger("Damage");
        }
    }

    void GameOver()
    {
        //TODO
    }
}
