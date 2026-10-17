using NUnit.Framework;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public GameObject[] collectibleAntsVariables;
    public bool isRandom;
    int decideIfSpawning;
    int randomSelection;

    private void Start()
    {
        // If the random flag is active, then:
        if (isRandom)
        {
            // Select at random if an ant is spawned or not
            RandomizeSpawn();
        }
        else
        {
            // Spawn an ant, always
            SpawnAnts();
        }
    }

    // Method called to randomly select if an ant is spawned or not
    void RandomizeSpawn()
    {
        // Pick between 0 (false) and 1 (true)
        decideIfSpawning = Random.Range(0, 2);

        // If 1 (true), then:
        if (decideIfSpawning == 1)
        {
            SpawnAnts();
        }
    }

    // Method called to spawn a randomly selected ant
    void SpawnAnts()
    {
        // Select a random game object from the collictble ants array
        randomSelection = Random.Range(0, collectibleAntsVariables.Length);
        GameObject selectedAnt = collectibleAntsVariables[randomSelection];

        // Spawn sekected game object at spawn point
        Instantiate<GameObject>(selectedAnt, transform.position, transform.rotation);
    }
}
