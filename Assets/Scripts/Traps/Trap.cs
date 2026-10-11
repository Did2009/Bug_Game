using UnityEngine;

public abstract class Trap : MonoBehaviour, IDamagable
{
    IDamagable whatIDamage;

    protected virtual void ApplyDamage(IDamagable damagable)
    {
        whatIDamage.TakeDamage(1);
    }

    void IDamagable.TakeDamage(int howMuch)
    {
        
    }

    void IDamagable.Die()
    {
        
    }
}
