using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRock : MonoBehaviour {

    public GameObject BigRockObject;
    public float ggfgfff;

    private Rigidbody2D rigidComponent;

    private float fallGravity = .4f;

    private Vector2 myScreen;
    // Use this for initialization
    void Start()
    {

        myScreen = new Vector2(Screen.width, Screen.height);
        myScreen = Camera.main.ScreenToWorldPoint(myScreen);

        //Debug.Log(myScreen.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidComponent == null) rigidComponent = GetComponent<Rigidbody2D>();

        //make it fall
        rigidComponent.gravityScale = fallGravity;

        if (transform.position.y < -myScreen.y)
        {
            Destroy(gameObject);
        }
    }
}
