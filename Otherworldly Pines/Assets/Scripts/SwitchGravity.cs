using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchGravity : MonoBehaviour
{

    //private float gravity = -9.81f;
    public GameObject player;
    private Rigidbody2D rb;
    private int angle = 0;
    private List<Rigidbody2D> flippables = new List<Rigidbody2D>();
    void Start(){
        rb = GetComponentInParent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        FlipGravity();
        
    }

    void FlipPlayer()
    {
        angle = angle + 180;
        player.transform.eulerAngles = new Vector3(0, 0, angle);
        Vector3 Scaler = player.transform.localScale;
        Scaler.x *= -1;
        player.transform.localScale = Scaler;
        rb.gravityScale *= -1;
        if (player.GetComponent<PlayerMovement>().isUpsideDown)
            player.GetComponent<PlayerMovement>().isUpsideDown = false;
        else
            player.GetComponent<PlayerMovement>().isUpsideDown = true;
    }

    void FlipGravity()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            FlipPlayer();
            if (flippables.Count != 0)
            {
                foreach (Rigidbody2D flippable in flippables)
                {
                    flippable.gravityScale *= -1;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            FlipPlayer();
        }


        if (Input.GetKeyDown(KeyCode.L))
        {
            if (flippables.Count != 0)
            {
                foreach (Rigidbody2D flippable in flippables)
                {
                    flippable.gravityScale *= -1;
                }
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        Rigidbody2D gameObject = collider.GetComponent<Rigidbody2D>();
        if (collider.gameObject.tag == "flippable" && !flippables.Contains(gameObject))
        {
            flippables.Add(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Rigidbody2D gameObject = collider.GetComponent<Rigidbody2D>();
        if (flippables.Contains(gameObject))
        {
            flippables.Remove(gameObject);
            if(gameObject.gravityScale < 0 ){
                 gameObject.gravityScale *= -1;
            }

        }
    }
}
