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
        if (!characterManager.isDead)
        {
            HandleMovement();
            HandleSpriteDirections();

            if (inputManager.isJumping)
            {
                HandleJump();
            }
        }
    }

    // Method to move the player left and right according to player inputs
    // Movement speed is different for each bug
    protected virtual void HandleMovement()
    {
        // Makes sure the player can't move while doing an action
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

    // Method to change the sprite directions to face when the player is moving
    protected virtual void HandleSpriteDirections()
    {
        if (!characterManager.isDoingAction)
        {
            // If the player is not moving:
            if (inputManager.movementInputX == 0)
            {
                // Keep the sprites' direction exactly as it was before
                characterManager.transform.eulerAngles = characterManager.transform.eulerAngles;
            }
            // If the player is moving right
            else if (inputManager.movementInputX > 0)
            {
                // Make the sprites look to the right
                characterManager.transform.eulerAngles = Vector3.zero;
            }
            // If the player is moving left
            else
            {
                // Make the sprites look to the left
                characterManager.transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            // Keep the sprites' direction exactly as it was before
            characterManager.transform.eulerAngles = characterManager.transform.eulerAngles;
        }
    }
}
