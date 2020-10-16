using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerAggro : BehaviorRelated
{
    private float aggroSpeed = 5;
    
    // Update is called once per frame
    // It check if the deer is chasing, look for the target that is locked and move in that direction.
    // Exaust rate applied to this state too
    void Update()
    {
        if(this.behavior.isGrounded() && this.behavior.isChasing()){
            Vector2 direction;
            if(this.behavior.getTarget().transform.position.x > gameObject.transform.position.x){
                this.behavior.turnRight();
            }
            else{
                this.behavior.turnLeft();
            }

            float movementRate = 1;
            if(this.behavior.isExausted()){
                movementRate = this.behavior.getExaustedMovementRate();        
            }
            gameObject.transform.Translate(new Vector2(this.behavior.getDirection() * Time.deltaTime * this.aggroSpeed * movementRate, 0));
        }

    }
}
