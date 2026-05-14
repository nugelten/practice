using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Êîìïîíåíòè")]
    public CharacterController controller;
    public Transform playerCamera;

    [Header("Íàëàøòóâàííÿ ðóõó")]
    public float speed = 10f;
    public float gravity = -19.62f; // Çåìíà ãðàâ³òàö³ÿ x2 äëÿ "âàæêîãî" â³ä÷óòòÿ
    public float jumpHeight = 2.5f;

    [Header("Íàëàøòóâàííÿ îãëÿäó")]
    public float mouseSensitivity = 30f;

    [Header("Çâóêè")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f; 
    private float stepTimer;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isGrounded;

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            // Ôîðìóëà ô³çè÷íîãî ñòðèáêà
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Âèêëèêàºòüñÿ ç Player Input (Action: Move)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Âèêëèêàºòüñÿ ç Player Input (Action: Look)
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Start()
    {
        // Õîâàºìî êóðñîð ï³ä ÷àñ ãðè
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // --- ÎÁÅÐÒÀÍÍß (ÌÈØÀ) ---
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime * 0.1f;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime * 0.1f;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ùîá íå êðóòèòè ãîëîâó íà 360 ãðàäóñ³â

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        
        // --- ÐÓÕ (WASD) ---
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        HandleFootsteps(move);

        // --- ÃÐÀÂ²ÒÀÖ²ß ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ïðèòèñêàºìî äî çåìë³
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleFootsteps(Vector3 moveDirection)
    {
        if (isGrounded && moveDirection.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayRandomFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
    private void PlayRandomFootstep()
    {
        if (footstepClips.Length > 0)
        {
            int index = Random.Range(0, footstepClips.Length);
            audioSource.PlayOneShot(footstepClips[index]);
        }
    }
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Êîìïîíåíòè")]
    public CharacterController controller;
    public Transform playerCamera;

    [Header("Íàëàøòóâàííÿ ðóõó")]
    public float speed = 10f;
    public float gravity = -19.62f; // Çåìíà ãðàâ³òàö³ÿ x2 äëÿ "âàæêîãî" â³ä÷óòòÿ
    public float jumpHeight = 2.5f;

    [Header("Íàëàøòóâàííÿ îãëÿäó")]
    public float mouseSensitivity = 30f;

    [Header("Çâóêè")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f; 
    private float stepTimer;

    private Vector2 moveInput;
    private Vector2 lookInput;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isGrounded;

    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            // Ôîðìóëà ô³çè÷íîãî ñòðèáêà
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Âèêëèêàºòüñÿ ç Player Input (Action: Move)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Âèêëèêàºòüñÿ ç Player Input (Action: Look)
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Start()
    {
        // Õîâàºìî êóðñîð ï³ä ÷àñ ãðè
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // --- ÎÁÅÐÒÀÍÍß (ÌÈØÀ) ---
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ùîá íå êðóòèòè ãîëîâó íà 360 ãðàäóñ³â

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        
        // --- ÐÓÕ (WASD) ---
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        HandleFootsteps(move);

        // --- ÃÐÀÂ²ÒÀÖ²ß ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Ïðèòèñêàºìî äî çåìë³
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleFootsteps(Vector3 moveDirection)
    {
        if (isGrounded && moveDirection.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayRandomFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }
    private void PlayRandomFootstep()
    {
        if (footstepClips.Length > 0)
        {
            int index = Random.Range(0, footstepClips.Length);
            audioSource.PlayOneShot(footstepClips[index]);
        }
    }
}