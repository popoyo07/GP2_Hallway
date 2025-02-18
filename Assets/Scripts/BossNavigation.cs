using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossNavigation : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    private GameObject player;
    private NavMeshAgent agent;
    public Transform[] waypoints;

    

    [SerializeField] LayerMask playerLayers;
    
    private int waypointIndex;
    private LineOfSight LOS;


    private void OnTriggerEnter(Collider other)
    {
           
        if (other.transform.tag == "Player")
        {
            endGame();
        }
        
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        // Get Line of Sight from child object 
        LOS = GetComponentInChildren<LineOfSight>();


        Patroling();
    }
    
    void Update()
    {
     
        if (LOS.canChase)
        {
        
            Chase();
        }
        else
        {
            Patroling();
        }

    }

 

    private void Patroling()
    {
        // Chose a random waypoint to move next
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            waypointIndex = Random.Range(0, waypoints.Length); 
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }   
  

    private void Chase()
    {
        // Chase Player
        agent.destination = player.transform.position;
    }

    private void endGame()
    {
        // need to add more things for when 
        gameOver.SetActive(true);
        Destroy(gameObject);
    }


}
