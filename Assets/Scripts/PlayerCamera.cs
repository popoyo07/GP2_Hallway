using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivity = 300f;
    public float mouseSensitivityMultiplier = 0.1f;
    public Transform player;
    private float xRotation = 0f;
    private GameObject thePlayer;
    private PlayerControls controls;
    private Vector2 lookInput;
    private bool isUsingMouse = false;

    // track enemy and lsoe condition
    [Header(" Enemy ")]
    [SerializeField] private GameObject enemy;

    //private Vector3 locking;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        thePlayer = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float multiplier = isUsingMouse ? mouseSensitivityMultiplier : 1f;

        float mouseX = lookInput.x * sensitivity * multiplier * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * multiplier * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
        
        if(enemy != null)
        {
            if (enemy.GetComponent<BossNavigation>().playerCaught) // checking for the playerCaught bool in other script 
            {
                thePlayer.GetComponent<Player>().noMove = true;
                player.LookAt(enemy.transform.position); // turn to look at enemy
                                                         // misisng a way to lock palyer position so they dont move. 
            }
        }

        /*if (isUsingMouse == true)
        {
            Debug.Log("Is Using Mouse");
        }

        else
        {
            Debug.Log("Is Using Controller");
        } */
        
    }

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Look.performed += ctx =>
        {
            lookInput = ctx.ReadValue<Vector2>();

            isUsingMouse = ctx.control.device is Mouse;
        };

        controls.Player.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
