using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    // References
    PlayerInputManager inputManager;

    [Header("Ground Check Variables")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    [Header("Character Selection Variables")]
    [SerializeField] int selectedCharacter = 1;
    public GameObject stinkbug;
    public GameObject rollyPolly;
    public GameObject butterfly;

    private void Start()
    {
        // Asign relevant references within the code
        inputManager = GetComponent<PlayerInputManager>();
        
        // Start with selected character
        HandleCharacterSwitching();
    }

    private void Update()
    {
        // Logic to handle character switching
        if (inputManager.switchCharacters)
        {
            // Deactivate the input so it doesn't repeat
            inputManager.switchCharacters = false;

            // Makes sure the character selection remains between 1 and 3
            if (selectedCharacter == 3)
            {
                selectedCharacter = 1;
            }
            else
            {
                selectedCharacter++;
            }

            // Switch the character according to logic
            HandleCharacterSwitching();
        }
    }

    // Method that checks if the player is touching the ground or not
    // Returns a true or false result
    public bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    // Method to switch characters
    private void HandleCharacterSwitching()
    {
        switch (selectedCharacter)
        {
            case 1:
                Debug.Log("Stinkbug Selected");
                stinkbug.SetActive(true);
                rollyPolly.SetActive(false);
                butterfly.SetActive(false);
                break;
            case 2:
                Debug.Log("Rolly Polly Selected");
                stinkbug.SetActive(false);
                rollyPolly.SetActive(true);
                butterfly.SetActive(false);
                break;
            case 3:
                Debug.Log("Butterfly Selected");
                stinkbug.SetActive(false);
                rollyPolly.SetActive(false);
                butterfly.SetActive(true);
                break;
            default:
                Debug.Log("Error");
                break;
        }
    }
}

