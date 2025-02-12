using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public BossNavigation bossNavigation;
    public LayerMask obstacleMask; // Layer mask for obstacles 

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check for obstacles between enemy and player
            Vector3 directionToPlayer = (other.transform.position - bossNavigation.transform.position).normalized;
            float distanceToPlayer = Vector3.Distance(bossNavigation.transform.position, other.transform.position);

            if (!Physics.Raycast(bossNavigation.transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
            {
                // player is in sight
                bossNavigation.PlayerOnSight();
            }
            else
            {
                // Obstacle detected
                bossNavigation.PlayerExitSight();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player exited the trigger area
            bossNavigation.PlayerExitSight();
        }
    }
}
