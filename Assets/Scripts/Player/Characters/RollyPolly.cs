using UnityEngine;

public class RollyPolly : PlayableCharacter
{
    // References:
    SpriteRenderer spriteRenderer;

    [Header("Launching Variables")]
    public SpriteRenderer launchingArrowRenderer;

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
    }

    protected override void Update()
    {
        base.Update();

        // Mechanics to 
        if (inputManager.preparingToLaunchRollyPolly)
        {
            parentRb.linearVelocity = new Vector2(0, 0);
            spriteRenderer.sprite = circleSprite;
            launchingArrowRenderer.enabled = true;
            characterManager.isDoingAction = true;
        }
        else
        {
            spriteRenderer.sprite = squareSprite;
            launchingArrowRenderer.enabled = false;
            characterManager.isDoingAction = false;
        }
    }
}
