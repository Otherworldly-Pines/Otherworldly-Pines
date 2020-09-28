using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class Patrol : MonoBehaviour
{
    public float left;
    public float right;

    public float speed = 1;
    private float leftBound;
    private float rightBound;
    private EnemyBehavior behavior;
    private int currentDirection = 1;
    // Start is called before the first frame update
    void Start()
    {
        this.initPatrolRange();
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
    }

    public void initPatrolRange(){
        this.leftBound = gameObject.transform.position.x + this.left;
        this.rightBound = gameObject.transform.position.x + this.right;
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
                this.currentDirection = 1;
            }
            else if(gameObject.transform.position.x > this.rightBound){
                this.currentDirection = -1;
            }

            gameObject.transform.Translate(new Vector2(this.currentDirection * Time.deltaTime * this.speed * movementRate, 0));
        }
        
    }
}
