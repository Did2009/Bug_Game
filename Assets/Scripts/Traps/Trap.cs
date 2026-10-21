using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Trap : MonoBehaviour
{
    [Header("Trap Settings")]
    public int health = 1;
    [SerializeField] protected int damageToInflict = 1;

    // Method that is called when a trap deals damage to the player
    protected virtual void ApplyDamageToPlayer(IDamagable damagable, CharacterManager characterManager, Collision2D collision)
    {
        // Start knockback
        characterManager.knockbackCounter = characterManager.knockbackTotalTime;

        // Decide to which side pushback occurs
        if (collision.transform.position.x <= transform.position.x)
        {
            characterManager.knockFromRight = true;
        }
        if (collision.transform.position.x > transform.position.x)
        {
            characterManager.knockFromRight = false;
        }

        // Make player take damage
        damagable.TakeDamage(damageToInflict);
    }

    // Method that activates when an object collides with a trap
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Assign the colliding GameObject to a variable
        GameObject whatIDamage = collision.gameObject;       

        // Check if the colliding object's tag is "Player", if so:
        if (whatIDamage.CompareTag("Player"))
        {
            // Assign IDamagable and CharacterManager to variables
            IDamagable playerDamagable = whatIDamage.GetComponent<IDamagable>();
            CharacterManager characterManager = whatIDamage.GetComponentInParent<CharacterManager>();   

            // Apply damage to player
            ApplyDamageToPlayer(playerDamagable, characterManager, collision);

            // Check if player's HP is zero or lower, if so:
            if (characterManager.playerUIManager.currentHP <= 0)
            {
                // Activate player death
                playerDamagable.Die();
            }
        }
    }
}
