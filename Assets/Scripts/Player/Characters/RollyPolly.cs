using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RollyPolly : PlayableCharacter
{
    // References:
    SpriteRenderer spriteRenderer;

    [Header("Launching Variables")]
    [SerializeField] bool isLaunching;
    [SerializeField] Vector3 launchDirection;
    [SerializeField] float launchPower;
    [SerializeField] float launchTime;
    float launchTimeReset;
    public SpriteRenderer arrowRenderer;
    [SerializeField] bool finishedLaunch;

    [Header("Temporary Sprites")]
    public Sprite squareSprite;
    public Sprite circleSprite;

    protected override void Start()
    {
        base.Start();

        // Assign References:
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Determines the rolly polly's movement speed
        movementSpeed = 6;
        // Determines the rolly polly's jumping power
        jumpingPower = 5;

        arrowRenderer.enabled = false;

        // Set the launch time reset
        launchTimeReset = launchTime;
    }

    protected override void Update()
    {
        base.Update();

        if (!characterManager.isDead)
        {
            HandleLaunch();
        } 
    }

    private void HandleLaunch()
    {
        // The player can only preform a launch when touching the ground
        if (characterManager.IsGrounded())
        {
            // While the player is holding the right mouse key:
            if (inputManager.isChoosingLaunchDirection)
            {
                // The player stops moving
                parentRb.linearVelocity = Vector2.zero;
                // The action flag is activated, stopping the player from doing other actions
                characterManager.isDoingAction = true;
                // An arrow to choose direction appears
                arrowRenderer.enabled = true;
                // TEMPORARY: The player turns into a ball
                spriteRenderer.sprite = circleSprite;
            }
            // When the player releases the right mouse key:
            else if (inputManager.preformLaunch)
            {
                // Cancel input so it doesn't repeat
                inputManager.preformLaunch = false;
                // The choosing direction arrow disappears
                arrowRenderer.enabled = false;

                // Used to get the mouse position on screen
                Vector3 mousePosition = Mouse.current.position.ReadValue();

                // Calculations to determine launch direction
                launchDirection = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
                launchDirection.z = 0;
                launchDirection = launchDirection.normalized;

                /* Used to change rotation of sprite, enable when sprites are used
                if (launchDirection.x > 0)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                */

                // Activate the launch
                isLaunching = true;
            }
        }

        // When a launch is preformed
        if (isLaunching)
        {
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
                // Cancel input so it doesn't repeat
                isLaunching = false;
                // Cancel action flag so the player can make other actions
                characterManager.isDoingAction = false;
                // Reset the timer
                launchTime = launchTimeReset;
                // Return the player's usual movement controls
                parentRb.linearVelocity = new Vector2(parentRb.linearVelocity.x, 0);
                finishedLaunch = true;
            }
        }

        // After the launch is preformed
        if (finishedLaunch)
        {
            // When the character touches the ground:
            if (characterManager.IsGrounded())
            {
                // Cancel input so it doesn't repeat
                finishedLaunch = false;

                // TEMPORARY: The player turns back into a square
                spriteRenderer.sprite = squareSprite;
            }
        }
    }
}
