using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float xdirection;
    public float xspeed;
    public float maxSpeed;
    public float accel;
    public float friction;
    public bool allowJump;
    public float yspeed;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.A)) {
            xdirection = -1;
        }
        else if (Input.GetKey(KeyCode.D)) {
            xdirection = 1;
        }else
        {
            xdirection = 0;
        }
        if(xspeed < maxSpeed && (maxSpeed * -1) < xspeed && xdirection != 0)
        {
            xspeed += (xdirection * accel);
        }

        if(xdirection == 0)
        {
            if(xspeed > 0)
            {
                xspeed -= friction;
            }
            if(xspeed < 0)
            {
                xspeed += friction;
            }
        }

        myRigidbody.velocity = new Vector2(xspeed, myRigidbody.velocity.y);

    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && allowJump)
        {
            myRigidbody.AddForce(new Vector2(myRigidbody.velocity.x, jumpForce));
        }
    }


    public void SetAllowJump(bool allowJump)
    {
        this.allowJump = allowJump;
    }



}
