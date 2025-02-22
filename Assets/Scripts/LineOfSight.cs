using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class LineOfSight : MonoBehaviour
{
    public GameObject player;
    public bool canChase;


    [Header("Raycast Settings")]
    public LayerMask obstacleLayer; // Layer for obstacles 
    public LayerMask playerLayer;

    public float radius;
    [Range(0,360)]
    public float angel; //meant to type "angle"

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FieldOfViewCheck();
    }

    private void FieldOfViewCheck()
    {
        // collect all colliders 
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerLayer);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angel / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleLayer))
                {
                    canChase = true;
                   
                }
                else
                {
                    canChase = false;
               
                }
            } 
            else
            {
                canChase = false;
                
            }
        }
        else if (canChase)
        {
            canChase = false;
            
        }
    }

}
