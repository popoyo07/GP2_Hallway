using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossNavigation : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public LayerMask playerMask, obstacleMask;
    public Transform[] waypoints;

    private int waypointIndex;

    [SerializeField] private float sightRange;

    [SerializeField] private bool onSight;

    // Start is called before the first frame update
    void Start()
    {
        agent.SetDestination(waypoints[0].position);
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("PlayerTest").transform;

        if (waypoints.Length > 0)
        {
            waypointIndex = Random.Range(0, waypoints.Length); // CHANGE: Randomize initial waypoint
            agent.SetDestination(waypoints[waypointIndex].position);
        }
    }
    
    void FixedUpdate()
    {
        // Check if the player is within sight range
        onSight = PlayerInSight();

        if (!onSight)
        {
            Patroling();
        }
        else
        {
            Chase();
        }

    }

    private bool PlayerInSight()
    {
        // Check if the player is within sight range
        if (Physics.CheckSphere(transform.position, sightRange, playerMask))
        {
            // Calculate direction to the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Calculate distance to the player
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Perform a raycast to check for obstacles
            if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
            {
                // If no obstacles are in the way, the player is visible
                return true;
            }
        }

        // Player is not visible
        return false;
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

    private void OnDrawGizmosSelected()
    {
        // Gizmos that shows the sight range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
