using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;

    public Collider2D groundCollider;
    public LayerMask groundMask;
    
    private float currentMovementSpeed;
    private bool isGrounded;
    private bool isFacingRight = true;

    private Rigidbody2D body;
    private GravityFlippable flippable;

    void Start()
    {   
        body = GetComponent<Rigidbody2D>();
        flippable = GetComponent<GravityFlippable>();
    }

    void FixedUpdate()
    {
        isGrounded = groundCollider.IsTouchingLayers(groundMask);

        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * currentMovementSpeed, body.velocity.y);

        if ((isFacingRight && horizontalInput < 0) || (!isFacingRight && horizontalInput > 0))
        {
            FlipHorizontal();
        }
    }

    void Update()
    {
        currentMovementSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 jumpDirection = !flippable.isUpsideDown ? Vector2.up : Vector2.down;
            body.velocity = jumpDirection * jumpForce;
        }
    }

    void FlipHorizontal()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
