using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour {

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered


    //The Score Object
    public TextMeshProUGUI PlayerScoreObject;
    public TextMeshProUGUI FinalScoreObject;
    private int intPlayerScore;

    //this player explosion object for prefab
    public GameObject PlayerExplosionObject;

    //this this animation plays for acquiring bullet power up   
    public GameObject BulletAcquiredAnimObject;

    //The Lose Menu object
    public GameObject LoseMenu;

    //The Player object
    public GameObject PlayerObject;

    //the Bullet Object
    public GameObject BulletPrefab;
    public List<GameObject> BulletObjectList = new List<GameObject>();
    private float bulletVelocity= 10;
    public GameObject bulletToMove;

    //bullet number of bullet available
    private int availableBullet = 2;
    private int numberOfPossibleBullets = 15;
    private int numberOfBulletsPerPotion = 5;
    public TextMeshProUGUI BulletTextObject;


    //speed
    private Vector2 playerSpeed = new Vector2(10,10);

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

    //get direction keyboard move
    float inputX;

    //tells when the plaayer is at any time
    int playerRealTimePosition = 0; //0 for center, 1 for left, 2 for right
    bool beginSwipe = false;
    int swipeDirection = 0; //1 for left, 2 for right

    float pointAtSwipe;
    //bool firstSwipe = true;//true for first swipe, false for continous swipes

    Vector2 nextPlayerPosition;


    void Start()
    {
        dragDistance = Screen.width * 10 / 100; //dragDistance is 15% height of the screen
        
        //to make object start in the center
          //full screen dimension
        myScreen = new Vector2(Screen.width, Screen.height);
        myScreen = Camera.main.ScreenToWorldPoint(myScreen);

        //half width screen dimension
        myHalfScreen = new Vector2(Screen.width/2, Screen.height * 0.1f);
        //print("screen " + myScreen.x);

        myHalfScreen = Camera.main.ScreenToWorldPoint(myHalfScreen);

        myHalfScreen = new Vector2(myHalfScreen.x, myHalfScreen.y / 1.2f);

        //print("myWorld " + myHalfScreen.x);

        gameObject.transform.position = myHalfScreen;


        //get the size of player game object
        playerPolygonSize = gameObject.GetComponent<PolygonCollider2D>().bounds.size;


        
    }
    

    void Update()
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
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
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
                    //else
                    //{   //the vertical movement is greater than the horizontal movement
                    //    if (lp.y > fp.y)  //If the movement was up
                    //    {   //Up swipe
                    //        Debug.Log("Up Swipe");
                    //    }
                    //    else
                    //    {   //Down swipe
                    //        Debug.Log("Down Swipe");
                    //    }
                    //}
                }
                
            }
            else if (touch.phase == TouchPhase.Ended && touch.tapCount == 2)
            {
                //It's a tap 
                Debug.Log("Tap");
                BulletShooter();

                //tapBegin = false;
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

    void FixedUpdate()
    {
        
        //grab the rigid body
        if (rigidComponent == null) rigidComponent = PlayerObject.GetComponent<Rigidbody2D>();

        
        Vector2 pos = PlayerObject.transform.position;
        
       
        if (pointAtSwipe < -.9f) //when at left
        {
            rigidComponent.velocity = playerMoveRate;
            pos.x = Mathf.Clamp(pos.x, -myScreen.x+(playerPolygonSize.x), 0);
            PlayerObject.transform.position = pos;
            
        }
        else if (pointAtSwipe > 0.9f) //when at right
        {
            rigidComponent.velocity = playerMoveRate;
            pos.x = Mathf.Clamp(pos.x, 0, myScreen.x - (playerPolygonSize.x));
            PlayerObject.transform.position = pos;
        }
        else //zero (when center)
        {
            if (swipeDirection == 1) //left swipe
            {
                rigidComponent.velocity = playerMoveRate;
                pos.x = Mathf.Clamp(pos.x, -myScreen.x + (playerPolygonSize.x), 0);
                PlayerObject.transform.position = pos;
            }
            else if (swipeDirection == 2) //right swipe
            {
                rigidComponent.velocity = playerMoveRate;
                pos.x = Mathf.Clamp(pos.x, 0, myScreen.x - (playerPolygonSize.x));
                PlayerObject.transform.position = pos;
            }

        }


        //moves the bullet
        for (int i = 0; i < BulletObjectList.Count; i++)
        {
            bulletToMove = BulletObjectList[i];
            if (bulletToMove != null)
            {
                bulletToMove.transform.Translate(new Vector2(0, 1) * Time.deltaTime * bulletVelocity);

                if (bulletToMove.transform.position.y > myScreen.y)
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
        playerMoveRate = new Vector2(playerSpeed.x, 0);//send direction of player movement

       Vector2 pos = PlayerObject.transform.position;

        pointAtSwipe = pos.x;

    }
    private void MoveLeft()
    {
        swipeDirection = 1; //1 for left
        playerMoveRate = new Vector2(-playerSpeed.x , 0);//send direction of player movement

        Vector2 pos = PlayerObject.transform.position;

        pointAtSwipe = pos.x;

    }


    private void DirectionButtonTester()
    {
        playerMoveRate = new Vector2(playerSpeed.x * inputX, 0);
    }


    //where player hits rock
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        smallRock smallRockObject = otherCollider.gameObject.GetComponent<smallRock>();
        BigRock BigRockObject = otherCollider.gameObject.GetComponent<BigRock>();
        PowerUps PowerUpObject = otherCollider.gameObject.GetComponent<PowerUps>();

        //if the collliding object is small rock
        if (smallRockObject != null)
        {
            //destroy object rock and player
            Destroy(smallRockObject.gameObject);
            Destroy(PlayerObject.gameObject);
            Instantiate(PlayerExplosionObject, PlayerObject.transform.position, transform.rotation);//player to explode

            //make lose menu appear
            LoseMenu.SetActive(true);
            Time.timeScale = 0f;
            FinalScoreObject.text = "Your Score is: "+intPlayerScore.ToString();
        }

        //if the collliding object is big rock
        if (BigRockObject != null)
        {
            //destroy object rock and player
            Destroy(BigRockObject.gameObject);
            Destroy(PlayerObject.gameObject);
            Instantiate(PlayerExplosionObject, PlayerObject.transform.position, transform.rotation);//player to explode

            //make lose menu appear
            LoseMenu.SetActive(true);
            Time.timeScale = 0f;
            FinalScoreObject.text = "Your Score is: " + intPlayerScore.ToString();
        }

        //if the collliding object is power up
        if (PowerUpObject != null)
        {
            //destroy power up
            Destroy(PowerUpObject.gameObject);
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

    }

    private void PlayerScoring()
    {
        intPlayerScore += 1;
        PlayerScoreObject.text = intPlayerScore.ToString();
    }
    
    private void BulletShooter()
    {
        //check if bullet is available
        if (availableBullet > 0)
        {
            GameObject gameObject = (GameObject)Instantiate(BulletPrefab, new Vector2(transform.position.x, transform.position.y / 1.5f ), Quaternion.identity);
            BulletObjectList.Add(gameObject);

            if (availableBullet > 0) //prevent the available bullet variable from going negative
            {
                availableBullet -= 1; //reduce bullet by 1
            }
            
        }
                
    }

}
