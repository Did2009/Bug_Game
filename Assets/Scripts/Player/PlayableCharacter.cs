using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour
{
    // References
    PlayerInputManager inputManager;
    Rigidbody2D parentRb;

    // Variables
    [Header("Character Variables")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float jumpingPower;

    [Header("Jumping Mechanics")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    protected virtual void Start()
    {
        // Asign relevant references within the code
        inputManager = GetComponentInParent<PlayerInputManager>();
        parentRb = GetComponentInParent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        HandleMovement();

        if (inputManager.isJumping)
        {
            HandleJump();
        }
    }

    // Method to move the player left and right according to player inputs.
    // Movement speed is different for each bug
    protected virtual void HandleMovement()
    {
        parentRb.linearVelocity = new Vector2(inputManager.movementInputX * movementSpeed, parentRb.linearVelocity.y);
    }

    // Method to make the player jump
    // Jumping power is different for each bug
    protected virtual void HandleJump()
    {
        // Turns off input
        inputManager.isJumping = false;

        // Checks if the player is grounded
        // Prevents the player from jumping in mid-air
        if (IsGrounded())
        {
            // Makes the player jump
            parentRb.linearVelocity = new Vector2(parentRb.linearVelocity.x, jumpingPower);
        }
    }

    // Method that checks if the player is touching the ground or not
    // Returns a true or false result
    private bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }
}
