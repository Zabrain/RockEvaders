using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered


    //The Score Object
    public TextMeshProUGUI PlayerScoreObject;
    public TextMeshProUGUI FinalScoreObject;
    private int intPlayerScore;
    private int intScoreBuffer;
    private int intScoreBufferValue = 20;

    //high score
    public TextMeshProUGUI HighScoreObject;
    public GameObject HighScoreConfeti;
    public Transform[] HighScoreConfetiPositions;


    public TextMeshProUGUI VillageAcquiredTextObject;
    private int intVillagersAcquired;
    private int intVillagersToAcquire;
    public TextMeshProUGUI VillageToAcquireTextObject;
    public TextMeshProUGUI VillageToAcquireTextObject1;

    //this player explosion object for prefab
    public GameObject PlayerExplosionObject;

    //this this animation plays for acquiring bullet power up   
    public GameObject BulletAcquiredAnimObject;

    //The Lose Menu object
    public GameObject LoseMenu;

    //The Player object
    public GameObject PlayerObject;
    public GameObject PlayerObjectContain;

    //the Bullet Object
    public GameObject BulletPrefab;
    public GameObject[] BulletObjectDefault;
    public List<GameObject> BulletObjectList = new List<GameObject>();
    private float bulletVelocity= 10;
    public GameObject bulletToMove;

    //bullet number of bullet available
    private int availableBullet = 2;
    private int numberOfPossibleBullets = 6;
    private int numberOfBulletsPerPotion = 2;
    public TextMeshProUGUI BulletTextObject;


    private int intHighScore;

    //speed
    private Vector2 playerSpeed = new Vector2(15,15);

    //player positions
    public Transform[] playerPositions;

    private Vector2 myScreen;
    private Vector2 myHalfScreen;

    //movement
    private Vector2 playerMoveRate;
   private Vector2 nullMoveRate = new Vector2(0, 0);
    private Rigidbody2D rigidComponent;

    //timer for touch
    float tapTimer = 0f;
    bool tabBegin = false;

    //height and width of player
    //float objectWidth, objectHeight;
    Vector2 playerPolygonSize;
    

    //tells when the plaayer is at any time
    int playerRealTimePosition = 0; //0 for center, 1 for left, 2 for right
    //bool beginSwipe = false;
    int swipeDirection = 0; //1 for left, 2 for right

    float pointAtSwipe;

    int pointToSwipe = 1;
    //bool firstSwipe = true;//true for first swipe, false for continous swipes

    Vector2 nextPlayerPosition;

    Vector2 currentPosition;


    void Start()
    {
        dragDistance = Screen.width * 13 / 100; //dragDistance is 15% height of the screen
        
        //to make object start in the center
          //full screen dimension
        myScreen = new Vector2(Screen.width, Screen.height);
        myScreen = Camera.main.ScreenToWorldPoint(myScreen);

        //half width screen dimension
        myHalfScreen = new Vector2(Screen.width/2, Screen.height * 0.1f);
        //print("screen " + myScreen.x);

        myHalfScreen = Camera.main.ScreenToWorldPoint(myHalfScreen);

        myHalfScreen = new Vector2(0, myHalfScreen.y / 1.2f);

        //print("myWorld " + myHalfScreen.x);

        PlayerObjectContain.transform.position = playerPositions[1].position;


        //get the size of player game object
        playerPolygonSize = gameObject.GetComponent<PolygonCollider2D>().bounds.size;

        //get current score
        intPlayerScore = PlayerPrefs.GetInt("Current_Score");

        //get level challenge
        intVillagersAcquired = 0;
        intVillagersToAcquire = PlayerPrefs.GetInt("Level_Amount")+3;

        //Instruction before game
        VillageToAcquireTextObject.text = "Save " + intVillagersToAcquire.ToString() + " Villagers = ";
        VillageToAcquireTextObject1.text = "X " + intVillagersToAcquire.ToString();

        //Village acquired text
        VillageAcquiredTextObject.text = intVillagersAcquired.ToString() + "/" + intVillagersToAcquire + " Villagers";


    }
    

    void Update()
    {
        //for pause
        if (PauseMenu.gamePaused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            //for swiping

            if (Input.touchCount == 1) // user is touching the screen with a single touch
            {

                Touch touch = Input.GetTouch(0); // get the touch
                if (touch.phase == TouchPhase.Began) //check for the first touch
                {
                    fp = touch.position;
                    lp = touch.position;

                    //start taptimer
                    //tapTimer += Time.deltaTime;
                    //tabBegin = true;
                }
                //else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
                //{
                //    lp = touch.position;
                //}
                else if (touch.phase == TouchPhase.Ended) // update the last position based on where they moved
                {
                    lp = touch.position;  //last touch position. Ommitted if you use list

                    //Check if drag distance is greater than 20% of the screen height
                    //if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                    if (Mathf.Abs(lp.x - fp.x) > dragDistance)
                    {//It's a drag
                     //check if the drag is vertical or horizontal
                        if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                        {   //If the horizontal movement is greater than the vertical movement...


                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                Debug.Log("Right Swipe");

                                MoveRight();
                            }
                            else
                            {   //Left swipe
                                Debug.Log("Left Swipe");

                                MoveLeft();
                            }
                        }
                    }
                    if (touch.phase == TouchPhase.Ended && fp == lp)
                    {
                        //It's a tap 
                        Debug.Log("Tap");
                        BulletShooter();
                    }

                }
               
            }

            //if (tap)
            //tapTimer +=1f;

            //Debug.Log(tapTimer);

            //for dirrection buttons///////////////////////////////
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                BulletShooter();
            }


            //keep showing the number of bullets available
            BulletTextObject.text = "X " + availableBullet.ToString();


            //Score Method
            PlayerScoring();
        }
        


        

    }

    void FixedUpdate()
    {
        
        //grab the rigid body
        if (rigidComponent == null) rigidComponent = PlayerObject.GetComponent<Rigidbody2D>();

        
        Vector2 pos = PlayerObject.transform.position;

        Vector2 posContain = PlayerObjectContain.transform.position;


        if (pointToSwipe == 0)
        {
            PlayerObjectContain.transform.position = Vector2.MoveTowards(PlayerObjectContain.transform.position, playerPositions[0].transform.position, 20 * Time.deltaTime);
        }
        else if (pointToSwipe == 1)
        {
            PlayerObjectContain.transform.position = Vector2.MoveTowards(PlayerObjectContain.transform.position, playerPositions[1].transform.position, 20 * Time.deltaTime);
        }
        else if (pointToSwipe == 2)
        {
            PlayerObjectContain.transform.position = Vector2.MoveTowards(PlayerObjectContain.transform.position, playerPositions[2].transform.position, 20 * Time.deltaTime);
        }
        Debug.Log("player pos: " + PlayerObject.transform.position.x);
        //PlayerObjectContain.transform.Translate();


        //if (pointAtSwipe < -.9f) //when at left
        //{
        //    rigidComponent.velocity = playerMoveRate;
        //    pos.x = Mathf.Clamp(pos.x, -myScreen.x+(playerPolygonSize.x *1.5f), 0);
        //    PlayerObject.transform.position = pos;

        //}
        //else if (pointAtSwipe > 0.9f) //when at right
        //{
        //    rigidComponent.velocity = playerMoveRate;
        //    pos.x = Mathf.Clamp(pos.x, 0, myScreen.x - (playerPolygonSize.x*1.5f));
        //    PlayerObject.transform.position = pos;
        //}
        //else //zero (when center)
        //{
        //    if (swipeDirection == 1) //left swipe
        //    {
        //        rigidComponent.velocity = playerMoveRate;
        //        pos.x = Mathf.Clamp(pos.x, -myScreen.x + (playerPolygonSize.x*1.5f), 0);
        //        PlayerObject.transform.position = pos;
        //    }
        //    else if (swipeDirection == 2) //right swipe
        //    {
        //        rigidComponent.velocity = playerMoveRate;
        //        pos.x = Mathf.Clamp(pos.x, 0, myScreen.x - (playerPolygonSize.x*1.5f));
        //        PlayerObject.transform.position = pos;
        //    }

        //}


        //moves the bullet
        for (int i = 0; i < BulletObjectList.Count; i++)
        {
            bulletToMove = BulletObjectList[i];
            if (bulletToMove != null)
            {
                bulletToMove.transform.Translate(new Vector2(0, 1) * Time.deltaTime * bulletVelocity);

                if (bulletToMove.transform.position.y >= myScreen.y*0.9)
                {
                    Destroy(bulletToMove);
                    BulletObjectList.Remove(bulletToMove);
                }
            }
        }


        //Debug.Log(pos);

    }


    private void MoveRight()
    {
        swipeDirection = 2; //2 for right

        pointToSwipe += 1;
        if (pointToSwipe > 2)
        {
            pointToSwipe = 2;
        }
        

    }
    private void MoveLeft()
    {
        swipeDirection = 1; //1 for left

        pointToSwipe -= 1;
        if (pointToSwipe < 0)
        {
            pointToSwipe = 0;
        }

        playerMoveRate = new Vector2(-playerSpeed.x , 0);//send direction of player movement

        Vector2 pos = PlayerObjectContain.transform.position;

        pointAtSwipe = pos.x;


    }


    


    //where player hits rock
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        smallRock smallRockObject = otherCollider.gameObject.GetComponent<smallRock>();
        BigRock BigRockObject = otherCollider.gameObject.GetComponent<BigRock>();
        PowerUps PowerUpObject = otherCollider.gameObject.GetComponent<PowerUps>();
        VillagerScript VillagerObject = otherCollider.gameObject.GetComponent<VillagerScript>();
        RockMasterShotScript RockMasterShot = otherCollider.gameObject.GetComponent<RockMasterShotScript>();

        //if the collliding object is small rock
        if (smallRockObject != null)
        {
            EnemyMaker.movingSmallRock1 = false;
            EnemyMaker.smallRockTimer1 = 0f;
            //destroy object rock and player
            smallRockObject.transform.position = new Vector2(smallRockObject.transform.position.x, myScreen.y + 1f);
            ReAudioManager.myAudioClipsSFXs[4].Play();//play player explode
            Destroy(PlayerObject.gameObject);
            Instantiate(PlayerExplosionObject, PlayerObject.transform.position, transform.rotation);//player to explode

            //make lose menu appear
            WhenPlayerDies();
        }

        //if the collliding object is big rock
        if (BigRockObject != null)
        {
            EnemyMaker.movingBigRock = false;
            EnemyMaker.bigRockTimer = 0f;
            //destroy object rock and player
            BigRockObject.transform.position = new Vector2(BigRockObject.transform.position.x, myScreen.y + 1f);
            ReAudioManager.myAudioClipsSFXs[4].Play();//play player explode
            Destroy(PlayerObject.gameObject);
            Instantiate(PlayerExplosionObject, PlayerObject.transform.position, transform.rotation);//player to explode

            //make lose menu appear
            WhenPlayerDies();
        }

        //if the collliding object is rockmaster fire
        if (RockMasterShot != null)
        {
            RockMasterShotScript.RockMasterShotMoving = false;

            //destroy object rock and player
            RockMasterShot.transform.position = new Vector2(RockMasterShot.transform.position.x, myScreen.y + 1f);
            ReAudioManager.myAudioClipsSFXs[4].Play();//play player explode
            Destroy(PlayerObject.gameObject);
            Instantiate(PlayerExplosionObject, PlayerObject.transform.position, transform.rotation);//player to explode

            //make lose menu appear
            WhenPlayerDies();

            
        }


        //if the collliding object is power up
        if (PowerUpObject != null)
        {
            //destroy power up
            Destroy(PowerUpObject.gameObject);
            ReAudioManager.myAudioClipsSFXs[6].Play();//play get powerup 
            Instantiate(BulletAcquiredAnimObject, PlayerObject.transform.position, transform.rotation);//show animation for getting power up

            //Give Player more bullets
            if (availableBullet < numberOfPossibleBullets) //check the possible bullets that can be gotten
            {
                availableBullet += numberOfBulletsPerPotion; //Increase shootable bullets
                if (availableBullet > numberOfPossibleBullets)
                {
                    availableBullet = numberOfPossibleBullets;
                }
            }
            
        }

         //if the collliding object is villager
        if (VillagerObject != null)
        {

            VillagerManager.villagerMoving = false;
            VillagerManager.VillagerCountDown = 0f;

            //destroy object rock and player
            VillagerObject.transform.position = new Vector2(VillagerObject.transform.position.x, myScreen.y +1f);
            ReAudioManager.myAudioClipsSFXs[8].Play();//play get village
            Instantiate(PlayerExplosionObject, PlayerObject.transform.position, transform.rotation);//player to explode

            intVillagersAcquired += 1;
            VillageAcquiredTextObject.text = intVillagersAcquired.ToString() + "/"+ intVillagersToAcquire+" Villagers";

            //For Level To End
            if (intVillagersAcquired == intVillagersToAcquire)
            {
                PlayerPrefs.SetInt("Current_Score", intPlayerScore);
                //Get The number of Villagers saved so far and add to the saved ones
                PlayerPrefs.GetInt("Villager_Total_Amount");
                PlayerPrefs.SetInt("Villager_Total_Amount", PlayerPrefs.GetInt("Villager_Total_Amount") +intVillagersAcquired);

                PlayerPrefs.SetInt("Level_Amount", intVillagersAcquired);

                SceneManager.LoadScene("SecondAnimation");
                SceneManager.UnloadSceneAsync("RockEvaders");
            }


        }

       

    }

    private void PlayerScoring()
    {
        intScoreBuffer += 1;
        if (intScoreBuffer == intScoreBufferValue)
        {
            intPlayerScore += 1;
            intScoreBuffer = 0;
        }
        PlayerScoreObject.text = intPlayerScore.ToString();
    }
    
    private void BulletShooter()
    {
        //check if bullet is available
        if (availableBullet > 0)
        {
            GameObject gameObject = (GameObject)Instantiate(BulletPrefab, new Vector2(transform.position.x, transform.position.y / 1.5f), Quaternion.identity);
            BulletObjectList.Add(gameObject);
                        
            ReAudioManager.myAudioClipsSFXs[3].Play(); //shoot bullet

            if (availableBullet > 0) //prevent the available bullet variable from going negative
            {
                availableBullet -= 1; //reduce bullet by 1
            }

        }

    }


    private void WhenPlayerDies()
    {
        LoseMenu.SetActive(true);
        Time.timeScale = 0f;
        FinalScoreObject.text = "Your Score is: " + intPlayerScore.ToString();
        

        //save highcore
        if (PlayerPrefs.GetInt("PlayerHighScore") < intPlayerScore)
        {
            PlayerPrefs.SetInt("PlayerHighScore", intPlayerScore);
        }

        HighScoreObject.text = "Highest Score: " + PlayerPrefs.GetInt("PlayerHighScore").ToString();
        Instantiate(HighScoreConfeti, HighScoreConfetiPositions[0].position, transform.rotation);//Confetti to explode
        Instantiate(HighScoreConfeti, HighScoreConfetiPositions[1].position, transform.rotation);//Confetti to explode
    }

}
