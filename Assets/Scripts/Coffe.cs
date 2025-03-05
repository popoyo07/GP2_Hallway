using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffe : MonoBehaviour
{
    private GameObject player;
    public AudioClip collectionSFX;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.tag == "Player")
        {
            player.GetComponent<Player>().hasCoffe = true;
            player.GetComponent<AudioSource>().clip = collectionSFX;
            player.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }

    }
}
