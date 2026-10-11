using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour
{
    // References
    protected PlayerInputManager inputManager;
    protected CharacterManager characterManager;
    protected Rigidbody2D parentRb;

    // Variables
    [Header("Character Variables")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float jumpingPower;

    

    protected virtual void Start()
    {
        // Asign relevant references within the code
        inputManager = GetComponentInParent<PlayerInputManager>();
        characterManager = GetComponentInParent<CharacterManager>();
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
        // Makes sure the player can't move while doinf an action
        if (!characterManager.isDoingAction)
        {
            parentRb.linearVelocity = new Vector2(inputManager.movementInputX * movementSpeed, parentRb.linearVelocity.y);
        }
    }

    // Method to make the player jump
    // Jumping power is different for each bug
    protected virtual void HandleJump()
    {
        // Turns off input
        inputManager.isJumping = false;

        // Makes sure the player can't jump while doing an action
        if (!characterManager.isDoingAction)
        {
            // Checks if the player is grounded
            // Prevents the player from jumping in mid-air
            if (characterManager.IsGrounded())
            {
                // Makes the player jump
                parentRb.linearVelocity = new Vector2(parentRb.linearVelocity.x, jumpingPower);
            }
        }
    }
}
