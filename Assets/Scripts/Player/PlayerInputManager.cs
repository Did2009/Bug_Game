using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    // References
    public PlayerControls controls;

    // Variables
    [Header("General Player Inputs")]
    public float movementInputX;
    public bool isJumping;
    public bool switchCharacters;
    [Header("Rolly Polly-related Inputs")]
    public bool isChoosingLaunchDirection;
    public bool preformLaunch;
    [Header("Stinkbug-related Inputs")]
    public bool preformSpray;

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
        // Jump:
        controls.Player.Jump.performed += i => isJumping = true;
        // Switch Characters:
        controls.Player.SwitchCharacters.performed += i => switchCharacters = true;
        // Launch Rolly Polly:
        controls.Player.RollyPollyLaunch.performed += i => isChoosingLaunchDirection = true;
        controls.Player.RollyPollyLaunch.canceled += i => isChoosingLaunchDirection = false;
        controls.Player.RollyPollyLaunch.canceled += i => preformLaunch = true;
        // Stinkbug Spray:
        controls.Player.StinkbugSpray.performed += i => preformSpray = true;
    }

    private void OnDisable()
    {
        // Disable the PlayerControls when the object is inactive
        controls.Player.Disable();
    }
}
