using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class PauseBehavior : MonoBehaviour
{
    public GameObject pause;
    private bool isPaused;

    private void Update()
    {
        if (isPaused != true)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                pauseGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                continueGame();
            }
        }

    }
    void pauseGame()
    {
        pause.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void continueGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pause.SetActive(false);
    }
    
}
