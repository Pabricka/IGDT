using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalControl : MonoBehaviour

{
    public bool jumpPowerUp ;
    public bool attackPowerUp;
    public bool grabPowerUp;

    public static GlobalControl Instance;
    private void Awake()
    {
        if (Instance== null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance!= this)
        {
            Destroy(gameObject);
        }
    }
}
