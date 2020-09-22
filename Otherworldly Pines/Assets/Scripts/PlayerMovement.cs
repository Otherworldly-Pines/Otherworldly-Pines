using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public float initialSpeed;
    public float runMultiplier;
    private float speed;

    public float jumpForce;
    private float moveInput;

    private Rigidbody2D body;
    private GravityFlippable flippable;

    private bool facingRight = true;

    public Collider2D groundCollider;
    public LayerMask groundMask;
    
    private bool isGrounded;

    void Start()
    {   
        body = GetComponent<Rigidbody2D>();
        flippable = GetComponent<GravityFlippable>();
    }

    void FixedUpdate()
    {
        isGrounded = groundCollider.IsTouchingLayers(groundMask);

        moveInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(moveInput * speed, body.velocity.y);

        if ((facingRight && moveInput < 0) || (!facingRight && moveInput > 0))
        {
            FlipHorizontal();
        }
    }

    void Update()
    {
        speed = Input.GetKey(KeyCode.LeftShift) ? initialSpeed * runMultiplier : initialSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            body.velocity = (!flippable.isUpsideDown ? Vector2.up : Vector2.down) * jumpForce;
        }
    }

    void FlipHorizontal()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

}
