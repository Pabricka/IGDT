using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * speed * Time.deltaTime;

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("spiderCollider"))
        {
            transform.eulerAngles += transform.forward * 90;
            transform.position += -transform.right * 1;
        }
    }
}
