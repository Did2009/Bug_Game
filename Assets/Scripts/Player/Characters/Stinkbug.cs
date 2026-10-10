using UnityEngine;

public class Stinkbug : PlayableCharacter
{
    protected override void Start()
    {
        base.Start();

        // Determines the stinkbug's movement speed
        movementSpeed = 5;
        // Determines the stinkbug's jumping power
        jumpingPower = 6.5f;
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
            inputManager.preformSpray = false;
            Debug.Log("Preformed Spray!");
        }
    }
}
