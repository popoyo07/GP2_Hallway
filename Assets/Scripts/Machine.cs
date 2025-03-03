using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    private GameObject player;
    private GameObject ui;
    [SerializeField] GameObject objectiveInfo;
    private GameObject enemy;
    public bool pWin;

    private GameObject musicController;
    
   
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ui = GameObject.FindGameObjectWithTag("UI");
        enemy = GameObject.Find("EnemyTest");
        musicController = GameObject.FindWithTag("Music");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Player>().hasCoffe != true)
        {
            ui.GetComponent<MainMenu>().hideUI();
            Debug.Log("You need the coffe");
            objectiveInfo.SetActive(true);
        }
        else
        {
            musicController.GetComponent<MusicControlelr>().PWinMusic();
            ui.GetComponent<MainMenu>().LoadWin();
            enemy.GetComponent<BossNavigation>().agent.isStopped = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (player.GetComponent<Player>().hasCoffe != true)
        {
            objectiveInfo.SetActive(false);
            ui.GetComponent<MainMenu>().UnHideUI();
            Debug.Log("player exit trigger");
        }

    }




}
