using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float proneSpeed = 2.5f;
    public float standingHeight = 2f;
    public float proneHeight = 0.5f;
    public bool isProne = false;
    public float standingCamHeight = 0.4f;
    public float proneCamHeight = -0.5f;
    private Vector3 originalCenter;
    private bool isSprinting = false;
   

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        originalCenter = controller.center;
        // cam kept moving on start, this seems to fix it
        Vector3 cameraPosition = cam.localPosition;
        cameraPosition.y = standingCamHeight;
        cam.localPosition = cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Crouching();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if(isProne == false)
        {
            if(isSprinting == false)
            {
                controller.Move(move * speed * Time.deltaTime);
            }
            
            else if (isSprinting == true)
            {
                controller.Move(move * sprintSpeed * Time.deltaTime);
            }
        }
        else if (isProne == true)
        {
            controller.Move(move * proneSpeed * Time.deltaTime);
        }


        if(Input.GetKeyDown(KeyCode.LeftShift) && isProne == false)
        {
            isSprinting = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }
    }

    void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isProne = !isProne;
        }

        float targetHeight = isProne ? proneHeight : standingHeight;
        controller.height = targetHeight;

        float centerY = originalCenter.y - (standingHeight - targetHeight) / 2f;
        controller.center = new Vector3(originalCenter.x, centerY, originalCenter.z);

        // This cam change took me forver to figure out im not gonna touch it
        float targetCamHeight = isProne ? proneCamHeight : standingCamHeight;
        Vector3 cameraPosition = cam.localPosition;
        cameraPosition.y = targetCamHeight;
        cam.localPosition = cameraPosition;
    }
}
