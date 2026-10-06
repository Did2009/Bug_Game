using UnityEngine;

public class Stinkbug : PlayableCharacter
{
    protected override void Start()
    {
        base.Start();
        speed = 5;
    }

    private void Update()
    {
        HandleMovement();
    }
}
