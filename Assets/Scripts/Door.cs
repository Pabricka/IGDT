using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D col;
    public void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    public void Break(float x)
    {
        if(x< transform.position.x)
        {
            anim.SetTrigger("Left");
        }
        else
        {
            anim.SetTrigger("Right");
        }
        Destroy(col);
    }

}
