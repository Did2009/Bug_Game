using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour, IDamagable
{
    [Header("References")]
    PlayerInputManager inputManager;
    public Rigidbody2D rb;
    public PlayerUIManager playerUIManager;
    public GameOverScreenManager gameOverScreenManager;
    public MoveToNextScene moveToNextScene;

    [Header("Character Flags")]
    public bool isDead;
    public bool isDoingAction;
    public bool isTouchingGround;
    public bool isInvincible;

    [Header("Ground Check Variables")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    [Header("Character Selection Variables")]
    public int selectedCharacter;
    public GameObject stinkbug;
    public GameObject rollyPolly;
    public GameObject butterfly;

    [Header("Knockback Variables")]
    public float knockbackForce = 3;
    public float knockbackCounter;
    public float knockbackTotalTime = 0.2f;
    public bool knockFromRight;

    private void Start()
    {

        // Asign relevant references within the code
        inputManager = GetComponent<PlayerInputManager>();
        rb = GetComponent<Rigidbody2D>();

        // Only randomize the character when restarting the level
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            // Select a random character to start with (1-3)
            selectedCharacter = Random.Range(1, 4);
        }
        // When moving to the window, keep the same character
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            // Set the selected character to the saved selected character
            selectedCharacter = playerUIManager.savedSelectedCharacter;
        }

        // Start with selected character
        SwitchCharacters();
    }

    private void Update()
    {
        if (!isDead)
        {
            HandleCharacterSwitching();
        }
    }

    // Method that checks if the player is touching the ground or not
    // Returns a true or false
    public bool IsGrounded()
    {
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(2, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    }

    // Method to handle character switching logic
    private void HandleCharacterSwitching()
    {
        // When switching characters is activated:
        if (inputManager.switchCharacters)
        {
            // Deactivate the input so it doesn't repeat
            inputManager.switchCharacters = false;

            // Makes sure the player can't change characters while doing an action, and they must be grounded
            if (!isDoingAction && inputManager.movementInputX == 0 && IsGrounded())
            {
                // This if...else statment makes sure the character selection remains between 1 and 3
                // If selected character is 3, then:
                if (selectedCharacter == 3)
                {
                    // Change selected character to 1
                    selectedCharacter = 1;
                }
                // If not:
                else
                {
                    // Increase selected character by 1
                    selectedCharacter++;
                }

                // Switch the character according to selected character
                SwitchCharacters();
            }
        }
    }

    // Method to switch characters
    private void SwitchCharacters()
    {
        // Enable the selected character
        switch (selectedCharacter)
        {
            case 1: // Stinkbug
                // Reset Stinkbug spray
                inputManager.preformSpray = false;

                // Set Stinkbug as active character
                stinkbug.SetActive(true);
                rollyPolly.SetActive(false);
                butterfly.SetActive(false);
                break;
            case 2: // Rolly polly
                // Reset Rolly Polly Launch
                inputManager.chooseLaunchDirection = false;
                inputManager.preformLaunch = false;

                // Set Rolly Polly as active character
                stinkbug.SetActive(false);
                rollyPolly.SetActive(true);
                butterfly.SetActive(false);
                break;
            case 3: // Butterfly
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

    // Method that is called whenever the player takes damage
    void IDamagable.TakeDamage(int howMuch)
    {
        if (!isDead)
        {
            // Decrease player health by input damage
            playerUIManager.currentHP = playerUIManager.currentHP - howMuch;
            // Start damage indication coroutine
            StartCoroutine(FlashRed());
            // Update health UI
            playerUIManager.UpdateHealth();
        }
    }

    // Coroutine that indicates the player has been damaged
    IEnumerator FlashRed()
    {
        // Turn the selected character's sprite red
        switch (selectedCharacter)
        {
            case 1:
                stinkbug.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 2:
                rollyPolly.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case 3:
                butterfly.GetComponent<SpriteRenderer>().color = Color.red;
                break;
            default:
                Debug.Log("Turn Red Error");
                break;
        }

        // Wait 0.15 seconds
        yield return new WaitForSeconds(0.15f);

        // Turn the selected character's sprite white
        switch (selectedCharacter)
        {
            case 1:
                stinkbug.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 2:
                rollyPolly.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            case 3:
                butterfly.GetComponent<SpriteRenderer>().color = Color.white;
                break;
            default:
                Debug.Log("Turn White Error");
                break;
        }
    }

    // Method that is called when the player dies
    void IDamagable.Die()
    {
        // Activate death flag, stopping all other methods
        isDead = true;

        // Set hp to zero
        playerUIManager.currentHP = 0;
        // Update health UI
        playerUIManager.UpdateHealth();

        // Stop player movement
        rb.linearVelocity = Vector3.zero;

        // Play the death animation
        PlayDeathAnimation();

        // Flip player on his back
        transform.eulerAngles = new Vector3(180, 0, 0);

        // Stops player from sliding
        rb.sharedMaterial = null;

        // Start coroutine for showing game over screen
        StartCoroutine(DelayBeforeShowingGameOverScreen());
    }

    void PlayDeathAnimation()
    {
        switch (selectedCharacter)
        {
            case 1:
                stinkbug.GetComponent<Animator>().Play("Death");
                break;
            case 2:
                rollyPolly.GetComponent<Animator>().Play("Death");
                break;
            case 3:
                butterfly.GetComponent<Animator>().Play("Death");
                break;
            default:
                Debug.Log("Death Animation Error");
                break;
        }
    }

    // Coroutine that shows the game over screen after a delay
    IEnumerator DelayBeforeShowingGameOverScreen()
    {
        yield return new WaitForSeconds(1.5f);

        gameOverScreenManager.ShowGameOverScreen();

        yield return null;
    }

    // Method that is called when the player deals damage to a trap
    public void ApplyDamageToTrap(IDamagable damagable)
    {
        damagable.TakeDamage(1);
    }

    // Method that activates when the Stinkbug sprays
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Asign the colliding GameObject to a variable
        GameObject whatIDamage = collision.gameObject;

        // Check if the colliding object's tag is "Web", if so:
        if (whatIDamage.CompareTag("Web"))
        {
            // Set the web's IDamagable and Web script to variables
            IDamagable webDamagable = whatIDamage.GetComponent<IDamagable>();
            Web webScript = whatIDamage.GetComponent<Web>();

            // Apply damage to the web
            ApplyDamageToTrap(webDamagable);

            // Check if the web's health is zero or lower, if so:
            if (webScript.health <= 0)
            {
                // Activate web death
                webDamagable.Die();
            }
        }
    }

    // Method that activates when the rolly polly launches into a burr
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Asign the colliding GameObject to a variable
        GameObject whatIDamage = collision.gameObject;

        // Check if the colliding object's tag is "Web", if so:
        if (whatIDamage.CompareTag("Burr") && isInvincible)
        {
            // Set the web's IDamagable and Web script to variables
            IDamagable burrDamagable = whatIDamage.GetComponent<IDamagable>();
            Burr burrScript = whatIDamage.GetComponent<Burr>();

            // Apply damage to the web
            ApplyDamageToTrap(burrDamagable);

            // Check if the web's health is zero or lower, if so:
            if (burrScript.health <= 0)
            {
                // Activate web death
                burrDamagable.Die();
            }
        }
    }
}

