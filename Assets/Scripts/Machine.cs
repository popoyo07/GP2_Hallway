using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    private GameObject player;
    private GameObject ui;

   
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ui = GameObject.FindGameObjectWithTag("UI");
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
        }
    }
 
}
