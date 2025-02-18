using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeMachine : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
        player.GetComponent<Inventory>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        player.hasCoffe = true;
    }
  
}
