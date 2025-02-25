using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    private GameObject player;
    private GameObject ui;
    private GameObject enemy;

   
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ui = GameObject.FindGameObjectWithTag("UI");
        enemy = GameObject.Find("EnemyTest");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Player>().hasCoffe != true)
        {
            Debug.Log("You need the coffe");
        }
        else
        {

            ui.GetComponent<MainMenu>().LoadWin();
            enemy.GetComponent<BossNavigation>().agent.isStopped = true;
        }
    }
 
}
