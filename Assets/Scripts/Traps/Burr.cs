using UnityEngine;

public class Burr : Trap
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.GetComponentInParent<CharacterManager>().isInvincible)
        {
            base.OnCollisionEnter2D(collision);
        }
    }
}
