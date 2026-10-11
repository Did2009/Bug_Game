using UnityEngine;

public class CharacterManager : MonoBehaviour, IDamagable
{
    // References
    PlayerInputManager inputManager;

    [Header("Player Health")]
    [SerializeField] int maxHP = 3;
    [SerializeField] int currentHP;
    public bool isDead;

    [Header("Character Flags")]
    public bool isDoingAction;

    [Header("Ground Check Variables")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    [Header("Character Selection Variables")]
    public int selectedCharacter = 1;
    public GameObject stinkbug;
    public GameObject rollyPolly;
    public GameObject butterfly;

    private void Start()
    {
        // Asign relevant references within the code
        inputManager = GetComponent<PlayerInputManager>();
        
        // Start with selected character
        SwitchCharacters();

        // Set Starting Health
        currentHP = maxHP;
        isDead = false;
    }

    private void Update()
    {
        if (!isDead)
        {
            // Logic to handle character switching
            HandleCharacterSwitching();
        }
    }

    // Method that checks if the player is touching the ground or not
    // Returns a true or false result
    public bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    // Method to handle character switching logic
    private void HandleCharacterSwitching()
    {
        // When switching characters is activated:
        if (inputManager.switchCharacters)
        {
            // Deactivate the input so it doesn't repeat
            inputManager.switchCharacters = false;

            // Makes sure the player can't change characters while doing an action
            if (!isDoingAction)
            {
                // Continue only if the player is not moving and is grounded
                if (inputManager.movementInputX == 0 && IsGrounded())
                {
                    // Make sure the character selection remains between 1 and 3
                    if (selectedCharacter == 3)
                    {
                        selectedCharacter = 1;
                    }
                    else
                    {
                        selectedCharacter++;
                    }
                    // Switch the character according to selected character
                    SwitchCharacters();
                }
            }
        }
    }

    // Method to switch characters
    private void SwitchCharacters()
    {
        switch (selectedCharacter)
        {
            case 1:
                // Reset Stinkbug spray
                inputManager.preformSpray = false;
                // Set Stinkbug as active character
                stinkbug.SetActive(true);
                rollyPolly.SetActive(false);
                butterfly.SetActive(false);
                break;
            case 2:
                // Reset Rolly Polly Launch
                inputManager.isChoosingLaunchDirection = false;
                inputManager.preformLaunch = false;
                // Set Rolly Polly as active character
                stinkbug.SetActive(false);
                rollyPolly.SetActive(true);
                butterfly.SetActive(false);
                break;
            case 3:
                // Set Butterfly as active character
                stinkbug.SetActive(false);
                rollyPolly.SetActive(false);
                butterfly.SetActive(true);
                break;
            default:
                Debug.Log("Error");
                break;
        }

    }

    void IDamagable.TakeDamage(int howMuch)
    {
        currentHP = currentHP - howMuch;
    }

    void IDamagable.Die()
    {
        isDead = true;
        currentHP = 0;
    }
}

