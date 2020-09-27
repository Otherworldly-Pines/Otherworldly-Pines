using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject animal;

    private EnemyBehavior behavior;
    private TargetChasing targetChasing;
    void Start()
    {
        this.animal = gameObject.transform.parent.gameObject;
        this.behavior = animal.GetComponent<EnemyBehavior>();
        this.targetChasing = animal.GetComponent<TargetChasing>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("IN");
        if(other.tag == "Player" || other.tag == "firecracker"){
            if(!behavior.isChasing()){
                this.targetChasing.setTarget(other.gameObject);
                this.behavior.chase();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log("OUT");
        if(other.tag == "Player" || other.tag == "firecracker"){
            if(behavior.isChasing() && other.tag == this.targetChasing.getTarget().tag){
                this.behavior.reInitPatrol();
                this.behavior.patrol();
                
            }
        }
    }
}
