using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class Patrol : MonoBehaviour
{
    public float left;
    public float right;
    public float maxLeft;
    public float maxRight;
    public float speed = 1;
    public float maxLeftBound;
    public float maxRightBound;

    public float leftBound;
    public float rightBound;

    private EnemyBehavior behavior;
    private int direction = -1;
    private SpriteController spriteController;
    // Start is called before the first frame update
    void Start()
    {
        
        this.maxLeftBound = gameObject.transform.position.x + this.maxLeft;
        this.maxRightBound = gameObject.transform.position.x + this.maxRight;

        this.initPatrolRange();
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.spriteController = gameObject.GetComponent<SpriteController>();
    }

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
    void Update()
    {
        if(this.behavior.isPatrolling()){
            float movementRate = 1;
            if(this.behavior.isExausted()){
                movementRate = this.behavior.getExaustedMovementRate();
            }

            if(gameObject.transform.position.x < this.leftBound){
                this.direction = 1;
            }
            else if(gameObject.transform.position.x > this.rightBound){
                this.direction = -1;
            }
            switch(this.direction){
                case 1:
                    this.spriteController.turnRight();
                    break;
                case -1:
                    this.spriteController.turnLeft();
                    break;
            }
            gameObject.transform.Translate(new Vector2(this.direction * Time.deltaTime * this.speed * movementRate, 0));
        }
        
    }
}
