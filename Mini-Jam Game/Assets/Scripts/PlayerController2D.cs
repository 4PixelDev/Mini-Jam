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

    // spriterederear
    public SpriteRenderer playerRenderer;

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

    // boolens
    bool facingRight = true;
    // bool canMove;
    //bool canJump;

    public bool canHideInBox = false;

    private int maxTimeInBox = 5;
    private int currentTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRenderer = GetComponent<SpriteRenderer>();

        // Ensure the Renderer is not null
        if (playerRenderer == null)
        {
            Debug.LogError("Renderer component not found on this GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerJump();
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check for the 'X' key press
        if (Input.GetKeyDown(KeyCode.X))
        {
            // Toggle the state of canHideInBox
            if (canHideInBox)
            {
                canHideInBox = true;
                Debug.Log("Player Can Hide Here");
                // Additional actions when hiding is enabled  // Disable rendering to make the player's sprite disappear
                playerRenderer.enabled = false;

            }
            else
            {
                Debug.Log("Player Can't Hide Here");
                // Additional actions when hiding is disabled

                // Enable rendering to make the player's sprite appear
                playerRenderer.enabled = true;
            }
        }
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void PlayerJump()
    {
        if (isGrounded) { coyoteTimeCounter = coyoteTime; }
        else { coyoteTimeCounter -= Time.deltaTime; }

        if (Input.GetKeyDown(KeyCode.UpArrow)) { jumpBufferCounter = jumpBufferTime; }
        if (Input.GetKeyDown(KeyCode.W)) { jumpBufferCounter = jumpBufferTime; }
        if (Input.GetKeyDown(KeyCode.Space)) { jumpBufferCounter = jumpBufferTime; }
        else { jumpBufferCounter -= Time.deltaTime; }


        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

            jumpBufferCounter = 0f;
        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

            jumpBufferCounter = 0f;

        }

        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumpBufferCounter = 0f;
        }


        if (Input.GetKeyUp(KeyCode.UpArrow) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
        //canJump = true;

    }

    private void PlayerMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        //canMove = true;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to a GameObject with the tag "Box"
        if (other.gameObject.CompareTag("Box"))
        {
            Debug.Log("Player Can Hide Here");

            // player can hide here from enemies
            canHideInBox = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to a GameObject with the tag "Box"
        if (other.gameObject.CompareTag("Box"))
        {
            Debug.Log("Player Can't Hide Here");
            // player can't hide here anymore
            canHideInBox = false;
        }
    }
}
