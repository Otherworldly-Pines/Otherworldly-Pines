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

    private Rigidbody2D rb;

    private bool facingRight = true;
    
    private bool isGrounded;

    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public bool isUpsideDown = false;

    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0){
            Flip();
        }else if(facingRight == true && moveInput < 0){
            Flip();
        }
    }

    void Update(){

        speed = Input.GetKey(KeyCode.LeftShift) ? initialSpeed * runMultiplier : initialSpeed;

        if( Input.GetKeyDown(KeyCode.Space) && isGrounded == true && !isUpsideDown){
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true && isUpsideDown)
        {
            rb.velocity = Vector2.down * jumpForce;
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
