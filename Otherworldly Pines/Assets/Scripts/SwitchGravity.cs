using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchGravity : MonoBehaviour
{

    //private float gravity = -9.81f;

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

    void FlipGravity()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            angle = angle + 180;
            transform.eulerAngles = new Vector3(0, 0, angle);
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;

            rb.gravityScale *= -1;
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
