using UnityEngine;

public class SpikeTrap : Trap
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision Detected!");
            ApplyDamage(collision.gameObject.GetComponent<IDamagable>());
        }
    }
}
