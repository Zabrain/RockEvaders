using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMasterShotScript : MonoBehaviour {
    
    public static bool RockMasterShotMoving = false;

    private Vector2 myScreen;

    void Start()
    {
        //full screen dimension
        myScreen = new Vector2(Screen.width, Screen.height);
        myScreen = Camera.main.ScreenToWorldPoint(myScreen);

    }
	
	// Update is called once per frame
	void Update () {
        if (PauseMenu.gamePaused == true)
        {
            Time.timeScale = 0f;
        }
        else
        {
            if (RockMasterShotMoving == true)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y - 0.1f);
                RotationOfShot();

                if (transform.position.y <= -myScreen.y)
                {
                    transform.position = new Vector2(transform.position.x, myScreen.y + 1f);
                    RockMasterShotMoving = false;
                }
            }
        }
       

	}

    void RotationOfShot()
    {
        transform.Rotate(new Vector3(0, 0, -8f));
        //transform.Rotate(Vector2.right, 45 * Time.deltaTime, Space.World);
    }
}
