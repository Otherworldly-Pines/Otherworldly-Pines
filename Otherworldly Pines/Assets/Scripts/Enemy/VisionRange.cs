using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : BehaviorRelated
{

    private GameObject animal;
    
    void Start()
    {
        animal = gameObject.transform.parent.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.GetInstanceID() == this.behavior.getTarget().GetInstanceID()){
            if (!this.behavior.isPatrolling()) {
                    Debug.Log("Patrol");
                    this.behavior.reInitPatrol();
                    this.behavior.patrol();
                }
        }
    }
}
