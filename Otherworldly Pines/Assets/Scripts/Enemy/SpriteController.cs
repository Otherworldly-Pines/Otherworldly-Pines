using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Control the art sprite for enemy
public class SpriteController : BehaviorRelated
{
    private SpriteRenderer controller;
    // Start is called before the first frame update
    void Start()
    {
        this.controller = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    // Flip the sprite if enemy is moving right. Will need to change based on art and animation
    void Update()
    {
        if(this.behavior.getDirection() == 1){
            this.controller.flipX = true;
        }
        else{
            this.controller.flipX = false;
        }
    }

}
