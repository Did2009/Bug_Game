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
}
