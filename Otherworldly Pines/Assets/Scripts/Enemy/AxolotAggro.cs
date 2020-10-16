using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// TODO: Implement this enemy
[RequireComponent(typeof(EnemyBehavior))]
public class AxolotAggro : MonoBehaviour
{
    private EnemyBehavior behavior;
    // Start is called before the first frame update
    void Start()
    {
        this.behavior = gameObject.GetComponent<EnemyBehavior>();
    }


    

    // Update is called once per frame
    void Update()
    {
        if(this.behavior.isChasing()){
            if(this.behavior.isExausted()){
                // Do something 
            }
            else{
                // Do something
            }
        }
    }
}
