using UnityEngine;

public class FinishGame : MonoBehaviour
{
    public VictoryScreenManager victoryScreenManager;
    public PlayerInputManager inputManager;
    public CharacterManager characterManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entering Window!");
            // Disable player inputs
            inputManager.enabled = false;
            // Stop player from moving and doing actions
            characterManager.isDoingAction = true;
            characterManager.rb.linearVelocity = Vector2.zero;

            // Show the victory screen
            victoryScreenManager.ShowVictoryScreen();
        }
    }
}
