using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform SpaceBot;
    public Transform lore;

    public void Update()
    {
        if(SpaceBot.localScale.x > 0.01f)
        {
            SpaceBot.localScale -= new Vector3(0.2f * Time.deltaTime, 0.2f * Time.deltaTime, 0.2f * Time.deltaTime);
        }
        else
        {
            SpaceBot.localScale = new Vector3(0, 0,0);
        }

        lore.position = new Vector3(lore.position.x, lore.position.y, lore.position.z +3 * Time.deltaTime);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Tutorial_area_02");
    }
    public void QuitGame()
    {
        Debug.Log("quit"); 
        Application.Quit();
    }
    public void ShowCredits()
    {
        SceneManager.LoadScene("Credits");
    }
}
