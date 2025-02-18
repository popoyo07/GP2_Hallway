using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject pause;
    private bool isPaused;
    public void LoadPlayGame()
    {
        SceneManager.LoadScene("WhiteBox");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadQuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
       
        if (isPaused != true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                pauseGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                LoadContinueGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    void pauseGame()
    {
        pause.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void LoadContinueGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pause.SetActive(false);
    }
}
