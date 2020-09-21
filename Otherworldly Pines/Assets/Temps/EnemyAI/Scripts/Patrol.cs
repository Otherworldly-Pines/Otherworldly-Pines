using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class Patrol : MonoBehaviour
{
    public float left;
    public float right;
    private float leftBound;
    private float rightBound;
    private EnemyBehavior behavior;

    // Start is called before the first frame update
    void Start()
    {
        this.initPatrolRange();
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.behavior.moveRight();
    }

    public void initPatrolRange(){
        this.leftBound = gameObject.transform.position.x + this.left;
        this.rightBound = gameObject.transform.position.x + this.right;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.behavior.isPatrolling()){
            if(gameObject.transform.position.x < this.leftBound){
                this.behavior.moveRight();
            }
            else if(gameObject.transform.position.x > this.rightBound){
                this.behavior.moveLeft();  
            }
        }
        
    }
}
