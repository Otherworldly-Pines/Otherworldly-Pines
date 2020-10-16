using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{

    private GameObject animal;
    private EnemyBehavior behavior;
    
    void Start()
    {
        animal = gameObject.transform.parent.gameObject;
        behavior = animal.GetComponent<EnemyBehavior>();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.GetInstanceID() == this.behavior.getTarget().GetInstanceID()){
            if (!this.behavior.isPatrolling()) {
                this.behavior.patrol();
            }
        }
    }
}
