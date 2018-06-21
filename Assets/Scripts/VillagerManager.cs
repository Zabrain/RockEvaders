using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour {

    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject VillagerObject;                // The enemy prefab to be spawned.

    public GameObject BackgroundObject;

    public float spawnTimeForVillager = 3f;
    public Transform[] spawnPointsVillager;         // An array of the spawn points this enemy can spawn from.



    void Start()
    {
        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        InvokeRepeating("SpawnVillager", spawnTimeForVillager, 10f);
    }


    void SpawnVillager()
    {
        //// If the player has no health left...
        //if (playerHealth.currentHealth <= 0f)
        //{
        //    // ... exit the function.
        //    return;
        //}

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPointsVillager.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(VillagerObject, spawnPointsVillager[spawnPointIndex].position, spawnPointsVillager[spawnPointIndex].rotation, BackgroundObject.transform);
    }
}
