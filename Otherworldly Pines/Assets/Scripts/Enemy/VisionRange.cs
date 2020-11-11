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
        if (behavior.IsTarget(other.gameObject)) {
            if (!behavior.isPatrolling()) {
                behavior.patrol();
            }
        }
    }
}
