using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool ArtPrototype = false;
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
    private float fixedY;
    private PlayerControls controls;
    private Vector2 moveInput;

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

    public bool noMove = false;

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

        fixedY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!noMove)
        {
            Movement();
        } 

        
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }

        if (slideSlider != null)
        {
            float timeLastSlide = Time.time - lastSlide;
            slideSlider.value = Mathf.Clamp(timeLastSlide, 0, slideCooldown);
        }
    }

    void Movement()
    {
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;

        if (!isSliding)
        {
            if (isSprinting && currentStamina > 0)
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

        StaminaRegen();

        if (ArtPrototype)
        {
            Vector3 fixedPosition = transform.position;
            fixedPosition.y = fixedY;
            transform.position = fixedPosition;
        }

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

        float raycastDistance = standingHeight - proneHeight + 0.1f;
        int layerMask = LayerMask.GetMask("Table");

        while (true)
        {
            Vector3 raycastStart = cam.position; 

            RaycastHit hit;
            if (Physics.Raycast(raycastStart, Vector3.up, out hit, raycastDistance, layerMask))
            {

                float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");
                Vector3 move = transform.right * x + transform.forward * z;
                controller.Move(move * speed * Time.deltaTime);

                cam.localPosition = new Vector3(cam.localPosition.x, proneCamHeight, cam.localPosition.z);
            }
            else
            {
                cam.localPosition = new Vector3(cam.localPosition.x, standingCamHeight, cam.localPosition.z);

                Vector3 pushDirection = transform.forward * 0.2f;
                controller.Move(pushDirection);

                break;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                break;
            }

            yield return null;
        }

        controller.height = originalHeight;
        controller.center = originalCenter;

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

    private void Awake()
    {
        controls = new PlayerControls();

        // Set up movement input
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        // Sprinting input
        controls.Player.Sprint.started += ctx => isSprinting = true;
        controls.Player.Sprint.canceled += ctx => isSprinting = false;

        // Sliding input
        controls.Player.Slide.started += ctx => TrySlide();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void TrySlide()
    {
        if (Time.time > lastSlide + slideCooldown && !ArtPrototype)
        {
            StartCoroutine(Slide());
        }
    }
}
