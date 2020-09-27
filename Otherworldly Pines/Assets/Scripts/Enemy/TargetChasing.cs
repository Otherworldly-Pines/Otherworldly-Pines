using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyBehavior))]
public class TargetChasing : MonoBehaviour
{
    private EnemyBehavior behavior;
    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
        this.target = GameObject.FindGameObjectWithTag("Player");
    }

    public void setTarget(GameObject target){
        this.target = target;
    }

    public GameObject getTarget(){
        return this.target;
    }


    // Update is called once per frame
    void Update()
    {
        if(this.behavior.isChasing()){
            if(this.target.transform.position.x - gameObject.transform.position.x < 0){
                this.behavior.moveLeft();
            }
            else{
                this.behavior.moveRight();
            }
        }
    }
}
