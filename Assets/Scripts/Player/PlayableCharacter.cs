using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour
{
    // References
    protected PlayerInputManager inputManager;
    public Rigidbody2D parentRb;

    // Movement Variables
    [SerializeField] protected int speed;

    protected virtual void Start()
    {
        // Asign relevant references within the code
        inputManager = GetComponentInParent<PlayerInputManager>();
        parentRb = GetComponentInParent<Rigidbody2D>();
    }

    // Called individually in each playable bug
    protected virtual void HandleMovement()
    {
        parentRb.linearVelocity = new Vector2(inputManager.movementInputX * speed, parentRb.linearVelocity.y);
    }
}
