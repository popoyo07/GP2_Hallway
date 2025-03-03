using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject pause;
    public GameObject win;
    public GameObject gameOver;
    public GameObject staminaSlider;
    public GameObject playerSprite;
    private bool isPaused;

    public EventSystem eventSystem;
    public void LoadPlayGame()
    {
        SceneManager.LoadScene("WhiteBox");
    }

    public void LoadMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadQuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
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
        staminaSlider.SetActive(false);
        playerSprite.SetActive(false);
        pause.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        eventSystem.sendNavigationEvents = false;
        Time.timeScale = 0;
        
    }

    public void LoadContinueGame()
    {
        staminaSlider.SetActive(true);
        playerSprite.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        pause.SetActive(false);
        eventSystem.sendNavigationEvents = true;
    }

    public void LoadWin()
    {
        staminaSlider.SetActive(false);
        playerSprite.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        win.SetActive(true);

    }

    public void TheGameOverUI()
    {
        staminaSlider.SetActive(false);
        playerSprite.SetActive(false);
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

    }

}
