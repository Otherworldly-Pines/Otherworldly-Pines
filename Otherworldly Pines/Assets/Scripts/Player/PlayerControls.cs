using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerControls : MonoBehaviour {

    public Animator animator;
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

    private bool controlsFrozen = false;
    
    //Vars Used to determine knockback
    private float timer = 0;
    private float knockDur = 0;
    private float knockbackPwr = 0;
    private Vector2 knockbackDir;

    void Start()
    {   
        body = GetComponent<Rigidbody2D>();
        flippable = GetComponent<GravityFlippable>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    void FixedUpdate() {
        if (controlsFrozen) return;
        if (shouldKnockback())
        {
            timer += Time.deltaTime;
            body.AddRelativeForce(new Vector2(this.knockbackDir.x * -10, knockbackDir.y * -knockbackPwr));
        }
        else
        {
            resetKnockback();
        }

        ;
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
        if (controlsFrozen) return;
        
        isPressingShift = Input.GetKey(KeyCode.LeftShift);
        currentHorizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isPlayerGrounded())
        {
            setJumping(true);
            Vector2 jumpDirection = !flippable.isUpsideDown ? Vector2.up : Vector2.down;
            body.velocity = jumpDirection * jumpForce;
        }

        animator.SetFloat("Speed", Mathf.Abs(currentHorizontalInput*walkSpeed));
        animator.SetBool("IsJumping", !groundCheck.IsGrounded());

    }

    public void setJumping(bool jp)
    {
        isJumping = jp;
    }
    public bool isPlayerJumping()
    {
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
            }
        } else if (currentPushable != null && !currentPushable.IsGrounded()) {
            // Check if the block the player was holding has fallen off the edge
            currentPushable.DisconnectFromBody();
            animator.SetBool("IsPulling", false);
            animator.SetBool("IsPushing", false);
            isPullingBlock = false;
            currentPushable.Soften();
            currentPushable = null;
        }

        if (currentPushable != null) {
            if (isGrounded && isPressingShift) {
                if (isPullingBlock) return;

                // Player is pushing/pulling a block
                isPullingBlock = true;
                PushPullBlock.SoftenAll();
                currentPushable.ConnectToBody(body);
                animator.SetBool("IsPulling", IsPulling(currentPushable));
                animator.SetBool("IsPushing", IsPushing(currentPushable));
            } else {
                // Player is against a block but not pushing it 
                isPullingBlock = false;
                currentPushable.Harden();
                animator.SetBool("IsPulling", false);
                animator.SetBool("IsPushing", false);
            }
        }
    }

    private bool IsPulling(PushPullBlock block){
        Vector3 playerBlockPosDiff = transform.localPosition - block.transform.localPosition;
        //player is on the right of the object and they are moving to the right direction
        bool onRightMoveRight = playerBlockPosDiff.x > Vector3.zero.x && Input.GetAxis("Horizontal") > 0;
        //player is on the left of the object and they are moving to the left direction
        bool onLeftMoveLeft = playerBlockPosDiff.x < Vector3.zero.x && Input.GetAxis("Horizontal") < 0;
        return onRightMoveRight || onLeftMoveLeft;
    }

    private bool IsPushing(PushPullBlock block){
        Vector3 playerBlockPosDiff = transform.localPosition - block.transform.localPosition;
        //player is on the right of the object and they are moving to the left direction
        bool onRightMoveLeft = playerBlockPosDiff.x > Vector3.zero.x && Input.GetAxis("Horizontal") < 0;
        //player is on the left of the object and they are moving to the right direction
        bool onLeftMoveRight = playerBlockPosDiff.x < Vector3.zero.x && Input.GetAxis("Horizontal") > 0;
        return onRightMoveLeft || onLeftMoveRight;
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

    public void Knockback(float knockDur, float knockbackPwr, Vector2 knockbackDir)
    {
        this.knockDur = knockDur;
        this.knockbackPwr = knockbackPwr;
        this.knockbackDir = knockbackDir;
    }

    public bool shouldKnockback()
    {
        return knockDur > timer;
    }

    public void resetKnockback()
    {
        timer = 0;
        this.knockDur = 0;
        this.knockbackPwr = 0;
        this.knockbackDir = new Vector2(0f,0f);
    }
    
    
    public void FreezeControls() {
        controlsFrozen = true;
    }

    public void UnfreezeControls() {
        controlsFrozen = false;
    }

}