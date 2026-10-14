using UnityEngine;

public abstract class Trap : MonoBehaviour, IDamagable
{
    protected virtual void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(1);
    }

    void IDamagable.TakeDamage(int howMuch)
    {
        
    }

    void IDamagable.Die()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject whatIDamage = collision.gameObject;

        if (whatIDamage.CompareTag("Player"))
        {
            ApplyDamage(whatIDamage.GetComponent<IDamagable>());

            if (whatIDamage.GetComponent<CharacterManager>().currentHP <= 0)
            {
                whatIDamage.GetComponent<IDamagable>().Die();
            }
        }
    }
}
