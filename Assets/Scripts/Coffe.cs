using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffe : MonoBehaviour
{
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            player.GetComponent<Player>().hasCoffe = true;
            Destroy(gameObject);
        }

    }
}
