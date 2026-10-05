using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour
{
    // References
    private PlayerInputManager inputManager;
    private Rigidbody2D rb;

    // Movement Variables
    protected int speed;

    private void Start()
    {
        // Asign relevant references within the code
        inputManager = GetComponent<PlayerInputManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Called individually in each playable bug
    protected void Movement()
    {
        // rb.linearVelocity = new Vector2(inputManager.movementInputX * speed, 0);
    }
}
