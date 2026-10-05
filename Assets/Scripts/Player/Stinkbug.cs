using UnityEngine;

public class Stinkbug : PlayableCharacter
{
    private void Start()
    {
        speed = 5;
    }

    private void Update()
    {
        Movement();
    }
}
