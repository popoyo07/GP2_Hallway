using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossNavigation : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent agent;
    public Transform[] waypoints;

    [SerializeField] LayerMask playerLayers;
    
    private int waypointIndex;
    private LineOfSight LOS;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerTest");

        // Get Line of Sight from child object 
        LOS = GetComponentInChildren<LineOfSight>();
       

        if (waypoints.Length > 0)
        {
            //Randomize initial waypoint
            waypointIndex = Random.Range(0, waypoints.Length);
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }
    
    void Update()
    {
       
        if (LOS.m_IsPlayerInRange)
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

        if (player != null && agent != null)
        {
            agent.destination = player.transform.position;
        }
    }


}
