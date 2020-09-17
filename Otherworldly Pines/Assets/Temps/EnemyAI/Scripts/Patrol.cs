using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicMovement))]
public class Patrol : MonoBehaviour
{
    public float left;
    public float right;
    private float leftBound;
    private float rightBound;
    private BasicMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        this.leftBound = gameObject.transform.position.x + this.left;
        this.rightBound = gameObject.transform.position.x + this.right;
        this.movement = gameObject.GetComponent<BasicMovement>();
        this.movement.moveRight();
    }

    // Update is called once per frame
    void Update()
    {

        if(gameObject.transform.position.x < this.leftBound){
            this.movement.moveRight();
		}
        else if(gameObject.transform.position.x > this.rightBound){
            this.movement.moveLeft();  
		}
    }
}
