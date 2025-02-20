using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Machine : MonoBehaviour
{
    private GameObject player;

   
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<Player>().hasCoffe != true)
        {
            Debug.Log("You need the coffe");
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("WIN");
        }
    }
 
}
