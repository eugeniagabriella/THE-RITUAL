using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float crouchSpeed = 1.5f;
    float currentSpeed;

    [Header("Jump")]
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Crouch")]
    public float standingHeight = 1.8f;
    public float crouchHeight = 1f;

    CharacterController controller;
    Vector3 velocity;
    bool isCrouching;

    void Awake()
    {
        controller = GetComponent<CharacterController>();

        if (!controller.enabled)
            controller.enabled = true;
    }

    void Start()
    {
        ResetPlayerState();

        currentSpeed = walkSpeed;
        controller.height = standingHeight;
    }

    void OnEnable()
    {
        ResetPlayerState();
    }

    void Update()
    {
        if (controller == null || !controller.enabled)
            return;

        GroundCheck();
        Move();
        Jump();
        Crouch();
        ApplyGravity();
    }

    void ResetPlayerState()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (Camera.main != null)
        {
            MouseLook mouseLook = Camera.main.GetComponent<MouseLook>();
            if (mouseLook != null)
                mouseLook.enabled = true;
        }

        if (controller != null)
            controller.enabled = true;

        velocity = Vector3.zero;
        isCrouching = false;
    }

    void GroundCheck()
    {
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift) && !isCrouching;

        if (isCrouching)
            currentSpeed = crouchSpeed;
        else
            currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && controller.isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = true;
            controller.height = crouchHeight;
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;
            controller.height = standingHeight;
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}