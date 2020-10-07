using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Target for aggro and investigation
[RequireComponent(typeof(EnemyBehavior))]
public class TargetLocking: MonoBehaviour
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
        // Debug.Log(this.target.tag);
    }
}
