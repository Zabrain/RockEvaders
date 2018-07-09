using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaker : MonoBehaviour {

    public PlayerHealth playerHealth;       // Reference to the player's heatlh.

    public GameObject BigRock;                // The enemy prefab to be spawned.
    private GameObject[] TheBigRocks = new GameObject[2];                // The enemy prefab to be spawned.

    public GameObject SmallRock;                // The enemy prefab to be spawned.
    private GameObject[] TheSmallRocks = new GameObject[3];                // The enemy prefab to be spawned.

    public GameObject RockMaster;                // The enemy prefab to be spawned.
    private GameObject TheRockMaster;

    public GameObject RockMasterShot;
    private GameObject[] TheRockMasterShots = new GameObject[1];



    public Transform[] spawnPointsBigRock;         // An array of the spawn points this enemy can spawn from.
    public Transform[] spawnPointsSmallRock;         // An array of the spawn points this enemy can spawn from.
    public Transform[] spawnPointsRockMaster;         // An array of the spawn points this enemy can spawn from.
    
    public static bool movingBigRock = false;
    int randomBigRockPoint;
    public static float bigRockTimer = 0;
    private float spawnTimeForBigRock = 9f; // How long between each spawn.
    private float bigRockSpeed=0.12f;


    public static bool movingSmallRock1 = false;
    public static bool movingSmallRock2 = false;
    public static float smallRockTimer1 = 0;
    public static float smallRockTimer2 = 0;
    int randomSmallRockPoint1;
    int randomSmallRockPoint2;
    private float spawnTimeForSmallRock1 = 3f;
    private float spawnTimeForSmallRock2 = 3f;
    private float smallRockSpeed1 = 0.08f;

    public static bool movingRockMaster = false;
    int randomRockMasterPoint;
    public static float RockMasterTimer = 0;
    public static float RockMasterTimeToStayed = 0f;
    public static float RockMasterTimeToStay = 4f;
    public static float RockMasterTimeBeforeShot = 0f;
    public static float RockMasterTimeForShot = 3f;
    private float spawnTimeForRockMaster = 3f; // How long between each spawn.


    private float TimeBeforeEnemiesStartCounter = 0f;
    private float TimeBeforeEnemiesStart = 5f;
    private bool EnemyStart = false;

    private Vector2 myScreen;

    void Start()
    {

        //full screen dimension
        myScreen = new Vector2(Screen.width, Screen.height);
        myScreen = Camera.main.ScreenToWorldPoint(myScreen);

        //instantiate all enemies
        TheSmallRocks[0] = Instantiate(SmallRock, spawnPointsSmallRock[0].position, spawnPointsSmallRock[0].rotation);
        TheSmallRocks[1] = Instantiate(SmallRock, spawnPointsSmallRock[1].position, spawnPointsSmallRock[1].rotation);
        TheSmallRocks[2] = Instantiate(SmallRock, spawnPointsSmallRock[2].position, spawnPointsSmallRock[2].rotation);

        TheBigRocks[0] = Instantiate(BigRock, spawnPointsBigRock[0].position, spawnPointsBigRock[0].rotation);
        TheBigRocks[1] = Instantiate(BigRock, spawnPointsBigRock[1].position, spawnPointsBigRock[1].rotation);

        TheRockMaster = Instantiate(RockMaster, spawnPointsRockMaster[0].position, spawnPointsRockMaster[0].rotation);

        TheRockMasterShots[0]= Instantiate(RockMasterShot, spawnPointsSmallRock[2].position, spawnPointsSmallRock[2].rotation);


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
            //if delay time before game has elaspsed
            if (!EnemyStart)
            {
                TimeBeforeEnemiesStartCounter += Time.deltaTime;
                if (TimeBeforeEnemiesStartCounter > TimeBeforeEnemiesStart) //if delay time elapses
                {
                    EnemyStart = true;
                }
            }
            else 
            {
                SpawnBigRock();
                SpawnSmallRock1();
                SpawnRockMaster();
            }



        }

        // TheRockMaster.transform.Translate(spawnPointsRockMaster.position / 1.5f);

        //Debug.Log(theClouds[0].transform.position.y + "   " + myScreen.y);

        //Debug.Log("TheSmallRocks.Length " + TheSmallRocks.Length);


    }


    void SpawnSmallRock1()
    {
        if (movingSmallRock1 == false)
        {
            smallRockTimer1 += Time.deltaTime;
            if (smallRockTimer1 > spawnTimeForSmallRock1)
            {
                randomSmallRockPoint1 = Random.Range(0, TheSmallRocks.Length-1);
                //flags on the moving
                movingSmallRock1 = true;
                smallRockTimer1 = 0f;
            }
        }

        if (movingSmallRock1 == true)
        {
            if (TheSmallRocks[randomSmallRockPoint1].transform.position.y >= -myScreen.y)
            {
                TheSmallRocks[randomSmallRockPoint1].transform.position = new Vector2(TheSmallRocks[randomSmallRockPoint1].transform.position.x, TheSmallRocks[randomSmallRockPoint1].transform.position.y - smallRockSpeed1);

                Debug.Log(TheSmallRocks[randomSmallRockPoint1].transform.position.y);
            }
            else if (TheSmallRocks[randomSmallRockPoint1].transform.position.y < -myScreen.y)
            {
                TheSmallRocks[randomSmallRockPoint1].transform.position = spawnPointsSmallRock[randomSmallRockPoint1].position;
                movingSmallRock1 = false;
            }
        }
    }




    void SpawnBigRock()
    {
        if (movingBigRock == false)
        {
            bigRockTimer += Time.deltaTime;
            if (bigRockTimer > spawnTimeForBigRock)
            {
                randomBigRockPoint = Random.Range(0, TheBigRocks.Length-1);
                //flags on the moving
                movingBigRock = true;
                bigRockTimer = 0f;
            }
        }

        if (movingBigRock == true)
        {
            if (TheBigRocks[randomBigRockPoint].transform.position.y >= -myScreen.y)
            {
                TheBigRocks[randomBigRockPoint].transform.position = new Vector2(TheBigRocks[randomBigRockPoint].transform.position.x, TheBigRocks[randomBigRockPoint].transform.position.y - bigRockSpeed);
            }
            else if (TheBigRocks[randomBigRockPoint].transform.position.y < -myScreen.y)
            {
                TheBigRocks[randomBigRockPoint].transform.position = spawnPointsBigRock[randomBigRockPoint].position;
                movingBigRock = false;
            }
        }
    }
    void SpawnRockMaster()
    {
        if (movingRockMaster == false)
        {
            RockMasterTimer += Time.deltaTime;
            if (RockMasterTimer > spawnTimeForRockMaster)
            {
                randomRockMasterPoint = Random.Range(0, spawnPointsRockMaster.Length - 1);
                //flags on the moving
                movingRockMaster = true;
                RockMasterTimer = 0f;
            }
        }

        if (movingRockMaster == true)
        {
            if (TheRockMaster.transform.position.y >= spawnPointsRockMaster[randomRockMasterPoint].transform.position.y / 1.6f) 
            {
                TheRockMaster.transform.position = new Vector2(spawnPointsRockMaster[randomRockMasterPoint].transform.position.x, TheRockMaster.transform.position.y - .08f);
            }

            RockMasterTimeToStayed += Time.deltaTime;
            RockMasterTimeBeforeShot += Time.deltaTime;
            if (RockMasterTimeBeforeShot > RockMasterTimeForShot)
            {
                TheRockMasterShots[0].transform.position = TheRockMaster.transform.position;
                RockMasterShotScript.RockMasterShotMoving = true;
                RockMasterTimeBeforeShot = 0f;
            }

            if (RockMasterTimeToStayed > RockMasterTimeToStay)
            {
                movingRockMaster = false;
                RockMasterTimeToStayed = 0f;
                RockMasterTimeBeforeShot = 0f;
                TheRockMaster.transform.position = spawnPointsRockMaster[randomRockMasterPoint].transform.position;
            }
            
        }

    }
    

}
