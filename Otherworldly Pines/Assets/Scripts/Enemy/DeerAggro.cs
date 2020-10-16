using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// This class is in charge of the aggressive state of the deer.
[RequireComponent(typeof(EnemyBehavior))]
public class DeerAggro : MonoBehaviour
{
    private float aggroSpeed = 5;
    private EnemyBehavior behavior;
    

    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
    }


    

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
