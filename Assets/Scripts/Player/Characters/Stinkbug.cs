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

        // Hide spray components at the beginning
        spraySpriteRenderer.enabled = false;
        sprayBoxCollider.enabled = false;
    }

    protected override void Update()
    {
        base.Update();

        HandleSpray();
    }

    private void HandleSpray()
    {
        if (inputManager.preformSpray)
        {
            // Turn off input so it doesn't repeat
            inputManager.preformSpray = false;

            spraySpriteRenderer.enabled = true;
            sprayBoxCollider.enabled = true;
        }
    }
}
