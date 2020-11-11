using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Investigate behavior of the enemy
public class Investigate : BehaviorRelated {

    private LayerMask berriesMask;
    
    // Start is called before the first frame update
    public float speed = 3; // Speed of movement

    private void Awake() {
        base.Awake();
        berriesMask = LayerMask.GetMask("Enemy Berry Colliders");
    }

    // Update is called once per frame
    // Move toward the target 
    void Update()
    {
        if(this.behavior.isGrounded() && this.behavior.isInvestigating()){
            if(this.behavior.getTarget() == null){
                this.behavior.patrol();
            }
            else{
                if(this.behavior.getTarget().transform.position.x >  gameObject.transform.position.x){
                    this.behavior.turnRight();
                }
                else{
                    this.behavior.turnLeft();
                }
                float movementRate = behavior.GetCurrentMovementSpeed();
                MoveForwardBy(Time.deltaTime * this.speed * movementRate);
            }
            
        }
    }

}
