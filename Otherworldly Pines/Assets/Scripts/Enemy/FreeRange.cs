using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class FreeRange : BehaviorRelated
{

    public float speed = 3; // Patrolling speed 
    private LayerMask groundMask;
    
    void Start() {
        groundMask = LayerMask.GetMask("Ground", "Pushables");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.behavior.isGrounded() && this.behavior.isPatrolling()){
            if(this.reachLedge()){
                this.behavior.flipDirection();
            }

            float movementRate = behavior.GetCurrentMovementSpeed();
            MoveForwardBy(Time.deltaTime * speed * movementRate);
        }
    }

    bool reachLedge(){
        float extraHeight = 0.1f;
        Vector2 center2d = this.behavior.getCollider().bounds.center;
        RaycastHit2D raycastHit = Physics2D.Raycast(center2d + new Vector2(collider.bounds.extents.x * this.behavior.direction, 0), 
                                                    this.body.gravityScale * Vector2.down, 
                                                    collider.bounds.extents.y + extraHeight, 
                                                    this.groundMask);
        return raycastHit.collider == null;
    }
}
