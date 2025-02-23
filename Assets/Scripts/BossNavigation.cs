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

    private AudioSource enemyCatch;
    [SerializeField] private AudioClip catchSound;
    public bool playerCaught;
    public GameObject gameMusic;

    private IEnumerator waitForCoroutine;

    [Header(" Patrol ")]
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float enemyAngularSpeed;


    [SerializeField] LayerMask playerLayers;
    
    private int waypointIndex;
    private LineOfSight LOS;


    private void OnTriggerEnter(Collider other)
    {
           
        if (other.transform.tag == "Player")
        {
            playerCaught = true;
            transform.LookAt(player.transform.position);
            agent.isStopped = true;
            enemyCatch.clip = catchSound;
            enemyCatch.Play();
            StartCoroutine(waitForCoroutine);
        }
        
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        waitForCoroutine = waitFor(4f);
        // sound control
        enemyCatch = GetComponent<AudioSource>();
       
        


        // Get Line of Sight from child object 
        LOS = GetComponentInChildren<LineOfSight>();

        agent.speed = patrolSpeed;
        agent.angularSpeed = enemyAngularSpeed;
        agent.acceleration = 100f;
        agent.stoppingDistance = .5f;

        Patroling();
    }
    
    void Update()
    {
     
        if (LOS.canChase && !playerCaught)
        {
            agent.speed = chaseSpeed;
            Chase();
        }
        else if (!playerCaught)
        {
            agent.speed = patrolSpeed;
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
        if (player != null)
        {
            // Chase Player
            agent.SetDestination(player.transform.position);

            // increase rotation speed 
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z ));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

    }

    // Used IEnumerator to dealy the game over screen execution
    IEnumerator waitFor(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        endGame();
        //Destroy(gameObject);
    }
    private void endGame()
    {
        // need to add more things for when 
        gameMusic.GetComponent<MusicControlelr>().GameOver(); // calls and executes 


        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
       
    }


}
