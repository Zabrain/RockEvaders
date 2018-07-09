using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour {

    public GameObject PowerUpObject;

    private Rigidbody2D rigidComponent;

    private float fallGravity = .3f;

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
        RotationOfShot();

        if (transform.position.y < -myScreen.y)
        {
            Destroy(gameObject);
        }
    }

    void RotationOfShot()
    {
        transform.Rotate(new Vector3(0, 0, 4f));
        //transform.Rotate(Vector2.right, 45 * Time.deltaTime, Space.World);
    }
}
