using UnityEngine;

public class Collectible : MonoBehaviour
{
    // References
    Rigidbody2D rb;
    GameObject web;

    [Header("Web Spawning Variables")]
    [SerializeField] bool isOnWeb;
    [SerializeField] bool isTrapped;

    private void Start()
    {
        // Assign references
        rb = GetComponent<Rigidbody2D>();

        isTrapped = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        CheckIfTrapped();
    }

    // Method to handle ants spawned on webs
    private void CheckIfTrapped()
    {
        // If the ant is spawned on a web
        if (isOnWeb)
        {
            // Turn on the trapped flag
            isTrapped = true;

            // If the web is destroyed
            if (web == null)
            {
                // turn off the trapped flag
                isTrapped = false;
            }

            // If the trapped flag is on, then:
            if (isTrapped)
            {
                // Make the ant rigidbody be kinematic
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
            else
            {
                // Make the ant rigidbody be dynamic
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    // Mehtod called when a collider enters a trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Assign the collider's game object to a variable
        GameObject collidingObject = collision.gameObject;

        // If the game object is a web, then:
        if (collidingObject.CompareTag("Web"))
        {
            // Turn on the web flag
            isOnWeb = true;
            // Store web game object to variable
            web = collidingObject;
        }

        // If the game object is the player
        if (collidingObject.CompareTag("Player"))
        {
            // Destroy this game object
            GameObject.Destroy(gameObject);
        }
    }   
}
