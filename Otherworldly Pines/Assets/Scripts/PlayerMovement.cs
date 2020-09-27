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
        Vector2 boxSize = new Vector2(0.6f, 0.01f);
        Vector2 boxOrigin = transform.localPosition + new Vector3(0f, -0.75f, 0f);
        Vector2 direction = !flippable.isUpsideDown ? Vector2.down : Vector2.up;
        
        RaycastHit2D hitInfo = Physics2D.BoxCast(boxOrigin, boxSize, 0f, direction, boxSize.y, groundMask);
        Debug.DrawRay(boxOrigin - (boxSize / 2.0f), new Vector2(0f, boxSize.y));
        Debug.DrawRay(boxOrigin + (boxSize / 2.0f), new Vector2(0f, boxSize.y));
        Debug.DrawRay(boxOrigin + (boxSize / 2.0f), new Vector2(-boxSize.x, 0f));
        return hitInfo.collider != null;
    }

    void FlipHorizontal()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
