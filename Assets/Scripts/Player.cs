using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 5f;
    public float crouchSpeed = 2.5f;
    public float standingHeight = 2f;
    public float crouchingHeight = -0.5f;
    private bool isCrouching = false;
    public float standingCamHeight = 0.4f;
    public float crouchingCamHeight = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

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
        if(isCrouching == false)
        {
            controller.Move(move * speed * Time.deltaTime);
        }
        else if (isCrouching == true)
        {
            controller.Move(move * crouchSpeed * Time.deltaTime);
        }
    }

    void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(isCrouching == false)
            {
                isCrouching = true;
            }

            else
            {
                isCrouching = false;
            }
        }

        float Height = isCrouching ? crouchingHeight : standingHeight;
        controller.height = Mathf.Lerp(controller.height, Height, Time.deltaTime * 8f);

        float CameraHeight = isCrouching ? crouchingCamHeight : standingCamHeight;
        Vector3 cameraPosition = cam.localPosition;
        cameraPosition.y = Mathf.Lerp(cam.localPosition.y, CameraHeight, Time.deltaTime * 8f); // i think this works im not gonna question it
        cam.localPosition = cameraPosition;
    }
}
