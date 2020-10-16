using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class FreeRange : MonoBehaviour
{

    public float speed = 3; // Patrolling speed 
    private EnemyBehavior behavior;
    private LayerMask groundMask;
    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        groundMask = LayerMask.GetMask("Ground", "Pushables");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.behavior.isGrounded() && this.behavior.isPatrolling()){
            if(this.reachLedge()){
                this.behavior.flipDirection();
            }


            float movementRate = 1;
            if(this.behavior.isExausted()){
                movementRate = this.behavior.getExaustedMovementRate();
            }
            
            gameObject.transform.Translate(new Vector2(this.behavior.getDirection() * Time.deltaTime * this.speed * movementRate, 0));
        }
    }

    bool reachLedge(){
        float extraHeight = 0.1f;
        Vector2 center2d = this.behavior.getCollider().bounds.center;
        RaycastHit2D raycastHit = Physics2D.Raycast(center2d + new Vector2(this.behavior.getCollider().bounds.extents.x * this.behavior.direction, 0), 
                                                    Vector2.down, 
                                                    this.behavior.getCollider().bounds.extents.y + extraHeight, 
                                                    this.groundMask);
        return raycastHit.collider == null;
    }
}
