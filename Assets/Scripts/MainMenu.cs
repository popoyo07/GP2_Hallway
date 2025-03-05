using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public GameObject pause;
    public GameObject win;
    public GameObject gameOver;
    public GameObject staminaSlider;
    public GameObject playerSprite;
    public GameObject map;
    private bool isPaused;

    public EventSystem eventSystem;

    public PlayerControls controls;

    [Header("First Selected Buttons")]
    public GameObject pauseBack;
    public GameObject controlBack;
    public GameObject winPlayAgain;
    public GameObject losePlayAgain;
    public GameObject mainControlBack;
    public GameObject mainCreditsBack;
    public GameObject playButton;
    public GameObject mainCreditsButton;
    public GameObject mainControlsButton;

    [Header("Menu Parents")]
    public GameObject controlParent;
    public GameObject pauseParent;
    public GameObject mainControlParent;
    public GameObject mainCreditsParent;
    public GameObject mainMenuParent;


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
        controls = new PlayerControls();
        controls.Player.Pause.performed += _ => TogglePause();
        controls.Player.Map.performed += _ => ToggleMap();
    }
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
    }

    void pauseGame()
    {
        hideUI();
        pause.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        //controls.UI.Enable();
        //controls.Player.Disable();
        eventSystem.SetSelectedGameObject(pauseBack);
        //eventSystem.sendNavigationEvents = false;
        Time.timeScale = 0;
        
    }

    public void LoadContinueGame()
    {
        UnHideUI();
        Time.timeScale = 1;
        isPaused = false;
        //controls.UI.Disable();
        //controls.Player.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        pause.SetActive(false);
        eventSystem.sendNavigationEvents = true;
    }

    public void LoadWin()
    {
        hideUI();
        Cursor.lockState = CursorLockMode.None;
        win.SetActive(true);
        eventSystem.SetSelectedGameObject(winPlayAgain);

    }

    public void TheGameOverUI()
    {
        hideUI();
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        eventSystem.SetSelectedGameObject(losePlayAgain);
    }

    public void hideUI()
    {
        staminaSlider.SetActive(false);
        playerSprite.SetActive(false);
    }
    public void UnHideUI()
    {
        staminaSlider.SetActive(true);
        playerSprite.SetActive(true);
    }

    public void OpenControlPause()
    {
        if (controlParent != null)
        {
            controlParent.SetActive(true);
            eventSystem.SetSelectedGameObject(controlBack);
        }
    }

    public void CloseControlPause()
    {
        if (pauseParent != null)
        {
            pauseParent.SetActive(true);
            eventSystem.SetSelectedGameObject(pauseBack);
        }
    }

    public void OpenControlMain()
    {
        if (mainControlParent != null)
        {
            mainControlParent.SetActive(true);
            eventSystem.SetSelectedGameObject(mainControlBack);
        }
    }

    public void CloseControlMain()
    {
        if (mainControlParent != null)
        {
            mainMenuParent.SetActive(true);
            eventSystem.SetSelectedGameObject(mainControlsButton);
        }
    }

    public void OpenCreditsMain()
    {
        if (mainCreditsParent != null)
        {
            mainCreditsParent.SetActive(true);
            eventSystem.SetSelectedGameObject(mainCreditsBack);
        }
    }

    public void CloseCreditsMain()
    {
        if (mainCreditsParent != null)
        {
            mainMenuParent.SetActive(true);
            eventSystem.SetSelectedGameObject(mainCreditsButton);
        }
    }


    private void TogglePause()
    {
        if (!isPaused)
        {
            pauseGame();
        }
        else
        {
            LoadContinueGame();
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void ToggleMap()
    {
        if (!isPaused) 
        {
            map.SetActive(!map.activeInHierarchy);
        }
    }
}
