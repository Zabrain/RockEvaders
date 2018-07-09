using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    //The Bullet object
    public GameObject BulletObject;

    public GameObject SmallExplosionObject;
    public GameObject BigExplosionObject;
    public GameObject SmallShockObject;

    private Vector2 myScreen;


    // Use this for initialization
    void Start () {
        //full screen dimension
        myScreen = new Vector2(Screen.width, Screen.height);
        myScreen = Camera.main.ScreenToWorldPoint(myScreen);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    //where bullet hits rock
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        smallRock smallRockObject = otherCollider.gameObject.GetComponent<smallRock>();
        BigRock BigRockObject = otherCollider.gameObject.GetComponent<BigRock>();
        RockMasterShotScript RockmasterShot = otherCollider.gameObject.GetComponent<RockMasterShotScript>();

        if (smallRockObject != null)
        {
            //make rock explode
            Instantiate(SmallExplosionObject, smallRockObject.transform.position, transform.rotation);

            //destroy object rock and player 
            EnemyMaker.movingSmallRock1 = false;
            EnemyMaker.smallRockTimer1 = 0f;

            smallRockObject.transform.position = new Vector2(smallRockObject.transform.position.x, myScreen.y + 1f);
            Destroy(BulletObject);


        }
        else if (BigRockObject != null)
        {
            //make rock explode
            Instantiate(BigExplosionObject, BigRockObject.transform.position, transform.rotation);

            //destroy object rock and player 
            EnemyMaker.movingBigRock = false;
            EnemyMaker.bigRockTimer = 0f;

            BigRockObject.transform.position = new Vector2(BigRockObject.transform.position.x, myScreen.y + 1f);
            Destroy(BulletObject);


        }
        else if (RockmasterShot != null)
        {
            //make rock explode
            Instantiate(SmallShockObject, RockmasterShot.transform.position, transform.rotation);
            RockmasterShot.transform.position = new Vector2(RockmasterShot.transform.position.x, RockmasterShot.transform.position.y + 1);
            Destroy(BulletObject);

        }

    }

}
