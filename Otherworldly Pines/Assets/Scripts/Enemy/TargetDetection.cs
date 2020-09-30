using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Control the detection for the box collider
[RequireComponent(typeof(EnemyBehavior))]
public class TargetDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject animal;

    private EnemyBehavior behavior;
    private TargetLocking targetLocking;
    private BoxCollider2D collider2D;
    void Start()
    {
        this.animal = gameObject.transform.parent.gameObject;
        this.behavior = animal.GetComponent<EnemyBehavior>();
        this.targetLocking = animal.GetComponent<TargetLocking>();
        this.collider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    // Rotate the box collider if direction got turned
    void Update()
    {
        if(this.behavior.getDirection() == 1){
            if(this.collider2D.offset.x < 0){
                this.collider2D.offset = new Vector2(-this.collider2D.offset.x, 1);
            }
        }
        else{
            if(this.collider2D.offset.x > 0){
                this.collider2D.offset = new Vector2(-this.collider2D.offset.x, 1);
            }
        }
    }

    // When an object enter the field. Check if its player or sandwich or player. 
    private void OnTriggerEnter2D(Collider2D other) {  
        // If enemy is not chasing, chase  
        if(other.tag == "Player"){
            Debug.Log("IN");
            if(!behavior.isChasing()){
                this.targetLocking.setTarget(other.gameObject);
                this.behavior.chase();
            }
        }
        // If enemy is not chasing, investigate. This means chasing the player have higher priority over sandwich
        if(other.tag == "sandwich"){
            if(!behavior.isChasing()){
                this.targetLocking.setTarget(other.gameObject);
                this.behavior.investigate();
            }
        }
    }

    // Check if the target we are chasing is the same as the one we are targeting
    private void OnTriggerExit2D(Collider2D other) {
        
        if(other.tag == "Player" || other.tag == "sandwich"){
            if((behavior.isChasing() || behavior.isInvestigating()) && other.tag == this.targetLocking.getTarget().tag){
                this.behavior.reInitPatrol();
                this.behavior.patrol();
                
            }
        }
    }
}
