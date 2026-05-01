using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Компоненти")]
    public CharacterController controller;
    public Transform playerCamera;

    [Header("Налаштування руху")]
    public float speed = 10f;
    public float gravity = -19.62f; // Земна гравітація x2 для "важкого" відчуття
    public float jumpHeight = 2.5f;

    [Header("Налаштування огляду")]
    public float mouseSensitivity = 30f;

    [Header("Звуки")]
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
            // Формула фізичного стрибка
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Викликається з Player Input (Action: Move)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Викликається з Player Input (Action: Look)
    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
    }

    void Start()
    {
        // Ховаємо курсор під час гри
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // --- ОБЕРТАННЯ (МИША) ---
        float mouseX = lookInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = lookInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Щоб не крутити голову на 360 градусів

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        
        // --- РУХ (WASD) ---
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        HandleFootsteps(move);

        // --- ГРАВІТАЦІЯ ---
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Притискаємо до землі
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