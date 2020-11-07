using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlippable : GravityAffected
{

    public bool gravityAffectsSprite = true;
    public bool isUpsideDown = false;

    private Rigidbody2D body;


    void Start()
    {
        base.Start();
        body = GetComponentInParent<Rigidbody2D>();
    }

    public bool StillExists() {
        return body != null;
    }
    
    // Flip gravity
    public void Flip()
    {
        body.gravityScale *= -1;
        isUpsideDown = body.gravityScale < 0;
        
        if (gravityAffectsSprite)
        {
            Vector3 flippedScale = transform.localScale;
            flippedScale.y *= -1;
            transform.localScale = flippedScale;            
        }
    }
}
