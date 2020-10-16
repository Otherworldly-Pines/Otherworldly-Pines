using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Investigate behavior of the enemy
public class Investigate : BehaviorRelated
{
    // Start is called before the first frame update
    public float speed = 3; // Speed of movement

    // Update is called once per frame
    // Move toward the target 
    void Update()
    {
        if(this.behavior.isGrounded() && this.behavior.isInvestigating()){
            if(this.behavior.getTarget() == null){
                Debug.Log("Patrol");
                this.behavior.reInitPatrol();
                this.behavior.patrol();
            }
            else{
                if(this.behavior.getTarget().transform.position.x >  gameObject.transform.position.x){
                    this.behavior.turnRight();
                }
                else{
                    this.behavior.turnLeft();
                }
                float movementRate = behavior.GetCurrentMovementSpeed();
                MoveForwardBy(Time.deltaTime * this.speed * movementRate);
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Berries"){
            StartCoroutine(eatThenDestroy(other.collider.gameObject));
        }
    }

    IEnumerator eatThenDestroy(GameObject berry){
        yield return StartCoroutine(this.behavior.eatBerries());
        if(berry != null){
            Destroy(berry);
        }
        
    }
}
