using UnityEngine;

public class Web : Trap, IDamagable
{
    // References
    Animator animator;

    private void Start()
    {
        // Set References
        animator = GetComponent<Animator>();

        // Make the web harmless
        damageToInflict = 0;

        // Set is destroyed animation flag to false
        animator.SetBool("isDestroyed", false);
    }

    protected override void ApplyDamage(IDamagable damagable)
    {

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
        animator.SetBool("isDestroyed", true);
    }
}
