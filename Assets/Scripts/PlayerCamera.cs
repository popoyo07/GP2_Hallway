using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensitivity = 300f;
    public Transform player;
    private float xRotation = 0f;
    
    // track enemy and lsoe condition
    [Header(" Enemy ")]
    [SerializeField] private GameObject enemy;

    //private Vector3 locking;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);
        

        if (enemy.GetComponent<BossNavigation>().playerCaught) // checking for the playerCaught bool in other script 
        {
            
            player.LookAt(enemy.transform.position); // turn to look at enemy
          // misisng a way to lock palyer position so they dont move. 
        }
    }
}
