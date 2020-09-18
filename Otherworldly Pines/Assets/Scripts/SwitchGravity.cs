using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwitchGravity : MonoBehaviour
{

    private float gravity = -9.81f;

    private int angle = 0;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            gravity = -1 * gravity;
            angle = angle + 180;
            Physics2D.gravity = new Vector2(0f, gravity);

            transform.eulerAngles = new Vector3(0, 0, angle);
            Vector3 Scaler = transform.localScale;
            Scaler.x *= -1;
            transform.localScale = Scaler;
        }
        
    }


}
