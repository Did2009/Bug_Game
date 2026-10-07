using UnityEngine;

public class RollyPolly : PlayableCharacter
{
    protected override void Start()
    {
        base.Start();

        // Determines the rolly polly's movement speed
        movementSpeed = 6;
        // Determines the rolly polly's jumping power
        jumpingPower = 5;
    }

    protected override void Update()
    {
        base.Update();
    }
}
