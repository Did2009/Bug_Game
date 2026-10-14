using Unity.VisualScripting;
using UnityEngine;

public abstract class Trap : MonoBehaviour, IDamagable
{
    [Header("Trap Settings")]
    [SerializeField] int health = 1;
    [SerializeField] protected int damageToInflict = 1;

    protected virtual void ApplyDamage(IDamagable damagable)
    {
        damagable.TakeDamage(damageToInflict);
    }

    void IDamagable.TakeDamage(int howMuch)
    {
        health = health - howMuch;
    }

    void IDamagable.Die()
    {
        Debug.Log("Trap Destroyed!");
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
