using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GroundChecker groundCheck;
    public LayerMask groundMask;
    public LayerMask pushablesMask;

	private float walkSpeed = 5f;
	private float runSpeed = 7f;
	private float jumpForce = 8.5f;
    
    private float currentHorizontalInput;
    private bool isPressingShift = false;
    private bool isGrounded;
    private bool isFacingRight = true;

    private Rigidbody2D body;
    private GravityFlippable flippable;
    private BoxCollider2D boxCollider;

    private PushPullBlock currentPushable;

    private float forwardCastLength = 0.01f;

    void Start()
    {   
        body = GetComponent<Rigidbody2D>();
        flippable = GetComponent<GravityFlippable>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    void FixedUpdate() {
        isGrounded = groundCheck.IsGrounded();
        bool isPushing = false;
        
        Vector2 nextVelocity = body.velocity;
        nextVelocity.x = currentHorizontalInput * walkSpeed;

        PushPullBlock pushableFromLastUpdate = currentPushable;
        currentPushable = IsAgainstPushable();

        if (pushableFromLastUpdate != currentPushable && pushableFromLastUpdate != null) {
            pushableFromLastUpdate.Harden();
        }

        if (currentPushable != null) {
            if (isGrounded && isPressingShift) {
                isPushing = true;
                currentPushable.Soften();
            } else {
                currentPushable.Harden();
            }
        } else if (isGrounded && isPressingShift) {
            nextVelocity.x = currentHorizontalInput * runSpeed;
        }

        if (!isPushing && IsAgainstWall()) nextVelocity.x = 0f;

        body.velocity = nextVelocity;

        if (currentHorizontalInput != 0 && isFacingRight == currentHorizontalInput < 0) FlipHorizontal();
    }

    void Update() {
        isPressingShift = Input.GetKey(KeyCode.LeftShift);
        currentHorizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Vector2 jumpDirection = !flippable.isUpsideDown ? Vector2.up : Vector2.down;
            body.velocity = jumpDirection * jumpForce;
        }
    }

    private Collider2D FrontIsTouchingMask(LayerMask mask) {
        Vector2 offset = new Vector2(boxCollider.size.x / 2f, 0f) + boxCollider.offset;
        Vector2 origin = (Vector2) transform.localPosition + (isFacingRight ? offset : -offset);
        Vector2 size = new Vector2(0.02f, boxCollider.size.y * 0.97f);
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        
        RaycastHit2D hitInfo = Physics2D.BoxCast(origin, size, 0f, direction, size.x, mask);
        return hitInfo.collider;
    }

    private PushPullBlock IsAgainstPushable() {
        Collider2D collision = FrontIsTouchingMask(pushablesMask);
        if (collision != null) return collision.GetComponent<PushPullBlock>();
        return null;
    }

    private bool IsAgainstWall() {
        return FrontIsTouchingMask(groundMask) != null;
    }

    void FlipHorizontal()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

}
