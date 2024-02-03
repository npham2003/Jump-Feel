using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Vector2 = UnityEngine.Vector2;

public class CharacterScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float xdirection;
    public float xspeed;
    public float maxSpeed;
    public float accel;
    public float friction;

    public float defaultDownSpeed;
    public float gravity;
    public float yspeed;
    public float jumpForce;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.3f;

    public bool allowJump;
    public bool  haveTrail;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody.gravityScale = gravity;
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
        GetComponent<TrailRenderer>().emitting = !IsGrounded();
        if (Input.GetKey(KeyCode.W) & IsGrounded())
        {
            myRigidbody.AddForce(new Vector2(myRigidbody.velocity.x, jumpForce));
        }
        
    }


    public void SetAllowJump(bool allowJump)
    {
        this.allowJump = allowJump;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("platforms"))
        {
            if(GetComponent<Collider2D>().bounds.min.y >= other.transform.position.y)
            {
                SetAllowJump(true);
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -1f);
            }
            else
            {
                other.transform.parent.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("platforms"))
        {
            other.transform.parent.GetComponent<Collider2D>().enabled = true;
            SetAllowJump(false);
        }
    }
    private bool IsGrounded()
    {   
        Collider2D collider = GetComponent<Collider2D>();
        Vector2 bottomLeft = new Vector2(collider.bounds.min.x,collider.bounds.min.y);
        Vector2 bottomRight = new Vector2(collider.bounds.max.x, collider.bounds.min.y);
        RaycastHit2D hitLeft = Physics2D.Raycast(bottomLeft, Vector2.down, groundCheckDistance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(bottomRight, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawLine(bottomLeft, bottomLeft+Vector2.down * groundCheckDistance, Color.red);
        Debug.DrawLine(bottomRight, bottomRight+Vector2.down * groundCheckDistance, Color.red);

        return myRigidbody.velocity.y < 0 & (hitLeft.collider != null || hitRight.collider != null);
    }




}
