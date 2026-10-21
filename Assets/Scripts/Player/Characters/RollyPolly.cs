using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RollyPolly : PlayableCharacter
{
    // References:
    BoxCollider2D boxCollider;
    CircleCollider2D circleCollider;

    [Header("Launching Flags")]
    public bool isCurrentlyChoosingDierction;
    [SerializeField] bool isLaunching;
    public bool readyToLaunch;

    [Header("Launching Variables")]
    [SerializeField] Vector3 launchDirection;
    [SerializeField] float launchPower;
    [SerializeField] float launchTime;
    public SpriteRenderer arrowRenderer;
    float launchTimeReset;
    

    protected override void Start()
    {
        base.Start();

        // Assign References:
        boxCollider = GetComponent<BoxCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();

        // Determines the rolly polly's movement speed
        movementSpeed = 6;
        // Determines the rolly polly's jumping power
        jumpingPower = 5;

        // Set starting collision and arrow visibility
        arrowRenderer.enabled = false;
        boxCollider.enabled = true;
        circleCollider.enabled = false;

        // Set the launch time reset
        launchTimeReset = launchTime;
    }

    protected override void Update()
    {
        base.Update();

        if (!characterManager.isDead)
        {
            HandleJumping();
            HandleJumpingColliders();

            HandlelaunchingPreparation();
            PerformLaunch();
        } 
    }

    private void HandlelaunchingPreparation()
    {
        // The player can only preform a launch when touching the ground
        if (characterManager.IsGrounded())
        {
            // While the player is holding the right mouse key:
            if (inputManager.chooseLaunchDirection)
            {
                // Turn off input so it doesn't repeat
                inputManager.chooseLaunchDirection = false;
                // Turn on isCurrentlyChoosingDirection (Used for Forcing the player to wait for animation to finish)
                isCurrentlyChoosingDierction = true;

                // The player stops moving
                parentRb.linearVelocity = Vector2.zero;

                // The action flag is activated, stopping the player from doing other actions
                characterManager.isDoingAction = true;

                // Play curling animation
                animator.Play("Curl");
                animator.SetBool("isGrounded", false);
            }

            // When the player releases the right mouse key, and is ready to launch:
            else if (inputManager.preformLaunch && readyToLaunch)
            {
                // turn off inputs so it doesn't repeat
                inputManager.preformLaunch = false;
                isCurrentlyChoosingDierction = false;
                readyToLaunch = false;

                // Switch animation state to fall
                animator.SetBool("launched", true);

                // The choosing direction arrow disappears
                arrowRenderer.enabled = false;

                // Used to get the mouse position on screen
                Vector3 mousePosition = Mouse.current.position.ReadValue();

                // Calculations to determine launch direction
                launchDirection = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
                launchDirection.z = 0;
                launchDirection = launchDirection.normalized;

                // Activate the launch
                isLaunching = true;
                // Turn on invicibility
                characterManager.isInvincible = true;
            }

            // When the player releases the right mouse key, and is not ready to launch:
            else if (inputManager.preformLaunch && !readyToLaunch)
            {
                // Cancel input so it doesn't repeat
                inputManager.preformLaunch = false;
                isCurrentlyChoosingDierction = false;

                // Turn off is doing action flag, so the player can continue doing other actions
                characterManager.isDoingAction = false;
            }

            // When the player is ready to launch (finished the curl animation)
            if (readyToLaunch)
            {
                // An arrow to choose direction appears
                arrowRenderer.enabled = true;
            }
        }
    }

    private void PerformLaunch()
    {
        // When a launch is preformed
        if (isLaunching)
        {
            // Change rotation of sprite in launch direction
            if (launchDirection.x > 0)
            {
                characterManager.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                characterManager.transform.eulerAngles = new Vector3(0, 180, 0);
            }

            // While timer is above 0
            if (launchTime > 0)
            {
                // Lower the timer
                launchTime -= Time.deltaTime;
                // Move the player in the directiopn of the launch
                parentRb.linearVelocity = launchDirection * launchPower;
            }
            // When the timer reaches 0, the launch is done:
            else
            {
                // Switch animation state to fall
                animator.SetBool("launched", false);
                // Cancel input so it doesn't repeat
                isLaunching = false;
                // Turn off invicibility
                characterManager.isInvincible = false;
                // Cancel action flag so the player can make other actions
                characterManager.isDoingAction = false;

                // Reset the timer
                launchTime = launchTimeReset;
                // Return the player's usual movement controls
                parentRb.linearVelocity = new Vector2(parentRb.linearVelocity.x, 0);
            }
        }
    }

    private void HandleJumpingColliders()
    {
        if (!characterManager.isDoingAction)
        {
            if (characterManager.IsGrounded())
            {
                boxCollider.enabled = true;
                circleCollider.enabled = false;
            }
            else if (!characterManager.IsGrounded())
            {
                boxCollider.enabled = false;
                circleCollider.enabled = true;
            }
        }
    }
}
