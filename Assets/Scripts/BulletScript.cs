using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    //The Bullet object
    public GameObject BulletObject;
    public GameObject SmallExplosionObject;
    public GameObject BigExplosionObject;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    //where bullet hits rock
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        smallRock smallRockObject = otherCollider.gameObject.GetComponent<smallRock>();
        BigRock BigRockObject = otherCollider.gameObject.GetComponent<BigRock>();

        if (smallRockObject != null)
        {
            //make rock explode
            Instantiate(SmallExplosionObject, smallRockObject.transform.position, transform.rotation);

            //destroy object rock and bullet
            Destroy(smallRockObject.gameObject);
            Destroy(BulletObject.gameObject);

            
        }
        else if (BigRockObject != null)
        {
            //make rock explode
            Instantiate(BigExplosionObject, BigRockObject.transform.position, transform.rotation);

            //destroy object rock and player 
            Destroy(BigRockObject.gameObject);
            Destroy(BulletObject.gameObject);


        } 

    }

}
