using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {

    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject PowerUpObject;                // The enemy prefab to be spawned.
    

    public float spawnTimeForPowerup = 3f;
    public Transform[] spawnPointsPowerup;         // An array of the spawn points this enemy can spawn from.



    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("SpawnPowerup", spawnTimeForPowerup, 10f);
    }


    void SpawnPowerup()
    {
        //// If the player has no health left...
        //if (playerHealth.currentHealth <= 0f)
        //{
        //    // ... exit the function.
        //    return;
        //}

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPointsPowerup.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(PowerUpObject, spawnPointsPowerup[spawnPointIndex].position, spawnPointsPowerup[spawnPointIndex].rotation);
    }
}
