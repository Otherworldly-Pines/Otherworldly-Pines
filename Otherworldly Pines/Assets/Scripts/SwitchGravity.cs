using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchGravity : MonoBehaviour
{

    //private float gravity = -9.81f;

    private Rigidbody2D rb;
    private int angle = 0;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J)){
            angle = angle + 180;
            transform.eulerAngles = new Vector3(0, 0, angle);
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;

            rb.gravityScale *= -1;
        }
        
    }


}
