using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public GameObject player;
    public bool m_IsPlayerInRange;

    [Header("Raycast Settings")]
    public LayerMask obstacleLayer; // Layer for obstacles (e.g., walls, environment)
 

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player.transform)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform == player.transform)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.transform.position - transform.position + Vector3.up;
            


            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, obstacleLayer))
            {
                if (hit.transform == player.transform)
                {
                    Debug.Log("On Sight!!!!!");
                }
                else 
                {
                    m_IsPlayerInRange=false;
                    Debug.Log("Obstacle Obstacle Obstacle");
                }
            }
        }
    }

}
