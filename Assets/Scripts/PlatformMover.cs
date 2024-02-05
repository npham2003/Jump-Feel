using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public float moveSpeed;
    public float moveDuration;

    private Rigidbody2D rb;
    private float moveTimer;
    private bool shouldMove = false;
    GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    void FixedUpdate()
    {
        moveSpeed = gameManager.moveSpeed;
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