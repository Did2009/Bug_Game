using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Trap : MonoBehaviour, IDamagable
{
    // References
    SpriteRenderer spriteRenderer;

    [Header("Trap Settings")]
    public int health = 1;
    [SerializeField] protected int damageToInflict = 1;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Method that is called when a trap deals damage to the player
    protected virtual void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(damageToInflict);
    }

    // Method that is called when a trap is damaged
    void IDamagable.TakeDamage(int howMuch)
    {
        health = health - howMuch;
    }

    // Method that is called when a trap is destroyed
    void IDamagable.Die()
    {
        health = 0;
        Object.Destroy(this.gameObject);
        // StartCoroutine(FadeThenDestroy());
    }

    // CURRENTLY UNUSED
    IEnumerator FadeThenDestroy()
    {
        float alphaValue = spriteRenderer.color.a;
        Color alteredColor = spriteRenderer.color;

        while (spriteRenderer.color.a < 1)
        {
            alphaValue += 0.01f;
            alteredColor.a = alphaValue;
            spriteRenderer.color = alteredColor;

            yield return new WaitForSeconds(0.05f); // update interval
        }

        if (spriteRenderer.color.a <= 1)
        {
            Debug.Log("Trap Destroyed");
            Object.Destroy(this.gameObject);
            yield return null;
        }
    }

    // Method that activates when an object collides with a trap
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Asign the colliding GameObject to a variable
        GameObject whatIDamage = collision.gameObject;       

        // Check if the colliding object's tag is "Player", if so:
        if (whatIDamage.CompareTag("Player"))
        {
            // Set IDamagable and CharacterManager to variables
            IDamagable playerDamagable = whatIDamage.GetComponent<IDamagable>();
            CharacterManager characterManager = whatIDamage.GetComponentInParent<CharacterManager>();

            // Apply damage to player
            ApplyDamage(playerDamagable);

            // Check if player's HP is zero or lower, if so:
            if (characterManager.currentHP <= 0)
            {
                // Activate player death
                playerDamagable.Die();
            }
        }
    }
}
