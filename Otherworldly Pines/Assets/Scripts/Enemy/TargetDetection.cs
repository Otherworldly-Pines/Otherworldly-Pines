using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject animal;

    private EnemyBehavior behavior;
    private TargetLocking targetLocking;
    void Start()
    {
        this.animal = gameObject.transform.parent.gameObject;
        this.behavior = animal.GetComponent<EnemyBehavior>();
        this.targetLocking = animal.GetComponent<TargetLocking>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {   
        if(other.tag == "Player"){
            Debug.Log("IN");
            if(!behavior.isChasing()){
                this.targetLocking.setTarget(other.gameObject);
                this.behavior.chase();
            }
        }
        if(other.tag == "sandwich"){
            if(!behavior.isChasing()){
                this.targetLocking.setTarget(other.gameObject);
                this.behavior.investigate();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if(other.tag == "Player" || other.tag == "sandwich"){
            if((behavior.isChasing() || behavior.isInvestigating()) && other.tag == this.targetLocking.getTarget().tag){
                this.behavior.reInitPatrol();
                this.behavior.patrol();
                
            }
        }
    }
}
