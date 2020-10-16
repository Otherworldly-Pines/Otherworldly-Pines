using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class Patrol : MonoBehaviour
{
    public float left; // Left range of patrol
    public float right; // Right range of patrol
    public float maxLeft; // Maximun left range that is check when re initialized
    public float maxRight; // Maximun right range that is check when re initialized
    public float speed = 3; // Patrolling speed 
    public float maxLeftBound; // Max left bound but add with starting position in world for checking
    public float maxRightBound; // Max right bound but add with starting position in world for checking
    private float leftBound ; // Left bound but add with starting position in world for checking
    private float rightBound ; // Right bound but add with starting position in world for checking


    private EnemyBehavior behavior;

    // Just for gizmos drawing
    private bool gizmosInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        
        this.maxLeftBound = gameObject.transform.position.x + this.maxLeft;
        this.maxRightBound = gameObject.transform.position.x + this.maxRight;

        this.initPatrolRange();
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        gizmosInitialized = true;
    }
    
    // Init patrol range inside max bound.
    public void initPatrolRange(){
        this.rightBound = gameObject.transform.position.x + this.right;
        this.leftBound =  gameObject.transform.position.x + this.left;

        if(this.leftBound < this.maxLeftBound){
            this.leftBound = this.maxLeftBound;
            this.rightBound = Mathf.Min(this.maxRightBound,this.leftBound + this.right - this.left);
        }
        else if(this.rightBound > this.maxRightBound){
            this.rightBound = this.maxRightBound;
            this.leftBound = Mathf.Max(this.maxLeftBound,this.rightBound - this.right + this.left);
        }

    }

    // Update is called once per frame
    // Turn when reach boundary
    void Update()
    {
        if(this.behavior.isGrounded() && this.behavior.isPatrolling()){
            float movementRate = 1;
            if(this.behavior.isExausted()){
                movementRate = this.behavior.getExaustedMovementRate();
            }

            if(gameObject.transform.position.x < this.leftBound){
                this.behavior.turnRight();
            }
            else if(gameObject.transform.position.x > this.rightBound){
                this.behavior.turnLeft();
            }
            
            gameObject.transform.Translate(new Vector2(this.behavior.getDirection() * Time.deltaTime * this.speed * movementRate, 0));
        }
        
    }

    private void OnDrawGizmosSelected() {
        if (!Application.isPlaying && !gizmosInitialized) {
            this.maxLeftBound = gameObject.transform.position.x + this.maxLeft;
            this.maxRightBound = gameObject.transform.position.x + this.maxRight;
            gizmosInitialized = true;
        }
        
        float height = 3;
        Vector2 center = gameObject.transform.position;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(maxLeftBound, center.y - height / 2), new Vector2(maxLeftBound, center.y + height / 2));
        Gizmos.DrawLine(new Vector2(maxRightBound, center.y - height / 2), new Vector2(maxRightBound, center.y + height / 2));

        if (Application.isPlaying) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(new Vector2(leftBound, center.y - height / 2), new Vector2(leftBound, center.y + height / 2));
            Gizmos.DrawLine(new Vector2(rightBound, center.y - height / 2), new Vector2(rightBound, center.y + height / 2));
        }
    }

}
