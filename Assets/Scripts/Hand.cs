using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    FixedJoint2D joint;
    // Start is called before the first frame update
    void Start()
    {
        joint = GetComponent<FixedJoint2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Movable")
        {
            joint.connectedBody = collision.rigidbody;
        }

    }
}
