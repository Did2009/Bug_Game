using UnityEngine;

public class Burr : Trap, IDamagable
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponentInParent<CharacterManager>().isInvincible)
        {
            base.OnCollisionEnter2D(collision);
        }
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
        GameObject.Destroy(gameObject);
    }
}
