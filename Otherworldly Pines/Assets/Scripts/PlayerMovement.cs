using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public LayerMask groundMask;

	private float walkSpeed = 5f;
	private float runSpeed = 7f;
	private float jumpForce = 8.5f;
    
    private float currentHorizontalInput;
    private float currentMovementSpeed;
    private bool isGrounded;
    private bool isFacingRight = true;

    private Rigidbody2D body;
    private GravityFlippable flippable;

    private Vector2 groundCastSize = new Vector2(0.6f, 0.01f);
    private Vector2 groundCastOffset = new Vector2(0f, -0.75f);

    void Start()
    {   
        body = GetComponent<Rigidbody2D>();
        flippable = GetComponent<GravityFlippable>();
    }

    void FixedUpdate()
    {
        isGrounded = CheckIsGrounded();

        body.velocity = new Vector2(currentHorizontalInput * currentMovementSpeed, body.velocity.y);

        if (currentHorizontalInput != 0 && isFacingRight == currentHorizontalInput < 0) FlipHorizontal();
    }

    void Update()
    {
        currentMovementSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        currentHorizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 jumpDirection = !flippable.isUpsideDown ? Vector2.up : Vector2.down;
            body.velocity = jumpDirection * jumpForce;
        }
    }

    bool CheckIsGrounded()
    {
        Vector2 direction = !flippable.isUpsideDown ? Vector2.down : Vector2.up;
        
        RaycastHit2D hitInfo = Physics2D.BoxCast((Vector2)transform.localPosition + groundCastOffset, groundCastSize, 0f, direction, groundCastSize.y, groundMask);
        return hitInfo.collider != null;
    }

    void FlipHorizontal()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;

        Vector2 groundCastTopLeft = (Vector2)transform.localPosition + groundCastOffset - (groundCastSize / 2.0f);
        Gizmos.DrawRay(groundCastTopLeft, new Vector2(0f, groundCastSize.y));
        Gizmos.DrawRay(groundCastTopLeft, new Vector2(groundCastSize.x, 0f));
        Gizmos.DrawRay(groundCastTopLeft + new Vector2(0f, groundCastSize.y), new Vector2(groundCastSize.x, 0f));
        Gizmos.DrawRay(groundCastTopLeft + new Vector2(groundCastSize.x, 0f), new Vector2(0f, groundCastSize.y));
    }

}
