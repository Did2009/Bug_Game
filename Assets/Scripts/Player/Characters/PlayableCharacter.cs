using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour
{
    // References
    protected PlayerInputManager inputManager;
    protected CharacterManager characterManager;
    protected Rigidbody2D parentRb;
    protected Animator animator;

    // Variables
    [Header("Character Variables")]
    [SerializeField] protected float movementSpeed;
    [SerializeField] protected float jumpingPower;

    

    protected virtual void Start()
    {
        // Asign shared references within the code
        inputManager = GetComponentInParent<PlayerInputManager>();
        characterManager = GetComponentInParent<CharacterManager>();
        parentRb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
    }

    protected virtual void Update()
    {
        HandleMovementAndKnockback();

        if (!characterManager.isDead)
        {
            HandleSpriteDirections(); 
        }
    }

    // Method to move the player left and right according to player inputs
    // Movement speed is different for each bug
    protected virtual void HandleMovementAndKnockback()
    {
        // Disable player movement when knocked back from damage
        if (characterManager.knockbackCounter <= 0)
        {
            // Disable player movement when doing an action
            if (!characterManager.isDoingAction)
            {
                // Stops the player from moving when dead, but enables the final pushback
                if (!characterManager.isDead)
                {
                    // Normal player movement
                    parentRb.linearVelocity = new Vector2(inputManager.movementInputX * movementSpeed, parentRb.linearVelocity.y);
                }
            }
        }
        // If knockback is in effect
        else
        {
            if (characterManager.knockFromRight)
            {
                // Apply negative knockback movement
                parentRb.linearVelocity = new Vector2(-characterManager.knockbackForce, characterManager.knockbackForce);
            }
            if (!characterManager.knockFromRight)
            {
                // Apply knockback movement
                parentRb.linearVelocity = new Vector2(characterManager.knockbackForce, characterManager.knockbackForce);
            }

            // Lower timer
            characterManager.knockbackCounter -= Time.deltaTime;
        }
        
    }

    // Method to make the player jump
    // Jumping power is different for each bug
    protected virtual void HandleJumping()
    {
        if (inputManager.isJumping)
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
