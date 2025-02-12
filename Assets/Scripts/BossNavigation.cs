using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public Transform[] waypoints;

    private int waypointIndex;

    [SerializeField] private bool onSight;

    private LineOfSight LOS;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(waypoints[0].position);
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerTest").transform;

        // Get Line of Sight from child object 
        LOS = GetComponentInChildren<LineOfSight>();
        if (LOS != null)
        {
            LOS.bossNavigation = this;
        }

        if (waypoints.Length > 0)
        {
            waypointIndex = Random.Range(0, waypoints.Length); //Randomize initial waypoint
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }
    
    void FixedUpdate()
    {

        if (!onSight)
        {
            Patroling();
        }
        else
        {
            Chase();
        }

    }

    public void PlayerOnSight()
    {
        onSight = true;
    }

    public void PlayerExitSight()
    {
        onSight = false;
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
        if (player != null)
        {
            agent.destination = player.position;
        }
    }


}
