using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {



    public GameObject[] CloudObjects;

    public float spawnTimeForClouds = 3f;
    public Transform[] spawnPointsCloud;         // An array of the spawn points this enemy can spawn from.

    private GameObject [] theClouds = new GameObject[7];

    private Vector2 myScreen;

    void Start()
    {

        //full screen dimension
        myScreen = new Vector2(Screen.width, Screen.height);
        myScreen = Camera.main.ScreenToWorldPoint(myScreen);

        theClouds[0] = Instantiate(CloudObjects[0], spawnPointsCloud[0].position, spawnPointsCloud[0].rotation); 
        theClouds[1] = Instantiate(CloudObjects[1], spawnPointsCloud[1].position, spawnPointsCloud[1].rotation);
        theClouds[2] = Instantiate(CloudObjects[2], spawnPointsCloud[2].position, spawnPointsCloud[2].rotation);
        theClouds[3] = Instantiate(CloudObjects[3], spawnPointsCloud[3].position, spawnPointsCloud[3].rotation);
        theClouds[4] = Instantiate(CloudObjects[4], spawnPointsCloud[4].position, spawnPointsCloud[4].rotation);
        theClouds[5] = Instantiate(CloudObjects[5], spawnPointsCloud[5].position, spawnPointsCloud[5].rotation);

        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        //InvokeRepeating("SpawnCloud1", spawnTimeForClouds, 10f);




    }

    private void Update()
    {
        //for pause
        if (PauseMenu.gamePaused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            if (theClouds[0].transform.position.y < -myScreen.y)
            {
                int spawnPointIndex = Random.Range(0, spawnPointsCloud.Length);
                theClouds[0].transform.position = spawnPointsCloud[spawnPointIndex].position;
            }
            if (theClouds[1].transform.position.y < -myScreen.y)
            {
                int spawnPointIndex = Random.Range(0, spawnPointsCloud.Length);
                theClouds[1].transform.position = spawnPointsCloud[spawnPointIndex].position;
            }
            if (theClouds[2].transform.position.y < -myScreen.y)
            {
                int spawnPointIndex = Random.Range(0, spawnPointsCloud.Length);
                theClouds[2].transform.position = spawnPointsCloud[spawnPointIndex].position;
            }
            if (theClouds[3].transform.position.y < -myScreen.y)
            {
                int spawnPointIndex = Random.Range(0, spawnPointsCloud.Length);
                theClouds[3].transform.position = spawnPointsCloud[spawnPointIndex].position;
            }
            if (theClouds[4].transform.position.y < -myScreen.y)
            {
                int spawnPointIndex = Random.Range(0, spawnPointsCloud.Length);
                theClouds[4].transform.position = spawnPointsCloud[spawnPointIndex].position;
            }
            if (theClouds[5].transform.position.y < -myScreen.y)
            {
                int spawnPointIndex = Random.Range(0, spawnPointsCloud.Length);
                theClouds[5].transform.position = spawnPointsCloud[spawnPointIndex].position;
            }

            theClouds[0].transform.position = new Vector2(theClouds[0].transform.position.x, theClouds[0].transform.position.y - 0.05f);
            theClouds[1].transform.position = new Vector2(theClouds[1].transform.position.x, theClouds[1].transform.position.y - 0.05f);
            theClouds[2].transform.position = new Vector2(theClouds[2].transform.position.x, theClouds[2].transform.position.y - 0.05f);
            theClouds[3].transform.position = new Vector2(theClouds[3].transform.position.x, theClouds[3].transform.position.y - 0.05f);
            theClouds[4].transform.position = new Vector2(theClouds[4].transform.position.x, theClouds[4].transform.position.y - 0.05f);
            theClouds[5].transform.position = new Vector2(theClouds[5].transform.position.x, theClouds[5].transform.position.y - 0.05f);
        }
        

        //Debug.Log(theClouds[0].transform.position.y + "   " + myScreen.y);

       
    }

    void SpawnCloud1()
    {
        //// If the player has no health left...
        //if (playerHealth.currentHealth <= 0f)
        //{
        //    // ... exit the function.
        //    return;
        //}

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPointsCloud.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        //theClouds[0] = Instantiate(CloudObjects[0], spawnPointsCloud[spawnPointIndex].position, spawnPointsCloud[spawnPointIndex].rotation);
    }
}
