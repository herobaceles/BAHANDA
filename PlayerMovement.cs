using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    public float gravity = -9.81f;
    public float rotationSpeed = 10f;

    private Vector3 velocity;
    private CharacterController controller;

    [Header("Joystick")]
    public Joystick joystick;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // Try to find joystick if not assigned
        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
        }

        if (joystick == null)
        {
            Debug.LogWarning("Joystick not found in the scene. PlayerMovement will not work without it!");
        }
    }

    void Update()
    {
        // Ensure joystick exists
        if (joystick == null) return;

        // Read joystick input
        float moveX = joystick.Horizontal;
        float moveZ = joystick.Vertical;

        Vector3 move = new Vector3(moveX, 0, moveZ).normalized;

        if (move.magnitude >= 0.1f)
        {
            // Move player
            controller.Move(move * speed * Time.deltaTime);

            // Rotate smoothly toward movement direction
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // Apply gravity
        if (controller.isGrounded)
        {
            velocity.y = -2f; // small negative to keep grounded
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }
}
