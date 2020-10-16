using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class VisionRange : MonoBehaviour
{

    private GameObject animal;
    private EnemyBehavior behavior;
    // Start is called before the first frame update
    void Start()
    {
        animal = gameObject.transform.parent.gameObject;
        behavior = animal.GetComponent<EnemyBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
