using System.Collections;
using UnityEngine;

public class Stinkbug : PlayableCharacter
{
    [Header("Spray Variables")]
    public SpriteRenderer spraySpriteRenderer;
    public Animator sprayAnimator;
    public BoxCollider2D sprayBoxCollider;

    protected override void Start()
    {
        base.Start();

        // Determines the stinkbug's movement speed
        movementSpeed = 5;
        // Determines the stinkbug's jumping power
        jumpingPower = 6.5f;

        // Disable the spray's components
        spraySpriteRenderer.enabled = false;
        sprayBoxCollider.enabled = false;
    }

    protected override void Update()
    {
        base.Update();
        if (!characterManager.isDead)
        {
            HandleJumping();

            HandleSpray();
        }
    }

    // Method to handle the stinbug's spray
    private void HandleSpray()
    {
        if (inputManager.preformSpray)
        {
            // Turn off input so it doesn't repeat
            inputManager.preformSpray = false;

            // Enable spray only when character is grounded
            if (characterManager.IsGrounded() && characterManager.selectedCharacter == 1)
            {
                // Activate is doing action flag
                characterManager.isDoingAction = true;

                // Play Spray animation
                animator.Play("Spray");
            }
        }
    }

    // Method timed with animation to activate spray
    public void StartSpray()
    {
        // Enable the spray's components
        spraySpriteRenderer.enabled = true;
        sprayBoxCollider.enabled = true;

        // Play the spray
        sprayAnimator.Play("Spray");

        // Start coroutine to disable spray visibilty
        StartCoroutine("WaitThenDisappear");
    }

    // Coroutine to limit spray visibility length
    IEnumerator WaitThenDisappear()
    {
        // Wait for 0.15 seconds
        yield return new WaitForSeconds(sprayAnimator.GetCurrentAnimatorClipInfo(0).Length);

        // Disable the spray's components
        sprayBoxCollider.enabled = false;
        spraySpriteRenderer.enabled = false;
    }
}
