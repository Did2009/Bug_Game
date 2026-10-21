using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextScene : MonoBehaviour
{
    // Referneces
    DataManager dataManager;
    SpriteRenderer spriteRenderer;

    public PlayerUIManager playerUIManager;
    public CharacterManager characterManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Set references
        dataManager = FindAnyObjectByType<DataManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Disable sprite renderer
        spriteRenderer.enabled = false;
    }

    // Method used to load the next scene when the player enters the trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Store current character, score, and HP to Data Manager
            dataManager.StoreSavedCharacter(characterManager.selectedCharacter);
            dataManager.StoreSavedScore(playerUIManager.score);
            dataManager.StoreSavedHP(playerUIManager.currentHP);

            SceneManager.LoadScene(2);
        }
    }
}
