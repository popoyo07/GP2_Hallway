using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Slider staminaSlider;
    public Slider slideSlider;
    public float speed = 5f;
    public float sprintSpeed = 10f;
    public float standingHeight = 2f;
    public float standingCamHeight = 0.4f;
    private Vector3 originalCenter;
    private bool isSprinting = false;

    [Header("Stamina")]
    public float maxStamina = 60f;
    public float currentStamina;
    public float staminaDrain = 20f;
    public float staminaRegenSpeed = 10f;
    public float staminaDelay = 2f;
    private float recentSprint;

    [Header("Sliding")]
    public float proneHeight = 0.5f;
    public float proneCamHeight = -0.5f;
    private bool isSliding = false;
    public float slideSpeed = 7f;
    public float slideLength = 0.5f;
    public float slideCooldown = 2f;
    private float lastSlide;

    // For future refference 
    public bool hasCoffe = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
        originalCenter = controller.center;
        // cam kept moving on start, this seems to fix it
        Vector3 cameraPosition = cam.localPosition;
        cameraPosition.y = standingCamHeight;
        cam.localPosition = cameraPosition;

        currentStamina = maxStamina;

        if (slideSlider != null)
        {
            slideSlider.maxValue = slideCooldown;
            slideSlider.value = slideCooldown;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }

        float timeLastSlide = Time.time - lastSlide;
        slideSlider.value = Mathf.Clamp(timeLastSlide, 0, slideCooldown);
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if(isSliding == false)
        {
            if (isSprinting == true && currentStamina > 0)
            {
                controller.Move(move * sprintSpeed * Time.deltaTime);
                currentStamina -= staminaDrain * Time.deltaTime;
                recentSprint = Time.time;
            }
            
            else 
            {
                isSprinting = false;
                controller.Move(move * speed * Time.deltaTime);
   
            }
        }
        

        if(Input.GetKeyDown(KeyCode.LeftShift) && isSliding == false && currentStamina > 0)
        {
            isSprinting = true;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            isSprinting = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && Time.time > lastSlide + slideCooldown)
        {
            StartCoroutine(Slide());
        }

        StaminaRegen();
    }

    IEnumerator Slide()
    {
        isSliding = true;
        lastSlide = Time.time;

        float originalHeight = controller.height;
        controller.height = proneHeight;

        Vector3 originalCenter = controller.center;
        controller.center = new Vector3(originalCenter.x, originalCenter.y - (standingHeight - proneHeight) / 2f, originalCenter.z);

        float originalCamHeight = cam.localPosition.y;
        cam.localPosition = new Vector3(cam.localPosition.x, proneCamHeight, cam.localPosition.z);

        Vector3 slideDirection = transform.forward;
        float slideStartTime = Time.time;

        while (Time.time < slideStartTime + slideLength)
        {
            controller.Move(slideDirection * slideSpeed * Time.deltaTime);
            yield return null;
        }

        controller.height = originalHeight;
        controller.center = originalCenter;
        cam.localPosition = new Vector3(cam.localPosition.x, originalCamHeight, cam.localPosition.z);

        isSliding = false;
    }

    void StaminaRegen()
    {
        if (isSprinting == false && currentStamina < maxStamina && Time.time > recentSprint + staminaDelay)
        {
            currentStamina += staminaRegenSpeed * Time.deltaTime;

            //Limit stamina
            currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        }
    }
}
