using NUnit.Framework;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject[] collectibleAntsVariables;
    int decideIfSpawning;
    int randomSelection;

    private void Start()
    {
        SpawnAnts();
    }

    void SpawnAnts()
    {
        // Pick between 0 (false) and 1 (true)
        decideIfSpawning = Random.Range(0, 2);

        // If 1 (true), then:
        if (decideIfSpawning == 1)
        {
            // Select a random game object from the collictble ants array
            randomSelection = Random.Range(0, collectibleAntsVariables.Length);
            GameObject selectedAnt = collectibleAntsVariables[randomSelection];

            // Spawn sekected game object at spawn point
            Instantiate<GameObject>(selectedAnt, transform.position, transform.rotation);
        }
    }
}
