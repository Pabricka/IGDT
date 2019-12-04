using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public bool jumpPowerUp = false;
    public bool attackPowerUp = false;
    public bool grabPowerUp = false;

    bool stopGrab;
    bool dead;

    public float runSpeed;
    public bool jump = false;
    public CharacterController2D controller;
    float horizontalMove = 0f;
    private bool shoot = false;
    public bool grab = false;

    private Animator anim;

    private float lastDamageTime;
    private float lastAttackTime;

    public float immuneTime;
    public float stunTime;


    void Start()
    {
        dead = false;
        anim = GetComponent<Animator>();
        jumpPowerUp = GlobalControl.Instance.jumpPowerUp;
        attackPowerUp = GlobalControl.Instance.attackPowerUp;
        grabPowerUp = GlobalControl.Instance.grabPowerUp;
        
    }

    void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            Application.Quit();
        }

        if (!dead)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            if (Input.GetButton("Jump") && jumpPowerUp)
            {
                jump = true;
            }
            else
            {
                jump = false;
            }
            if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))
            {
                anim.SetBool("Attack", true);
                lastAttackTime = Time.time;
            }
            if (Input.GetButton("Fire1") && attackPowerUp)
            {
                if (Time.time > lastAttackTime + 0.15)
                {
                    shoot = true;
                }
            }
            else if (Input.GetButton("Fire2") && grabPowerUp)
            {
                if (Time.time > lastAttackTime + 0.15)
                {
                    grab = true;
                }
            }
            else
            {
                grab = false;
                shoot = false;
                anim.SetBool("Attack", false);
            }
        }
        
    }

    void FixedUpdate()
    {
        if (Time.time > lastDamageTime + stunTime)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump, shoot, grab);
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
    public void SavePlayer()
    {
        GlobalControl.Instance.attackPowerUp = attackPowerUp;
        GlobalControl.Instance.jumpPowerUp = jumpPowerUp;
        GlobalControl.Instance.grabPowerUp = grabPowerUp;

        horizontalMove = 0;
        dead = true;
        anim.SetTrigger("Death");
    }
    
}
