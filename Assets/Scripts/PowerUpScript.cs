using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public bool jump;
    public bool jumpBoost;
    public bool grab;
    public bool attackUp;

    public float fuelAmount;
    public GameObject text;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (jump)
            {
                collision.gameObject.GetComponent<PlayerMovement>().jumpPowerUp = true;
            }
            if (jumpBoost)
            {
                collision.gameObject.GetComponent<CharacterController2D>().maxFuel += fuelAmount;
            }
            if (grab)
            {
                collision.gameObject.GetComponent<PlayerMovement>().grabPowerUp = true;
            }
            if (attackUp)
            {
                collision.gameObject.GetComponent<PlayerMovement>().attackPowerUp = true;
            }

            text.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
