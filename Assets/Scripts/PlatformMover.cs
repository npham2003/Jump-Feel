using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float moveDuration = 2f;

    private Rigidbody2D rb;
    private float moveTimer;
    private bool shouldMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        moveTimer -= Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + Vector2.down * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
        /*if (shouldMove && moveTimer > 0)
        {
            moveTimer -= Time.fixedDeltaTime;
            Vector2 newPosition = rb.position + Vector2.down * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);
        }
        else if (moveTimer <= 0)
        {
            shouldMove = false;
        }*/
    }
    public void StartMoving()
    {
        if (!shouldMove)
        {
            shouldMove = true;
            moveTimer = moveDuration;
        }
    }
}