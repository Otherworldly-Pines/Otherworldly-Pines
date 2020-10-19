using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerControls : MonoBehaviour {

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
    private bool isPullingBlock = false;

    private float forwardCastLength = 0.01f;
    private bool isJumping = false;

    void Start()
    {   
        body = GetComponent<Rigidbody2D>();
        flippable = GetComponent<GravityFlippable>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    void FixedUpdate() {
        Vector2 nextVelocity = body.velocity;
        
        isGrounded = groundCheck.IsGrounded();

        HandlePushPull();

        float horizontalMovementSpeed = walkSpeed;
        if (!isPullingBlock && IsAgainstWall()) horizontalMovementSpeed = 0f;
        else if (currentPushable == null && isGrounded && isPressingShift) horizontalMovementSpeed = runSpeed;
        
        nextVelocity.x = currentHorizontalInput * horizontalMovementSpeed;

        body.velocity = nextVelocity;

        if (currentHorizontalInput != 0 && isFacingRight == currentHorizontalInput < 0) FlipHorizontal();
    }

    void Update() {
        isPressingShift = Input.GetKey(KeyCode.LeftShift);
        currentHorizontalInput = Input.GetAxis("Horizontal");
    
        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded())
        {
            setJumping(true);
            Debug.Log("Jump if: "+ isJumping);

            Vector2 jumpDirection = !flippable.isUpsideDown ? Vector2.up : Vector2.down;
            body.velocity = jumpDirection * jumpForce;
        }
        if (isPlayerGrounded())
        {
            //Debug.Log("Ground If: "+ isJumping);

            //   isJumping = false;
        }
    }

    public void setJumping(bool jp)
    {
        isJumping = jp;
    }
    public bool isPlayerJumping()
    {
        Debug.Log("Method Call: " + isJumping);
        return isJumping;
    }
    public bool isPlayerGrounded()
    {
        return isGrounded && !isPullingBlock;
    }

    private void HandlePushPull() {
        if (!isPullingBlock) {
            PushPullBlock lastPushable = currentPushable;
            currentPushable = GetCurrentPushable();

            // Check if the player has just turned away from a block they were previously facing
            if (lastPushable != currentPushable && lastPushable != null) {
                isPullingBlock = false;
                lastPushable.DisconnectFromBody();
                lastPushable.Soften();
            }
        } else if (currentPushable != null && !currentPushable.IsGrounded()) {
            // Check if the block the player was holding has fallen off the edge
            currentPushable.DisconnectFromBody();
            isPullingBlock = false;
            currentPushable.Harden();
            currentPushable = null;
        }
        
        if (currentPushable != null) {
            if (isGrounded && isPressingShift) {
                // Player is pushing/pulling a block
                isPullingBlock = true;
                currentPushable.ConnectToBody(body);
            } else {
                // Player is against a block but not pushing it 
                isPullingBlock = false;
                currentPushable.Harden();
            }
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

    private PushPullBlock GetCurrentPushable() {
        Collider2D collision = FrontIsTouchingMask(pushablesMask);
        if (collision != null) return collision.GetComponent<PushPullBlock>();
        return null;
    }

    private bool IsAgainstWall() {
        Collider2D collider = FrontIsTouchingMask(groundMask);
        return collider != null && !collider.isTrigger;
    }

    void FlipHorizontal() {
        if (isPullingBlock) return;
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public IEnumerator Knockback(float knockDur, float knockbackPwr, Vector2 knockbackDir)
    {
        float timer = 0;

        while (knockDur > timer)
        {
            timer += Time.deltaTime;
            body.AddForce(new Vector2(knockbackDir.x * -100, knockbackDir.y * -knockbackPwr));
        }

        yield return 0;
    }
    

}