using UnityEngine;

public class Burr : Trap, IDamagable
{
    // References:
    Animator animator;
    BoxCollider2D boxCollider;

    private void Start()
    {
        // Set references
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Set is destroyed animation flag to false
        animator.SetBool("isDestroyed", false);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        // Only damage the player if they are not invincible (Rolly Polly Launch)
        if (!collision.gameObject.GetComponentInParent<CharacterManager>().isInvincible)
        {
            base.OnCollisionEnter2D(collision);
        }
    }

    // Method that is called when a trap is damaged
    void IDamagable.TakeDamage(int howMuch)
    {
        health = health - howMuch;
        boxCollider.enabled = false;
    }

    // Method that is called when a trap is destroyed
    void IDamagable.Die()
    {
        health = 0;
        animator.SetBool("isDestroyed", true);
    }
}
