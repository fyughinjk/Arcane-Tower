using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // Ground check variables
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing from the player GameObject.");
        }
        else
        {
            Debug.Log("PlayerController initialized. Rigidbody2D component acquired.");
        }

        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck Transform is not assigned in the Inspector.");
        }
    }

    void Update()
    {
        // Check if the player is grounded
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            Debug.Log("Ground check result: " + isGrounded);
        }

        // Handle horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        if (Mathf.Approximately(moveInput, 0f))
        {
            Debug.LogWarning("Horizontal input axis is not properly configured or returning zero. Check Unity Input settings.");
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        Debug.Log("Horizontal input: " + moveInput + ", Velocity: " + rb.velocity);

        // Flip the player sprite based on movement direction
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            Debug.Log("Player facing right.");
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Debug.Log("Player facing left.");
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("Jump initiated. Velocity: " + rb.velocity);
        }
    }

    void OnDrawGizmos()
    {
        // Visualize the ground check radius in the editor
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            Debug.Log("Drawing ground check gizmo at position: " + groundCheck.position);
        }
    }
}
