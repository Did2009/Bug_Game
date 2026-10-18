using System.Collections;
using UnityEngine;

public class Stinkbug : PlayableCharacter
{
    [Header("Spray Settings")]
    public SpriteRenderer spraySpriteRenderer;
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

        HandleSpray();
    }

    // Method to handle the stinbug's spray
    private void HandleSpray()
    {
        if (inputManager.preformSpray)
        {
            // Turn off input so it doesn't repeat
            inputManager.preformSpray = false;

            // Enable the spray's components
            spraySpriteRenderer.enabled = true;
            sprayBoxCollider.enabled = true;

            // Start coroutine to disable spray visibilty
            StartCoroutine("WaitThenDisappear");
        }
    }

    // Coroutine to limit spray visibility length
    IEnumerator WaitThenDisappear()
    {
        // Wait for 0.15 seconds
        yield return new WaitForSeconds(0.15f);

        // Disable the spray's components
        sprayBoxCollider.enabled = false;
        spraySpriteRenderer.enabled = false;
    }
}
