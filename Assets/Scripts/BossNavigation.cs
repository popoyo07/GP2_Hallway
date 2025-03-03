using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

//using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class BossNavigation : MonoBehaviour
{
    [SerializeField] private GameObject levelUI;
    private GameObject player;
    public NavMeshAgent agent;
    public Transform[] waypoints;

    private AudioSource enemyCatch;
    [SerializeField] private AudioClip catchSound;
    public bool playerCaught;
    public GameObject gameMusic;

   // private IEnumerator waitForCoroutine;

    [Header(" Patrol ")]
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float enemyAngularSpeed;

    [Header(" UI Animation ")]
    public GameObject playerSprite;
    public Animator pSprite;
 
 
    [SerializeField] LayerMask playerLayers;
    
    private int waypointIndex;
    private LineOfSight LOS;


    private void OnTriggerEnter(Collider other)
    {
           
        if (other.transform.tag == "Player")
        {
            //Cursor.lockState = false;
            player.GetComponent<Player>().noMove = true;
            playerCaught = true;
            transform.LookAt(player.transform.position);
            agent.isStopped = true;
            enemyCatch.clip = catchSound;
            enemyCatch.Play();
            StartCoroutine(WaitFor(4f));
        }

        if (other.transform.tag == "Break")
        {
            Patroling();
        }
        
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        //waitForCoroutine = waitFor(4f);
        // sound control
        enemyCatch = GetComponent<AudioSource>();
        // music = GameObject.FindWithTag("Music");
        

        // Player Sprite animator
        playerSprite = GameObject.Find("PlayerSprite");
        pSprite = playerSprite.GetComponent<Animator>();

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
        else if (!LOS.canChase && !playerCaught && agent.remainingDistance < 0.2f)
        {
            agent.speed = patrolSpeed;
            Patroling();
           
        }
    }

 

    private void Patroling()
    {
        gameMusic.GetComponent<MusicControlelr>().ResumeMusic();
        pSprite.SetBool("isChased", false);
        Debug.Log("this is not chased");
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
            gameMusic.GetComponent<MusicControlelr>().PChaseMusic();
            pSprite.SetBool("isChased", true);
            // Chase Player
            agent.SetDestination(player.transform.position);
            Debug.Log("this is chased");
            // increase rotation speed 
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z ));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
        }

    }

    // Used IEnumerator to dealy the game over screen execution
    IEnumerator WaitFor(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        endGame();
        //Destroy(gameObject);
    }
    private void endGame()
    {
        // need to add more things for when 
        gameMusic.GetComponent<MusicControlelr>().GameOver(); // calls and executes 

        pSprite.SetBool("dead", true);
      
        levelUI.GetComponent<MainMenu>().TheGameOverUI();
       
    }


}
