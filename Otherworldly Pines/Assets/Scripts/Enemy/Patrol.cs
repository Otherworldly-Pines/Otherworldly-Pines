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
    private float leftBound; // Left bound but add with starting position in world for checking
    private float rightBound; // Right bound but add with starting position in world for checking


    private EnemyBehavior behavior;
    private int direction = -1; // Current moving direction 

    // Start is called before the first frame update
    void Start()
    {
        
        this.maxLeftBound = gameObject.transform.position.x + this.maxLeft;
        this.maxRightBound = gameObject.transform.position.x + this.maxRight;

        this.initPatrolRange();
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
    }

    // Init patrol range inside max bound.
    public void initPatrolRange(){
        Debug.Log(this.rightBound);
        Debug.Log(this.leftBound);
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
        if(this.behavior.isPatrolling()){
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
}
