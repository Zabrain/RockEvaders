using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour {

    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject BigRock;                // The enemy prefab to be spawned.
    public GameObject SmallRock;                // The enemy prefab to be spawned.
    public float spawnTimeForSmallRock = 3f;
    public float spawnTimeForBigRock = 7f; // How long between each spawn.
    public Transform[] spawnPointsBigRock;         // An array of the spawn points this enemy can spawn from.
    public Transform[] spawnPointsSmallRock;         // An array of the spawn points this enemy can spawn from.


    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("SpawnSmallRock", spawnTimeForSmallRock, spawnTimeForSmallRock);
        InvokeRepeating("SpawnBigRock", spawnTimeForBigRock, spawnTimeForBigRock);
    }


    void SpawnSmallRock()
    {
        //// If the player has no health left...
        //if (playerHealth.currentHealth <= 0f)
        //{
        //    // ... exit the function.
        //    return;
        //}

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPointsSmallRock.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(SmallRock, spawnPointsSmallRock[spawnPointIndex].position, spawnPointsSmallRock[spawnPointIndex].rotation);
    }

    void SpawnBigRock()
    {
        //// If the player has no health left...
        //if (playerHealth.currentHealth <= 0f)
        //{
        //    // ... exit the function.
        //    return;
        //}

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPointsBigRock.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(BigRock, spawnPointsBigRock[spawnPointIndex].position, spawnPointsBigRock[spawnPointIndex].rotation);
    }
}
