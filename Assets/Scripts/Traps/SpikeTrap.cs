using UnityEngine;

public class SpikeTrap : Trap
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject whatIDamage = collision.gameObject;

        if (whatIDamage.CompareTag("Player"))
        {
            Debug.Log("Player Detected!");
            ApplyDamage(whatIDamage.GetComponent<IDamagable>());

            if (whatIDamage.GetComponent<CharacterManager>().currentHP <= 0)
            {
                whatIDamage.GetComponent<IDamagable>().Die();
            }
        }
    }
}
