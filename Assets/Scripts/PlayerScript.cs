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

    //The Lose Menu object
    public GameObject LoseMenu;

    //The Lose Menu object
    public GameObject PlayerObject;

    //speed
    private Vector2 playerSpeed = new Vector2(10,10);

    private Vector2 myScreen;
    private Vector2 myHalfScreen;

    //movement
    private Vector2 playerMoveRate;
   private Vector2 nullMoveRate = new Vector2(0, 0);
    private Rigidbody2D rigidComponent;

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

        myHalfScreen = new Vector2(myHalfScreen.x, myHalfScreen.y);

        //print("myWorld " + myHalfScreen.x);

        gameObject.transform.position = myHalfScreen;


        //get the size of player game object
        playerPolygonSize = gameObject.GetComponent<PolygonCollider2D>().bounds.size;


        
    }
    

    void Update()
    {
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
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
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            Debug.Log("Up Swipe");
                        }
                        else
                        {   //Down swipe
                            Debug.Log("Down Swipe");
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }


        //for dirrection buttons
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }

        //Score Method
        PlayerScoring();


    }

    void FixedUpdate()
    {
        
        //grab the rigid body
        if (rigidComponent == null) rigidComponent = PlayerObject.GetComponent<Rigidbody2D>();

        
        Vector2 pos = PlayerObject.transform.position;
        
       
        if (pointAtSwipe < -.5f) //when at left
        {
            rigidComponent.velocity = playerMoveRate;
            pos.x = Mathf.Clamp(pos.x, -myScreen.x+(playerPolygonSize.x), 0);
            PlayerObject.transform.position = pos;
            
        }
        else if (pointAtSwipe > 0.5f) //when at right
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

        if (smallRockObject != null)
        {
            //destroy object rock and player
            Destroy(smallRockObject.gameObject);
            Destroy(PlayerObject.gameObject);

            //make lose menu appear
            LoseMenu.SetActive(true);
            Time.timeScale = 0f;
            FinalScoreObject.text = "Your Score is: "+intPlayerScore.ToString();
        }
    }

    private void PlayerScoring()
    {
        intPlayerScore += 1;
        PlayerScoreObject.text = intPlayerScore.ToString();
    }
    

}
