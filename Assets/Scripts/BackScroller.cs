using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackScroller : MonoBehaviour {

    public float scrollSpeed;//speed of the background sprite
    public float tileSizeZ;//size of the background sprite

    private Vector2 myScreen;

    private Vector2 startPosition; //position of the background sprite. 

    // Use this for initialization
    void Start () {
        //startPosition = transform.position;

        //myScreen = new Vector2(Screen.width, Screen.height);
        //myScreen = Camera.main.ScreenToWorldPoint(myScreen);

        //scrollSpeed = 4;
        //tileSizeZ = 11;
    }
	
	// Update is called once per frame
	void Update () {

       //// Debug.Log(transform.position);
       // float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);
       // transform.position = startPosition + Vector2.down * newPosition;

    }
}
