using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This class is in charge of the aggressive state of the deer.
[RequireComponent(typeof(EnemyBehavior))]

public class RabbitAggro : BasicAggro {

    public float jumpForce = 0.5f;

    private bool jumpable = true;

    // Update is called once per frame
    // It check if the deer is chasing, look for the target that is locked and move in that direction.
    // Exaust rate applied to this state too
    void Update()
    {
        base.Update();
        if (behavior.isChasing() && behavior.getTarget() != null) {
            var distanceToTarget = Vector2.Distance(transform.position, behavior.getTarget().transform.position);
            if (distanceToTarget > forgetDistance) {
                behavior.setTarget(null);
                behavior.investigate();
                return;
            }
        }

        if (behavior.isChasing() && behavior.isGrounded() && jumpable && !behavior.isExausted()) {
            jump();
        }

    }
    


    //TODO: Work with gravity flip
    void jump(){
        
        body.AddForce(new Vector2(0, jumpForce) * body.gravityScale,ForceMode2D.Impulse);
        StartCoroutine(disableJumpForSeconds(0.5f));
    }


    IEnumerator disableJumpForSeconds(float seconds){
        this.jumpable = false;
        yield return new WaitForSeconds(seconds);
        this.jumpable = true;
    }
}
