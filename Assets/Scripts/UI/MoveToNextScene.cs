using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    // Referneces
    SpriteRenderer spriteRenderer;

    public PlayerUIManager playerUIManager;
    public CharacterManager characterManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set references
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Disable sprite renderer
        spriteRenderer.enabled = false;
    }

    // Method used to load the next scene when the player enters the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Remember which character was selected at the time
            playerUIManager.savedSelectedCharacter = characterManager.selectedCharacter;

            SceneManager.LoadScene(2);
        }
    }
}
