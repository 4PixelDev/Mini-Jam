using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpVelocity = 12f;
    private float moveInput;
    private Rigidbody2D rb;


    // ground stuff
    public bool isGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public float groundCheckRadius = 0.3f;

    // better Jump parameters 

    // coyote 
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    // Jump Buffer
    private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else { jumpBufferCounter -= Time.deltaTime; }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        PlayerJump();
    }

    private void PlayerJump()
    {
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f) /*|| Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)*/
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumpBufferCounter = 0f;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private void PlayerMovement()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}
