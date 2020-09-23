using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFlippable : MonoBehaviour
{

    public bool gravityAffectsSprite = true;
    public bool isUpsideDown = false;

    private Rigidbody2D body;

    void Start()
    {
        body = GetComponentInParent<Rigidbody2D>();
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
