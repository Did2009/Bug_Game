using System.Collections;
using UnityEngine;

public class Butterfly : PlayableCharacter
{
    [Header("Flying Variables")]
    [SerializeField] int numberOfFlaps = 0;

    protected override void Start()
    {
        base.Start();

        // Determines the butterfly's movement speed
        movementSpeed = 4;
        // Determines the butterfly's jumping power
        jumpingPower = 7;
    }

    protected override void Update()
    {
        if (!characterManager.isDead)
        {
            HandleMovement();
            HandleSpriteDirections();
            HandleFlying();
        }
    }

    private void HandleFlying()
    {
        if (inputManager.isJumping)
        {
            // Turns off input
            inputManager.isJumping = false;
            //StartCoroutine(FlyingCooldown());

            if (characterManager.IsGrounded())
            {
                numberOfFlaps = 1;
                // Makes the player jump
                parentRb.linearVelocity = new Vector2(parentRb.linearVelocity.x, jumpingPower);
            }
            else if (numberOfFlaps == 1 || numberOfFlaps == 2)
            {
                numberOfFlaps++;
                // Makes the player jump
                parentRb.linearVelocity = new Vector2(parentRb.linearVelocity.x, jumpingPower);
            }
        }
    }
}
