using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * 25, Time.deltaTime * 25, 0);
    }
}
