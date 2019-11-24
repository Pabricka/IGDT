using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCube : MonoBehaviour
{

    public PlayerMovement pm;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Stop")
        {
            if (collision.transform.position.x > transform.position.x)
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y);
                pm.transform.position = new Vector3(pm.transform.position.x - 0.1f, pm.transform.position.y);
            }
            else
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y);
                pm.transform.position = new Vector3(pm.transform.position.x + 0.1f, pm.transform.position.y);
            }

            pm.grab = false;
        }
    }


}
