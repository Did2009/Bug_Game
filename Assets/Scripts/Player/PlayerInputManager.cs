using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    // References
    public PlayerControls controls;

    // Input Varaibles
    public float movementInputX;

    private void Awake()
    {
        // Create an instance of the PlayerControls
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Enable the PlayerControls when the object is active
        controls.Player.Enable();

        // Assign PlayerControls Inputs to relevant variables:
        // Movement:
        controls.Player.Movement.performed += i => movementInputX = i.ReadValue<Vector2>().x;
    }

    private void OnDisable()
    {
        // Disable the PlayerControls when the object is inactive
        controls.Player.Disable();
    }
}
