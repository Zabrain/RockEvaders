using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerManager : MonoBehaviour {

    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject VillagerObject;                // The enemy prefab to be spawned.
    

    private float spawnTimeForVillager = 1f;
    public Transform[] spawnPointsVillager;         // An array of the spawn points this enemy can spawn from.


    private GameObject Villager;

    private Vector2 myScreen;
    
    public static bool villagerMoving;
    public static float VillagerCountDown = 0f;

    void Start()
    {
        //full screen dimension
        myScreen = new Vector2(Screen.width, Screen.height);
        myScreen = Camera.main.ScreenToWorldPoint(myScreen);


        Villager = Instantiate(VillagerObject, spawnPointsVillager[0].position, spawnPointsVillager[0].rotation);

        
    }


    


    private void Update()
    {
        if (PauseMenu.gamePaused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            if (Villager != null)
            {
                //begin countdown for villager
                VillagerCountDown += Time.deltaTime;

                //position the villager at the top and start moving it down if countdown is greater than spawntime
                if (VillagerCountDown > spawnTimeForVillager && villagerMoving == false)
                {
                    if (Villager.transform.position.y > myScreen.y)
                    {
                        int spawnPointIndex = Random.Range(0, spawnPointsVillager.Length);
                        Villager.transform.position = spawnPointsVillager[spawnPointIndex].position;
                    }
                    villagerMoving = true;
                }

                if (villagerMoving == true)
                {
                    Villager.transform.position = new Vector2(Villager.transform.position.x, Villager.transform.position.y - 0.1f);
                    Villager.transform.Rotate(new Vector3(0, 0, -2f));
                }

                //instantiate villager countdown timer if villager leaves the sceen
                if (Villager.transform.position.y < -myScreen.y * 1.5)
                {
                    VillagerCountDown = 0f;
                    villagerMoving = false;
                    Villager.transform.position = spawnPointsVillager[0].position;
                }

                //Debug.Log(VillagerCountDown);
            }

        }

        
    }



}


